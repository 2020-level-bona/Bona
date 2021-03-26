using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElseCommand : IControlCommand
{
    public const string Keyword = "ELSE";
    public bool Blocking => true;
    public int LineNumber {get;}

    public ElseCommand() { }

    public ElseCommand(CommandLineParser lineParser) {
        LineNumber = lineParser.lineNumber;
    }

    public ICommandProvider GetCommandProvider() {
        return null;
    }
}
