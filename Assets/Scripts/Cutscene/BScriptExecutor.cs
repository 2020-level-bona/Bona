using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BScriptExecutor : MonoBehaviour
{
    public BScriptString script;

    Level level;
    ChatManager chatManager;
    BSInterpreter interpreter;

    void OnValidate() {
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
        level = FindObjectOfType<Level>();
        chatManager = FindObjectOfType<ChatManager>();
    }

    void Start() {
        Run();
    }

    void Update() {
        List<int> linePointers = new List<int>();
        foreach (LinePointer linePointer in interpreter.linePointers)
            linePointers.Add(linePointer.line);
        script.linePointers = linePointers;
        script.linePointerCount = linePointers.Count;
    }

    public void Run() {
        interpreter = new BSInterpreter(level, chatManager, script.code);
        StartCoroutine(RunSession(interpreter));
    }

    IEnumerator RunSession(IScriptSession session) {
        Queue<IScriptCommand> commands = session.GetCommands();
        session.linePointers = new List<LinePointer>();

        LinePointer linePointer = new LinePointer(0);
        session.linePointers.Add(linePointer);
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

public enum ScriptExecutorState {
    READY, RUNNING, FAILED
}
