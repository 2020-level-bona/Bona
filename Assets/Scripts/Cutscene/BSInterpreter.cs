﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BSInterpreter : IScriptSession
{
    Level level;
    ChatManager chatManager;
    List<BSException> syntaxErrors;
    Queue<IScriptCommand> commands;
    public List<Token> tokens {get;}
    bool expired = false;
    public List<LinePointer> linePointers {get; set;} = new List<LinePointer>();

    public BSInterpreter(Level level, ChatManager chatManager, string code) {
        this.level = level;
        this.chatManager = chatManager;

        tokens = new List<Token>();

        syntaxErrors = new List<BSException>();
        commands = ParseCode(code);
    }

    public void MakeExpire() {
        expired = true;
    }

    public bool HasExpired() {
        return expired;
    }

    public Queue<IScriptCommand> GetCommands() {
        return commands;
    }

    public List<BSException> GetSyntaxErrors() {
        return syntaxErrors;
    }

    Queue<IScriptCommand> ParseCode(string code) {
        string[] lines = code.Split('\n');

        Queue<IScriptCommand> commands = new Queue<IScriptCommand>();
        for (int i = 0; i < lines.Length; i++) {
            string line = lines[i].Trim();
            if (line.Length == 0)
                continue;
            
            try {
                IScriptCommand command = ParseLine(i, lines[i]);
                if (command != null)
                    commands.Enqueue(command);
            } catch (BSException e) {
                syntaxErrors.Add(e);
            }
        }            

        return commands;
    }

    IScriptCommand ParseLine(int lineNumber, string line) {
        CommandLineParser lineParser = new CommandLineParser(lineNumber, line);
        tokens.AddRange(lineParser.args);
        if (lineParser.HasKeyword()) {
            switch (lineParser.GetKeyword()) {
                case HideCommand.Keyword:
                    return new HideCommand(level, lineParser);
                case MessageCommand.Keyword:
                    return new MessageCommand(chatManager, level, lineParser);
                case MoveCommand.Keyword:
                    return new MoveCommand(level, lineParser);
                case ShowCommand.Keyword:
                    return new ShowCommand(level, lineParser);
                case WaitCommand.Keyword:
                    return new WaitCommand(lineParser);
            }
            throw new BSSyntaxException(lineNumber, $"키워드 {lineParser.GetKeyword()}에 해당하는 명령어가 존재하지 않습니다.");
        }
        return null;
    }
}
