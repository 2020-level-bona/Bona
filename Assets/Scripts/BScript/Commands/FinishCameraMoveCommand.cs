using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishCameraMoveCommand : IActionCommand
{
    Level level;

    public const string Keyword = "CAMFINISH";
    public bool Blocking => false;
    public int LineNumber {get;}

    public FinishCameraMoveCommand(Level level) {
        this.level = level;
    }

    public FinishCameraMoveCommand(Level level, CommandLineParser lineParser) {
        LineNumber = lineParser.lineNumber;
        this.level = level;
    }

    public IEnumerator GetCoroutine() {
        CameraController cameraController = GameObject.FindObjectOfType<CameraController>();
        if (cameraController) {
            foreach (ICameraOperator cameraOperator in cameraController.GetCameraOperators()) {
                cameraOperator.ForceFinish();
            }
        }
        yield return null;
    }
}
