using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkAnimationController : IAnimationController

{
    // FIXME : 하드코딩
    const string IDLE = "기본(좌)";
    const string LEFT = "걷기(좌)";
    const string UP = "걷기(위)";
    const string DOWN = "걷기(아래)";

    Movable movable;

    public WalkAnimationController(Movable movable) {
        this.movable = movable;
    }

    public string GetClip() {
        float x = movable.velocity.x;
        float y = movable.velocity.y;

        if (Mathf.Abs(x) <= 0.1f && Mathf.Abs(y) <= 0.1f) {
            return IDLE;
        } else if (Mathf.Abs(x) >= Mathf.Abs(y) - 0.01f) {
            return LEFT;
        } else {
            if (y > 0) {
                return UP;
            } else {
                return DOWN;
            }
        }
    }

    public bool HasDone() {
        return false;
    }
}
