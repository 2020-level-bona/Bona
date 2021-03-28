using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Trigger : MonoBehaviour
{
    public bool runAllScripts = true;
    public bool runInCutscene = false;

    protected UnityEvent Event = new UnityEvent();

    public void AddListener(UnityAction action) {
        Event.AddListener(action);
    }

    protected void Invoke() {
        if (!runInCutscene && ScriptSession.IsPlayingCutscene)
            return;

        if (runAllScripts) {
            foreach (BScriptExecutor executor in GetComponents<BScriptExecutor>())
                executor.Run();
        }
        Event.Invoke();
    }
}
