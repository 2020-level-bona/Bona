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
        while (commands.Count > 0) {
            IScriptCommand command = commands.Dequeue();
            if (command.Blocking)
                yield return command.GetCoroutine();
            else
                StartCoroutine(command.GetCoroutine());
        }
        session.MakeExpire();
    }
}
