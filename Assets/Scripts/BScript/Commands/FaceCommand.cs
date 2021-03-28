using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FaceCommand : IActionCommand
{
    string movableName;
    Level level;
    Face face;

    public const string Keyword = "FACE";
    public bool Blocking => false;
    public int LineNumber {get;}

    public FaceCommand(Level level, string movableName, Face face) {
        this.level = level;
        this.movableName = movableName;
        this.face = face;
    }

    public FaceCommand(Level level, CommandLineParser lineParser) {
        LineNumber = lineParser.lineNumber;
        this.level = level;
        this.movableName = lineParser.GetString(1);
        this.face = lineParser.GetFace(2);
    }

    public IEnumerator GetCoroutine() {
        Movable movable = level.GetCharacterMovableOrMovable(movableName);
        if (!movable)
            throw new BSMovableNotFoundException(LineNumber, $"Movable[name={movableName}]이 존재하지 않습니다.");
        movable.SetFace(face);
        yield return null;
    }
}
