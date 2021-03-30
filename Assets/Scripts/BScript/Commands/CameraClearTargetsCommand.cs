using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraClearTargetsCommand : IActionCommand
{
    public const string Keyword = "CAMCLR";
    public bool Blocking => false;
    public int LineNumber {get;}

    public CameraClearTargetsCommand() { }

    public CameraClearTargetsCommand(CommandLineParser lineParser) {
        LineNumber = lineParser.lineNumber;
    }

    public IEnumerator GetCoroutine() {
        CameraController cameraController = GameObject.FindObjectOfType<CameraController>();
        if (cameraController) {
            FollowCharacters op = cameraController.FindCameraOperator<FollowCharacters>() as FollowCharacters;
            if (op != null)
                op.ClearTargets();
        }
        yield return null;
    }
}
