using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoCommand : IControlCommand, ICommandProvider
{
    Queue<ICommand> commands = new Queue<ICommand>();

    public const string Keyword = "DO";
    public bool Blocking => false;
    public int LineNumber {get;}

    public DoCommand() { }

    public DoCommand(CommandLineParser lineParser) {
        LineNumber = lineParser.lineNumber;
    }

    public void SetCommandQueue(Queue<ICommand> commands) {
        this.commands = commands;
    }

    public ICommandProvider GetCommandProvider() {
        return this;
    }

    public ICommand Next() {
        if (commands.Count == 0)
            return null;
        return commands.Dequeue();
    }
}
