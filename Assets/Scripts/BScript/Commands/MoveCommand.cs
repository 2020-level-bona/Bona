﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCommand : IActionCommand
{
    string movableName;
    Level level;
    Vector2Face target;

    public const string Keyword = "MOVE";
    public bool Blocking {get;}
    public int LineNumber {get;}

    public MoveCommand(Level level, string movableName, Vector2Face target, bool block) {
        this.level = level;
        this.movableName = movableName;
        this.target = target;
        Blocking = block;
    }

    public MoveCommand(Level level, CommandLineParser lineParser) {
        LineNumber = lineParser.lineNumber;
        this.level = level;
        this.movableName = lineParser.GetString(1);
        this.target = lineParser.GetVector2Face(level, 2);
        Blocking = lineParser.ContainsFlag("WAIT");
    }

    public IEnumerator GetCoroutine() {
        Movable movable = level.GetCharacterMovableOrMovable(movableName);
        if (!movable)
            throw new BSMovableNotFoundException(LineNumber, $"Movable[name={movableName}]이 존재하지 않습니다.");
        ITweenEntry tween = Tween.Add(movable, target.position, movable.speed);
        yield return new WaitUntil(() => tween.HasDone());

        movable.SetFace(target.face);
    }
}
