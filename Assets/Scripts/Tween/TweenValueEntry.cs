using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TweenValueEntry<T> : ITweenEntry
{
    public UnityEngine.Object owner;
    public Action<T> action;
    public ILerpable<T> range;
    public float start;
    public float duration;
    public Action finishCallback;

    public TweenValueEntry(UnityEngine.Object owner, Action<T> action, ILerpable<T> range, float duration, Action finishCallback) {
        this.owner = owner;
        this.action = action;
        this.range = range;
        this.duration = Mathf.Max(duration, 1e-5f);
        this.finishCallback = finishCallback;

        start = Time.time;
    }

    public bool HasDone() {
        if (!owner)
            return true;
        
        if (start + duration <= Time.time) {
            action(range.Lerp(1f));

            if (finishCallback != null)
                finishCallback();
            
            return true;
        }

        return false;
    }

    public void Update() {
        action(range.Lerp((Time.time - start) / duration));
    }
}
