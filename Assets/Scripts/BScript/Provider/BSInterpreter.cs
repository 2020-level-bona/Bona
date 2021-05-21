using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BSInterpreter : ICommandProvider
{
    Game game;
    Level level;
    ChatManager chatManager;
    List<BSException> syntaxErrors;
    Queue<ICommand> commands;
    public List<Token> tokens {get;}

    public BSInterpreter(Game game, Level level, ChatManager chatManager, string code) {
        this.game = game;
        this.level = level;
        this.chatManager = chatManager;

        tokens = new List<Token>();

        syntaxErrors = new List<BSException>();
        commands = ParseCode(code);
    }

    public ICommand Next() {
        if (commands.Count == 0)
            return null;
        return commands.Dequeue();
    }

    public List<BSException> GetSyntaxErrors() {
        return syntaxErrors;
    }

    Queue<ICommand> ParseCode(string code) {
        string[] lines = code.Split('\n');
        
        int line = 0;
        Queue<ICommand> commands = ParseCode(lines, ref line, 0, null);

        // 모든 줄을 다 읽었는지 확인
        if (line < lines.Length)
            throw new BSSyntaxException(line, "구문이 잘못되었습니다.");
        
        return commands;
    }

    Queue<ICommand> ParseCode(string[] lines, ref int line, int depth, IfCommand branch) {
        Queue<ICommand> commands = new Queue<ICommand>();

        while(line < lines.Length) {
            if (lines[line].Length == 0) {
                line++;
                continue;
            }
            
            try {
                ICommand command = ParseLine(line, lines[line]);
                if (command is IActionCommand) {
                    commands.Enqueue(command);
                } else if (command is DoCommand) {
                    line++;
                    (command as DoCommand).SetCommandQueue(ParseCode(lines, ref line, depth + 1, null));
                    commands.Enqueue(command);
                } else if(command is IfCommand) {
                    line++;
                    IfCommand newBranch = command as IfCommand;
                    newBranch.AddBranch(newBranch.currentExpression, ParseCode(lines, ref line, depth + 1, newBranch));
                    commands.Enqueue(command);
                } else if (command is ElifCommand) {
                    if (branch == null)
                        throw new BSSyntaxException(line, "ELIF 이전의 IF가 없습니다.");
                    
                    line++;
                    branch.AddBranch((command as ElifCommand).currentExpression, ParseCode(lines, ref line, depth + 1, branch));
                    return commands;
                } else if (command is ElseCommand) {
                    if (branch == null)
                        throw new BSSyntaxException(line, "ELSE 이전의 IF가 없습니다.");
                    
                    line++;
                    branch.AddBranch("true", ParseCode(lines, ref line, depth + 1, branch));
                    return commands;
                }  else if (command is EndCommand) {
                    if (depth == 0)
                        throw new BSSyntaxException(line, "제어문이 불필요하게 닫혔습니다.");
                    return commands;
                }
            } catch (BSException e) {
                syntaxErrors.Add(e);
            }

            line++;
        }

        if (depth > 0)
            throw new BSSyntaxException(line, "제어문이 닫히지 않았습니다. END를 추가해주세요.");
        return commands;
    }

    ICommand ParseLine(int lineNumber, string line) {
        CommandLineParser lineParser = new CommandLineParser(lineNumber, line);

        tokens.AddRange(lineParser.args);
        if (lineParser.HasKeyword()) {
            switch (lineParser.GetKeyword().ToUpper()) {
                case HideCommand.Keyword:
                    return new HideCommand(level, lineParser);
                case SayCommand.Keyword:
                    return new SayCommand(chatManager, level, lineParser);
                case MoveCommand.Keyword:
                    return new MoveCommand(level, lineParser);
                case ShowCommand.Keyword:
                    return new ShowCommand(level, lineParser);
                case WaitCommand.Keyword:
                    return new WaitCommand(lineParser);
                case TransferCommand.Keyword:
                    return new TransferCommand(game, lineParser);
                case SetCommand.Keyword:
                    return new SetCommand(lineParser);
                case PlayAnimCommand.Keyword:
                    return new PlayAnimCommand(level, lineParser);
                case StopAnimCommand.Keyword:
                    return new StopAnimCommand(level, lineParser);
                case SpeedCommand.Keyword:
                    return new SpeedCommand(level, lineParser);
                case FadeInCommand.Keyword:
                    return new FadeInCommand(lineParser);
                case FadeOutCommand.Keyword:
                    return new FadeOutCommand(lineParser);
                case CameraMoveCommand.Keyword:
                    return new CameraMoveCommand(level, lineParser);
                case CameraAddTargetCommand.Keyword:
                    return new CameraAddTargetCommand(level, lineParser);
                case CameraDeleteTargetCommand.Keyword:
                    return new CameraDeleteTargetCommand(level, lineParser);
                case CameraClearTargetsCommand.Keyword:
                    return new CameraClearTargetsCommand(lineParser);
                case FaceCommand.Keyword:
                    return new FaceCommand(level, lineParser);
                case GiveCommand.Keyword:
                    return new GiveCommand(lineParser);
                case TakeCommand.Keyword:
                    return new TakeCommand(lineParser);

                // 제어문
                case DoCommand.Keyword:
                    return new DoCommand(lineParser);
                case IfCommand.Keyword:
                    return new IfCommand(lineParser);
                case ElifCommand.Keyword:
                    return new ElifCommand(lineParser);
                case ElseCommand.Keyword:
                    return new ElseCommand(lineParser);
                case EndCommand.Keyword:
                    return new EndCommand(lineParser);
                
                // FIXME: 매크로 더 스마트하게 처리
                case "#":
                    return new SayCommand(lineNumber, chatManager, level, CharacterType.BONA, lineParser.GetAllStrings(1));
            }
            throw new BSSyntaxException(lineNumber, $"키워드 {lineParser.GetKeyword()}에 해당하는 명령어가 존재하지 않습니다.");
        }
        return null;
    }
}
