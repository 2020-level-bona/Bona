using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Movable))]
public class WalkAnimationController : MonoBehaviour, IAnimationController
{
    Movable movable;
    public string idleClip;
    public string leftClip, upClip, downClip;

    void Awake() {
        movable = GetComponent<Movable>();
    }

    public string GetClip() {
        float x = movable.velocity.x;
        float y = movable.velocity.y;

        if (Mathf.Abs(x) <= 0.1f && Mathf.Abs(y) <= 0.1f) {
            return idleClip;
        } else if (Mathf.Abs(x) >= Mathf.Abs(y)) {
            return leftClip;
        } else {
            if (y > 0) {
                return upClip;
            } else {
                return downClip;
            }
        }
    }
}
