using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BScriptExecutor : MonoBehaviour
{
    public BScriptString script;

    Game game;
    Level level;
    ChatManager chatManager;
    BSInterpreter interpreter;
    ScriptSession session;

    void OnValidate() {
        Game game = FindObjectOfType<Game>();
        Level level = FindObjectOfType<Level>();
        ChatManager chatManager = FindObjectOfType<ChatManager>();

        BSInterpreter interpreter = new BSInterpreter(level, chatManager, script.code);

        script.tokens = interpreter.tokens;
        script.tokenCount = interpreter.tokens.Count;

        List<BSExceptionAsSerializedProperty> exceptions = new List<BSExceptionAsSerializedProperty>();
        foreach (BSException exception in interpreter.GetSyntaxErrors()) {
            exceptions.Add(exception);
        }

        script.exceptions = exceptions;
        script.exceptionCount = exceptions.Count;
    }

    void Awake() {
        game = FindObjectOfType<Game>();
        level = FindObjectOfType<Level>();
        chatManager = FindObjectOfType<ChatManager>();
    }

    void Start() {
        interpreter = new BSInterpreter(level, chatManager, script.code);
        session = game.CreateScriptSession(interpreter);
        session.Start();
    }

    void Update() {
        List<int> linePointers = new List<int>();
        foreach (LinePointer linePointer in session.linePointers)
            linePointers.Add(linePointer.line);
        script.linePointers = linePointers;
        script.linePointerCount = linePointers.Count;
    }
}

public enum ScriptExecutorState {
    READY, RUNNING, FAILED
}
