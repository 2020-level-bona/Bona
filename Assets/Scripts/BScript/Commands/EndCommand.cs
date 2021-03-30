using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndCommand : IControlCommand
{
    public const string Keyword = "END";
    public bool Blocking => true;
    public int LineNumber {get;}

    public EndCommand() { }

    public EndCommand(CommandLineParser lineParser) {
        LineNumber = lineParser.lineNumber;
    }

    public ICommandProvider GetCommandProvider() {
        return null;
    }
}
