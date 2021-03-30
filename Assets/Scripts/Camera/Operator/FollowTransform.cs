using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowTransform : Follower
{
    Transform transform;

    public FollowTransform(Transform transform, CameraController cameraController, float maxSpeed) : base(cameraController, maxSpeed) {
        this.transform = transform;
    }

    protected override Vector2 GetCurrentTarget() {
        return cameraController.ClampCameraPosition(transform.position);
    }
}
