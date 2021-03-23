using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptSession
{
    Level level;
    ChatManager chatManager;
    ICommandProvider commandProvider;
    MonoBehaviour coroutineRunner;
    public bool expired {get; private set;} = false;
    public List<LinePointer> linePointers {get; private set;} = new List<LinePointer>();

    public ScriptSession(Level level, ChatManager chatManager, ICommandProvider commandProvider, MonoBehaviour coroutineRunner) {
        this.level = level;
        this.chatManager = chatManager;
        this.commandProvider = commandProvider;
        this.coroutineRunner = coroutineRunner;
    }

    public void Start() {
        coroutineRunner.StartCoroutine(MainCoroutine());
    }

    IEnumerator MainCoroutine() {
        linePointers = new List<LinePointer>();

        LinePointer linePointer = new LinePointer(0);
        linePointers.Add(linePointer);

        IScriptCommand command = commandProvider.Next();
        while (command != null) {
            linePointer.Move(command.LineNumber);
            if (command.Blocking)
                yield return command.GetCoroutine();
            else
                coroutineRunner.StartCoroutine(command.GetCoroutine());
            command = commandProvider.Next();
        }
        linePointers.Remove(linePointer);
        expired = true;
    }
}

[System.Serializable]
public class LinePointer {
    public int line;

    public LinePointer(int line) {
        this.line = line;
    }

    public void Move(int line) {
        this.line = line;
    }
}
