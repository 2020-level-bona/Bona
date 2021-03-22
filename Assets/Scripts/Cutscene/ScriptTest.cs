using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptTest : MonoBehaviour
{
    public BScriptString script;

    void OnValidate() {
        Level level = FindObjectOfType<Level>();
        ChatManager chatManager = FindObjectOfType<ChatManager>();

        BSInterpreter interpreter = new BSInterpreter(level, chatManager, script.code);

        List<BSExceptionAsSerializedProperty> exceptions = new List<BSExceptionAsSerializedProperty>();
        foreach (BSException exception in interpreter.GetSyntaxErrors()) {
            exceptions.Add(exception);
        }

        script.exceptions = exceptions;
        script.exceptionCount = exceptions.Count;
    }

    void Start() {
        Level level = FindObjectOfType<Level>();
        ChatManager chatManager = FindObjectOfType<ChatManager>();
        FindObjectOfType<ScriptExecutor>().Run(new BSInterpreter(level, chatManager, script.code), script);
    }
}
