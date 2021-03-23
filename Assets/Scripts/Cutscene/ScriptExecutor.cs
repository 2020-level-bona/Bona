using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptExecutor : MonoBehaviour
{
    public void Run(IScriptSession session) {
        StartCoroutine(RunSession(session));
    }

    IEnumerator RunSession(IScriptSession session) {
        Queue<IScriptCommand> commands = session.GetCommands();
        session.linePointers = new List<LinePointer>();

        LinePointer linePointer = new LinePointer(0);
        while (commands.Count > 0) {
            IScriptCommand command = commands.Dequeue();
            linePointer.Set(command.LineNumber);
            if (command.Blocking)
                yield return command.GetCoroutine();
            else
                StartCoroutine(command.GetCoroutine());
        }
        session.linePointers.Remove(linePointer);
        session.MakeExpire();
    }
}

[System.Serializable]
public class LinePointer {
    public int line;

    public LinePointer(int line) {
        this.line = line;
    }

    public void Set(int line) {
        this.line = line;
    }
}
