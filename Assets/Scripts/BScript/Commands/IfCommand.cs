using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IfCommand : IControlCommand
{
    string expression;

    public const string Keyword = "IF";
    public bool Blocking => false;
    public int LineNumber {get;}

    public IfCommand(string expression) {
        this.expression = expression;
    }

    public IfCommand(CommandLineParser lineParser) {
        LineNumber = lineParser.lineNumber;
        this.expression = lineParser.GetAllStrings();
    }

    public ICommandProvider GetCommandProvider() {
        return null; // TODO
    }
}
