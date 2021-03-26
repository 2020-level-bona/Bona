using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingleAnimationController : IAnimationController
{
    float playUntil;
    string clipName;

    public SingleAnimationController(string clipName, float duration = -1) {
        this.clipName = clipName;
        if (duration < 0)
            playUntil = float.PositiveInfinity;
        else
            playUntil = Time.time + duration;
    }

    public string GetClip() {
        return clipName;
    }

    public bool HasDone() {
        return Time.time >= playUntil;
    }
}
