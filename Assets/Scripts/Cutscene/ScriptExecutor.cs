using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptExecutor : MonoBehaviour
{
    public void Run(IScriptSession session, BScriptString scriptString = null) {
        StartCoroutine(RunSession(session, scriptString));
    }

    IEnumerator RunSession(IScriptSession session, BScriptString scriptString) {
        Queue<IScriptCommand> commands = session.GetCommands();
        while (commands.Count > 0) {
            IScriptCommand command = commands.Dequeue();
            if (scriptString != null)
                scriptString.linePointer = command.LineNumber;
            if (command.Blocking)
                yield return command.GetCoroutine();
            else
                StartCoroutine(command.GetCoroutine());
        }
        if (scriptString != null)
            scriptString.linePointer = -1;
        session.MakeExpire();
    }
}
