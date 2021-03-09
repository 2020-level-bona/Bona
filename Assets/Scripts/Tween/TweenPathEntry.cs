using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TweenPathEntry : ITweenEntry
{
    public Transform transform;
    public Path path;
    public float t;
    public float speed;
    public Action finishCallback;

    public TweenPathEntry(Transform transform, Path path, float speed, Action finishCallback) {
        this.transform = transform;
        this.path = path;
        this.speed = speed;
        this.finishCallback = finishCallback;

        t = 0;
    }

    public bool HasDone() {
        if (!transform)
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
        ApplyPosition(path.Solver.CalcPosition(t));
    }

    void ApplyPosition(Vector2 position) {
        transform.position = new Vector3(position.x, position.y, transform.position.z);
    }
}
