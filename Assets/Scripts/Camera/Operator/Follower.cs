using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Follower : ICameraOperator
{
    public CameraOperatorType type {
        get => CameraOperatorType.REPLACE;
    }

    protected CameraController cameraController;

    float maxSpeed;
    Vector2 cameraVelocity = Vector2.zero;
    const float cameraSmoothTime = 0.2f;

    Vector2 position;

    public Follower(CameraController cameraController, float maxSpeed) {
        this.cameraController = cameraController;
        this.maxSpeed = maxSpeed;
    }

    protected abstract Vector2 GetCurrentTarget();

    public Vector2 GetCameraPosition() {
        return position;
    }

    public void Update() {
        position = Vector2.SmoothDamp(position, GetCurrentTarget(), ref cameraVelocity, cameraSmoothTime, maxSpeed);
    }

    // 오퍼레이터의 위치를 카메라로 가져간다.
    public void ResetState() {
        position = cameraController.position;
        cameraVelocity = Vector2.zero;
    }

    public virtual bool HasDone() {
        return false;
    }
}
