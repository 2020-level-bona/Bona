using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptSession : IScriptSession
{
    Level level;
    ChatQueue chatQueue;

    Queue<IScriptCommand> commands;

    public ScriptSession(Level level, ChatQueue chatQueue) {
        this.level = level;
        this.chatQueue = chatQueue;
    }

    public Queue<IScriptCommand> GetCommands() {
        return commands;
    }

    public void Wait(float seconds) {
        commands.Enqueue(new WaitCommand(seconds));
    }

    public void Msg(CharacterType characterType, string message) {
        commands.Enqueue(new MessageCommand(chatQueue, level, characterType, message));
    }

    public void Move(string movableName, Vector2 target, bool block = false) {
        commands.Enqueue(new MoveCommand(level, movableName, target, block));
    }

    public void Move(string movableName, string markerName, bool block = false) {
        Marker marker = level.GetMarker(markerName);
        if (!marker)
            throw new System.Exception($"Marker[name={markerName}]가 존재하지 않습니다.");
        commands.Enqueue(new MoveCommand(level, movableName, marker.position, block));
    }

    public void Show(CharacterType characterType, Vector2 target) {
        commands.Enqueue(new ShowCommand(level, characterType, target));
    }

    public void Hide(CharacterType characterType) {
        commands.Enqueue(new HideCommand(level, characterType));
    }
}
