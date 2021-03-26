using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkAnimationController : IAnimationController

{
    public const string IDLE = "IDLE";
    public const string LEFT = "WALK_L";
    public const string UP = "WALK_U";
    public const string DOWN = "WALK_D";

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
