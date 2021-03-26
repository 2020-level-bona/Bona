using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraAddTargetCommand : IActionCommand
{
    Level level;
    CharacterType characterType;

    public const string Keyword = "CAMADD";
    public bool Blocking => false;
    public int LineNumber {get;}

    public CameraAddTargetCommand(Level level, CharacterType characterType) {
        this.level = level;
        this.characterType = characterType;
    }

    public CameraAddTargetCommand(Level level, CommandLineParser lineParser) {
        LineNumber = lineParser.lineNumber;
        this.level = level;
        characterType = lineParser.GetCharacterType(1);
    }

    public IEnumerator GetCoroutine() {
        CameraController cameraController = GameObject.FindObjectOfType<CameraController>();
        Character character = level.GetSpawnedCharacter(characterType);
        if (cameraController && character) {
            FollowCharacters op = cameraController.FindCameraOperator<FollowCharacters>() as FollowCharacters;
            if (op == null) {
                op = new FollowCharacters(cameraController, CameraController.DEFAULT_SPEED, character);
                cameraController.AddCameraOperator(op);
            } else {
                op.AddTarget(character);
            }
        }
        yield return null;
    }
}
