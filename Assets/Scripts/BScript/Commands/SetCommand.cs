using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetCommand : IScriptCommand
{
    string path;
    string value;

    public const string Keyword = "SET";
    public bool Blocking => true;
    public int LineNumber {get;}

    public SetCommand(string path, string value) {
        this.path = path;
        this.value = value;
    }

    public SetCommand(CommandLineParser lineParser) {
        LineNumber = lineParser.lineNumber;
        this.path = lineParser.GetString(1);
        this.value = lineParser.GetString(2);
    }

    public IEnumerator GetCoroutine() {
        object evaluatedValue = Session.Instance.Get(path);
        Session.Instance.Set(path, evaluatedValue);
        yield return null; 
    }
}
