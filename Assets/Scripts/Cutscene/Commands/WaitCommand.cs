using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaitCommand : IScriptCommand
{
    float seconds;

    public const string Keyword = "WAIT";
    public bool Blocking => true;

    public WaitCommand(float seconds) {
        this.seconds = seconds;
    }

    public WaitCommand(CommandLineParser lineParser) {
        this.seconds = lineParser.GetFloat(1);
    }

    public IEnumerator GetCoroutine() {
        yield return new WaitForSeconds(seconds);
    }
}
