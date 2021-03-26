using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraDeleteTargetCommand : IActionCommand
{
    Level level;
    CharacterType characterType;

    public const string Keyword = "CAMDEL";
    public bool Blocking => false;
    public int LineNumber {get;}

    public CameraDeleteTargetCommand(Level level, CharacterType characterType) {
        this.level = level;
        this.characterType = characterType;
    }

    public CameraDeleteTargetCommand(Level level, CommandLineParser lineParser) {
        LineNumber = lineParser.lineNumber;
        this.level = level;
        characterType = lineParser.GetCharacterType(1);
    }

    public IEnumerator GetCoroutine() {
        CameraController cameraController = GameObject.FindObjectOfType<CameraController>();
        Character character = level.GetSpawnedCharacter(characterType);
        if (cameraController && character) {
            FollowCharacters op = cameraController.FindCameraOperator<FollowCharacters>() as FollowCharacters;
            if (op != null)
                op.RemoveTarget(character);
        }
        yield return null;
    }
}
