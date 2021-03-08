using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TweenEntry<T> : ITweenEntry
{
    public UnityEngine.Object owner;
    public Action<T> action;
    public ILerpable<T> range;
    public float start;
    public float duration;

    public TweenEntry(UnityEngine.Object owner, Action<T> action, ILerpable<T> range, float duration) {
        this.owner = owner;
        this.action = action;
        this.range = range;
        this.duration = duration;

        start = Time.time;
    }

    public bool HasDone() {
        if (!owner)
            return true;
        
        if (start + duration <= Time.time) {
            action(range.Lerp(1f));
            return true;
        }

        return false;
    }

    public void Update() {
        action(range.Lerp((Time.time - start) / duration));
    }
}
