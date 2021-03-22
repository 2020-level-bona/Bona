using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaitCommand : IScriptCommand
{
    float seconds;

    public string Keyword => "WAIT";
    public bool Blocking => true;

    public WaitCommand(float seconds) {
        this.seconds = seconds;
    }

    public IEnumerator GetCoroutine() {
        yield return new WaitForSeconds(seconds);
    }
}
