using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowTransform : ICameraOperator
{
    public CameraOperatorType type {
        get => CameraOperatorType.REPLACE;
    }

    Transform transform;
    CameraController cameraController;

    Vector2 cameraVelocity = Vector2.zero;
    const float cameraSmoothTime = 0.2f;

    Vector2 position;

    public FollowTransform(Transform transform, CameraController cameraController) {
        this.transform = transform;
        this.cameraController = cameraController;

        position = GetCurrentTarget();
    }

    Vector2 GetCurrentTarget() {
        return cameraController.ClampCameraPosition(transform.position);
    }

    public Vector2 GetCameraPosition() {
        return position;
    }

    public void Update() {
        position = Vector2.SmoothDamp(position, GetCurrentTarget(), ref cameraVelocity, cameraSmoothTime);
    }

    public void ResetState() {
        position = transform.position;
        cameraVelocity = Vector2.zero;
    }

    public bool HasDone() {
        return false;
    }
}
