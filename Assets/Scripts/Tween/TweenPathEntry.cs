using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TweenPathEntry : ITweenEntry
{
    public Movable movable;
    public Path path;
    public float t;
    public float speed;
    public Action finishCallback;

    public TweenPathEntry(Movable movable, Path path, float speed, Action finishCallback) {
        this.movable = movable;
        this.path = path;
        this.speed = speed;
        this.finishCallback = finishCallback;

        t = 0;
    }

    public bool HasDone() {
        if (!movable)
            return true;
        
        if (t >= path.Solver.GetMaxT()) {
            if (finishCallback != null)
                finishCallback();
            
            return true;
        }

        return false;
    }

    public void Update() {
        t = path.Solver.GetNextT(t, speed * Time.deltaTime);
        movable.MoveTo(path.Solver.CalcPosition(t));
    }
}
