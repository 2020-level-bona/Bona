using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Trigger))]
public class PrintMessages : MonoBehaviour
{
    public string[] messages;

    ScriptExecutor scriptExecutor;
    Trigger trigger;

    void Awake() {
        scriptExecutor = FindObjectOfType<ScriptExecutor>();

        trigger = GetComponent<Trigger>();
    }

    void Start() {
        trigger.AddListener(Chat);
    }

    void Chat() {
        ScriptSession session = new ScriptSession(FindObjectOfType<Level>(), FindObjectOfType<ChatManager>());
        foreach (string message in messages)
            session.Msg(CharacterType.BONA, message);
        scriptExecutor.Run(session);
    }
}
