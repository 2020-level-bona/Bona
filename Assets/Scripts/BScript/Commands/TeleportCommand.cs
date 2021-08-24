using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportCommand : IActionCommand
{
    string movableName;
    Level level;
    Vector2Face target;

    public const string Keyword = "TELEPORT";
    public bool Blocking => false;
    public int LineNumber {get;}

    public TeleportCommand(Level level, string movableName, Vector2Face target) {
        this.level = level;
        this.movableName = movableName;
        this.target = target;
    }

    public TeleportCommand(Level level, CommandLineParser lineParser) {
        LineNumber = lineParser.lineNumber;
        this.level = level;
        this.movableName = lineParser.GetString(1);
        this.target = lineParser.GetVector2Face(level, 2);
    }

    public IEnumerator GetCoroutine() {
        Movable movable = level.GetCharacterMovableOrMovable(movableName);
        if (!movable)
            throw new BSMovableNotFoundException(LineNumber, $"Movable[name={movableName}]이 존재하지 않습니다.");
        movable.MoveTo(target.position);
        movable.SetFace(target.face);
        yield return null;
    }
}
