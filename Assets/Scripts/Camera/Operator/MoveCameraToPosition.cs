using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCameraToPosition : Follower
{
    Vector2 target;

    public MoveCameraToPosition(Vector2 target, CameraController cameraController, float maxSpeed) : base(cameraController, maxSpeed) {
        this.target = target;
    }

    protected override Vector2 GetCurrentTarget()
    {
        return target;
    }

    public override bool HasDone()
    {
        return Vector2.Distance(cameraController.position, target) < 0.01f;
    }
}
