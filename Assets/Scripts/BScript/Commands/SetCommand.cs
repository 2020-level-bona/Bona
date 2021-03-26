using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetCommand : IActionCommand
{
    string path;
    string valueExpression;

    public const string Keyword = "SET";
    public bool Blocking => true;
    public int LineNumber {get;}

    public SetCommand(string path, string valueExpression) {
        this.path = path;
        this.valueExpression = valueExpression;
    }

    public SetCommand(CommandLineParser lineParser) {
        LineNumber = lineParser.lineNumber;
        this.path = lineParser.GetString(1);
        this.valueExpression = lineParser.GetAllStrings(2);
    }

    public IEnumerator GetCoroutine() {
        Session.Instance.Set(path, Expression.Eval(valueExpression));
        yield return null; 
    }
}
