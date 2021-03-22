using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCommand : IScriptCommand
{
    string movableName;
    Level level;
    Vector2 target;
    bool block;

    public bool Blocking => block;

    public MoveCommand(Level level, string movableName, Vector2 target, bool block) {
        this.level = level;
        this.movableName = movableName;
        this.target = target;
        this.block = block;
    }

    public IEnumerator GetCoroutine() {
        Movable movable = level.GetMovable(movableName);
        if (!movable)
            throw new System.Exception($"Movable[name={movableName}]이 존재하지 않습니다.");
        ITweenEntry tween = Tween.Add(movable, target, 5f);
        yield return new WaitUntil(() => tween.HasDone());
    }
}
