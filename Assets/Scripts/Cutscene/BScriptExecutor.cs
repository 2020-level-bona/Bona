using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class BScriptExecutor : MonoBehaviour
{
    public string script = "";

    Game game;
    Level level;
    ChatManager chatManager;
    BSInterpreter interpreter;
    public ScriptSession session {get; private set;}
    
    public ScriptExecutorState state {get; private set;}

    void OnValidate() {
        game = FindObjectOfType<Game>();
        level = FindObjectOfType<Level>();
        chatManager = FindObjectOfType<ChatManager>();

        interpreter = new BSInterpreter(game, level, chatManager, script);
        if (interpreter.GetSyntaxErrors().Count > 0)
            state = ScriptExecutorState.SYNTAX_ERROR;
        else
            state = ScriptExecutorState.READY;
    }

    void Awake() {
        game = FindObjectOfType<Game>();
        level = FindObjectOfType<Level>();
        chatManager = FindObjectOfType<ChatManager>();
    }

    void Start() {
        interpreter = new BSInterpreter(game, level, chatManager, script);
        if (interpreter.GetSyntaxErrors().Count > 0)
            state = ScriptExecutorState.SYNTAX_ERROR;
        else
            state = ScriptExecutorState.READY;
    }

    public void Run() {
        if (state == ScriptExecutorState.SYNTAX_ERROR)
            throw new System.Exception("스크립트에 구문 오류가 있어 실행할 수 없습니다.");
        if (state == ScriptExecutorState.RUNNING)
            throw new System.Exception("스크립트가 이미 실행 중입니다.");
        
        session = game.CreateScriptSession(new BSInterpreter(game, level, chatManager, script));
        session.Start();

        state = ScriptExecutorState.RUNNING;
    }

    public bool CanRun() {
        return state != ScriptExecutorState.SYNTAX_ERROR && state != ScriptExecutorState.RUNNING;
    }

    void Update() {
        if (session != null) {
            if (session.expired) {
                state = ScriptExecutorState.READY;
                session = null;
            }
        }
        EditorUtility.SetDirty(this); // 인스펙터 Refresh
    }

    public List<Token> GetTokens() {
        if (interpreter == null)
            return new List<Token>();
        return interpreter.tokens;
    }

    public List<BSException> GetExceptions() {
        if (interpreter == null)
            return new List<BSException>();
        return interpreter.GetSyntaxErrors();
    }

    public List<LinePointer> GetLinePointers() {
        if (session == null)
            return new List<LinePointer>();
        return session.linePointers;
    }
}

public enum ScriptExecutorState {
    SYNTAX_ERROR, READY, RUNNING, FAILED
}
