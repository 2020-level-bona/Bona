using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Follower : ICameraOperator
{
    public CameraOperatorType type {
        get => CameraOperatorType.REPLACE;
    }

    protected CameraController cameraController;

    Vector2 cameraVelocity = Vector2.zero;
    const float cameraSmoothTime = 0.2f;

    Vector2 position;

    public Follower(CameraController cameraController) {
        this.cameraController = cameraController;
    }

    protected abstract Vector2 GetCurrentTarget();

    public Vector2 GetCameraPosition() {
        return position;
    }

    public void Update() {
        position = Vector2.SmoothDamp(position, GetCurrentTarget(), ref cameraVelocity, cameraSmoothTime);
    }

    public void ResetState() {
        position = GetCurrentTarget();
        cameraVelocity = Vector2.zero;
    }

    public bool HasDone() {
        return false;
    }
}
