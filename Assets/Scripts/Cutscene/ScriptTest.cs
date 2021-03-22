using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptTest : MonoBehaviour
{
    public BScriptString script;

    void OnValidate() {
        Level level = FindObjectOfType<Level>();
        ChatManager chatManager = FindObjectOfType<ChatManager>();
        new BSInterpreter(level, chatManager, script.code); // Check Syntax
    }

    void Start() {
        Level level = FindObjectOfType<Level>();
        ChatManager chatManager = FindObjectOfType<ChatManager>();
        FindObjectOfType<ScriptExecutor>().Run(new BSInterpreter(level, chatManager, script.code));
    }
}
