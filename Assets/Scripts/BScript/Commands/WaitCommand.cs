using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaitCommand : IActionCommand
{
    float seconds;

    public const string Keyword = "WAIT";
    public bool Blocking => true;
    public int LineNumber {get;}

    public WaitCommand(float seconds) {
        this.seconds = seconds;
    }

    public WaitCommand(CommandLineParser lineParser) {
        LineNumber = lineParser.lineNumber;
        this.seconds = lineParser.GetFloat(1);
    }

    public IEnumerator GetCoroutine() {
        yield return new WaitForSeconds(seconds);
    }
}
