using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : ICameraOperator
{
    public CameraOperatorType type {
        get => CameraOperatorType.REPLACE;
    }

    Player player;
    CameraController cameraController;

    Vector2 cameraVelocity = Vector2.zero;
    const float cameraSmoothTime = 0.2f;

    Vector2 position;

    public FollowPlayer(Player player, CameraController cameraController) {
        this.player = player;
        this.cameraController = cameraController;

        position = GetCurrentTarget();
    }

    Vector2 GetCurrentTarget() {
        return cameraController.ClampCameraPosition(player.transform.position);
    }

    public Vector2 GetCameraPosition() {
        return position;
    }

    public void Update() {
        position = Vector2.SmoothDamp(position, GetCurrentTarget(), ref cameraVelocity, cameraSmoothTime);
    }

    public void ResetState() {
        position = player.transform.position;
        cameraVelocity = Vector2.zero;
    }

    public bool HasDone() {
        return false;
    }
}
