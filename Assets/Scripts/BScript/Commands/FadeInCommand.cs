using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeInCommand : IActionCommand
{
    float duration;

    public const string Keyword = "FADEIN";
    public bool Blocking {get;}
    public int LineNumber {get;}

    public FadeInCommand(float duration, bool block) {
        this.duration = duration;
        this.Blocking = block;
    }

    public FadeInCommand(CommandLineParser lineParser) {
        LineNumber = lineParser.lineNumber;
        this.duration = lineParser.GetFloat(1);
        Blocking = lineParser.ContainsFlag("WAIT");
    }

    public IEnumerator GetCoroutine() {
        CameraFader cameraFader = GameObject.FindObjectOfType<CameraFader>();
        if (cameraFader) {
            ITweenEntry tween = cameraFader.FadeIn();
            yield return new WaitUntil(() => tween.HasDone());
        }
    }
}
