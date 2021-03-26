using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMoveCommand : IActionCommand
{
    Level level;
    Vector2Face target;
    Speed speed;

    public const string Keyword = "CAMMOVE";
    public bool Blocking {get;}
    public int LineNumber {get;}

    public CameraMoveCommand(Level level, Vector2 target, bool block, Speed speed) {
        this.level = level;
        this.target = target;
        Blocking = block;
        this.speed = speed;
    }

    public CameraMoveCommand(Level level, CommandLineParser lineParser) {
        LineNumber = lineParser.lineNumber;
        this.level = level;
        this.target = lineParser.GetVector2Face(level, 1);
        speed = lineParser.GetSpeed(2);
        Blocking = lineParser.ContainsFlag("WAIT");
    }

    public IEnumerator GetCoroutine() {
        CameraController cameraController = GameObject.FindObjectOfType<CameraController>();
        if (cameraController) {
            if (speed == Speed.INSTANT) {
                cameraController.MoveInstantly(target);
            } else {
                float scaledSpeed = CameraController.DEFAULT_SPEED;
                switch (speed) {
                    case Speed.FASTEST:
                        scaledSpeed *= 3;
                        break;
                    case Speed.FAST:
                        scaledSpeed *= 3;
                        break;
                    case Speed.SLOW:
                        scaledSpeed *= 0.5f;
                        break;
                    case Speed.SLOWEST:
                        scaledSpeed *= 0.33f;
                        break;
                }
                cameraController.ClearCameraOperators();
                ICameraOperator cameraOperator = new MoveCameraToPosition(target, cameraController, scaledSpeed);
                cameraController.AddCameraOperator(cameraOperator);

                if (Blocking) {
                    yield return new WaitWhile(() => cameraController.ContainsCameraOperator(cameraOperator)
                        && !cameraOperator.HasDone());
                }
            }
        }
        yield return null;
    }
}
