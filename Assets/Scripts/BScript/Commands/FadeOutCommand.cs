using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeOutCommand : IActionCommand
{
    float duration;

    public const string Keyword = "FADEOUT";
    public bool Blocking {get;}
    public int LineNumber {get;}

    public FadeOutCommand(float duration, bool block) {
        this.duration = duration;
        this.Blocking = block;
    }

    public FadeOutCommand(CommandLineParser lineParser) {
        LineNumber = lineParser.lineNumber;
        this.duration = lineParser.GetFloat(1);
        Blocking = lineParser.ContainsFlag("WAIT");
    }

    public IEnumerator GetCoroutine() {
        CameraFader cameraFader = GameObject.FindObjectOfType<CameraFader>();
        if (cameraFader) {
            ITweenEntry tween = cameraFader.FadeOut(this.duration);
            yield return new WaitUntil(() => tween.HasDone());
        }
    }
}
