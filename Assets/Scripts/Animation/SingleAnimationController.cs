using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingleAnimationController : IAnimationController
{
    float playUntil;
    string stateName;

    public SingleAnimationController(string stateName, float duration = -1) {
        this.stateName = stateName;
        if (duration < 0)
            playUntil = float.PositiveInfinity;
        else
            playUntil = Time.time + duration;
    }

    public string GetState() {
        return stateName;
    }

    public bool HasDone() {
        return Time.time >= playUntil;
    }
}
