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

        for (int i = 0; i < lines.Length; i++)
            lines[i] = lines[i].Trim();
        
        int line = 0;
        return ParseCode(lines, ref line, 0);
    }

    Queue<ICommand> ParseCode(string[] lines, ref int line, int depth) {
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
                    (command as DoCommand).SetCommandQueue(ParseCode(lines, ref line, depth + 1));
                    commands.Enqueue(command);
                } else if (command is EndCommand) {
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
            switch (lineParser.GetKeyword()) {
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
                
                // 제어문
                case DoCommand.Keyword:
                    return new DoCommand(lineParser);
                case EndCommand.Keyword:
                    return new EndCommand(lineParser);
            }
            throw new BSSyntaxException(lineNumber, $"키워드 {lineParser.GetKeyword()}에 해당하는 명령어가 존재하지 않습니다.");
        }
        return null;
    }
}
