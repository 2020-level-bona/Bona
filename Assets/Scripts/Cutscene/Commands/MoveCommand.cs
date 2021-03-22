using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCommand : IScriptCommand
{
    string movableName;
    Level level;
    Vector2Face target;
    bool block;

    public string Keyword => "MOVE";
    public bool Blocking => block;

    public MoveCommand(Level level, string movableName, Vector2Face target, bool block) {
        this.level = level;
        this.movableName = movableName;
        this.target = target;
        this.block = block;
    }

    public MoveCommand(Level level, CommandLineParser lineParser) {
        this.level = level;
        this.movableName = lineParser.GetString(1);
        this.target = lineParser.GetVector2Face(level, 2);
    }

    public IEnumerator GetCoroutine() {
        Movable movable = level.GetMovable(movableName);
        if (!movable)
            throw new BSMovableNotFoundException($"Movable[name={movableName}]이 존재하지 않습니다.");
        ITweenEntry tween = Tween.Add(movable, target.position, 5f);
        yield return new WaitUntil(() => tween.HasDone());
        // TODO: movable이 방향을 향하도록
    }
}
