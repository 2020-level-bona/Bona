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

    int subroutineCount;
    int subroutineFinishedCount;

    public ScriptSession(Level level, ChatManager chatManager, ICommandProvider commandProvider, MonoBehaviour coroutineRunner) {
        this.level = level;
        this.chatManager = chatManager;
        this.commandProvider = commandProvider;
        this.coroutineRunner = coroutineRunner;
    }

    public void Start() {
        coroutineRunner.StartCoroutine(MainRoutine());
    }

    IEnumerator MainRoutine() {
        linePointers = new List<LinePointer>();

        subroutineCount = subroutineFinishedCount = 0;

        yield return SubRoutine(new LinePointer(0), commandProvider);
        yield return new WaitWhile(() => subroutineCount > subroutineFinishedCount);

        expired = true;
    }

    IEnumerator SubRoutine(LinePointer linePointer, ICommandProvider commandProvider) {
        subroutineCount++;

        linePointers.Add(linePointer);

        ICommand command = commandProvider.Next();
        while (command != null) {
            linePointer.Move(command.LineNumber);

            if (command is IActionCommand) {
                IEnumerator routine = (command as IActionCommand).GetCoroutine();

                if (command.Blocking)
                    yield return routine;
                else
                    coroutineRunner.StartCoroutine(routine);
            } else if (command is IControlCommand) {
                ICommandProvider sub = (command as IControlCommand).GetCommandProvider();
                if (sub != null) {
                    if (command.Blocking)
                        yield return SubRoutine(new LinePointer(command.LineNumber), sub);
                    else
                        coroutineRunner.StartCoroutine(SubRoutine(new LinePointer(command.LineNumber), sub));
                }
            }
            
            command = commandProvider.Next();
        }

        linePointers.Remove(linePointer);

        subroutineFinishedCount++;
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
