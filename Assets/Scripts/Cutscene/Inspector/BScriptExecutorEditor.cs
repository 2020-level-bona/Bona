using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(BScriptExecutor))]
public class BScriptExecutorEditor : Editor
{
    const int MAX_WIDTH = 500;

    public override void OnInspectorGUI() {
        serializedObject.Update();
        // EditorGUILayout.BeginVertical();

        BScriptExecutor executor = (BScriptExecutor) target;

        DrawProperties(executor);

        DrawStatus(executor.state);

        Vector2 textAreaSize = GetTextAreaStyle().CalcSize(new GUIContent(executor.script));
        Rect position = GUILayoutUtility.GetRect(MAX_WIDTH, textAreaSize.y);
        Rect original = position;

        List<Token> tokens = executor.GetTokens();
        // 토큰을 등장 순서에 따라 정렬
        tokens.Sort((a, b) => {
            if (a.lineNumber == b.lineNumber)
                return a.startIndex.CompareTo(b.startIndex);
            return a.lineNumber.CompareTo(b.lineNumber);
        });

        List<BSException> exceptions = executor.GetExceptions();

        if (tokens.Count > 0)
            DrawBackground(position, tokens[tokens.Count - 1].lineNumber, exceptions);
        DrawLinePointer(position, executor.GetLinePointers());

        serializedObject.FindProperty("script").stringValue = EditorGUI.TextArea(position, serializedObject.FindProperty("script").stringValue, GetTextAreaStyle());

        float lineHeight = GetTextAreaStyle().lineHeight;
        float exceptionLineHeight = GetExceptionTextStyle().lineHeight;
        float spaceWidth = GetTextAreaStyle().CalcSize(new GUIContent(" ")).x;

        int line = 0;
        int column = 0;
        for (int i = 0; i < tokens.Count; i++) {
            if (line != tokens[i].lineNumber) {
                position.x = original.x;
                position.y += lineHeight * (tokens[i].lineNumber - line);

                line = tokens[i].lineNumber;
                column = 0;
            }

            // 토큰 간 공백 채움
            position.x += spaceWidth * (tokens[i].startIndex - column);

            DrawText(position, tokens[i].str, BScriptColorTheme.GetColor(tokens[i].type));
            position.x += GetTextAreaStyle().CalcSize(new GUIContent(tokens[i].str)).x;

            column = tokens[i].startIndex + tokens[i].str.Length;
        }

        DrawExceptions(exceptions);

        // EditorGUILayout.EndVertical();
        serializedObject.ApplyModifiedProperties();
    }

    void DrawProperties(BScriptExecutor executor) {
        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("고유 ID");
        executor.uniqueId = EditorGUILayout.TextField(executor.uniqueId);
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("스크립트 실행 타입");
        executor.executionType = (ScriptExecutionType) EditorGUILayout.EnumPopup(executor.executionType);
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("스크립트 실행 조건식");
        executor.executionCondition = EditorGUILayout.TextField(executor.executionCondition);
        EditorGUILayout.EndHorizontal();
    }

    void DrawStatus(ScriptExecutorState state) {
        Rect rect = GUILayoutUtility.GetRect(new GUIContent(GetStatusText(state)), GetStatusStyle());

        GUI.Label(new Rect(rect.x, rect.y, 20, 20), GetStatusTexture(state));
        GUI.Label(new Rect(rect.x + 20, rect.y, 200, 20), GetStatusText(state), GetStatusStyle());
    }

    void DrawBackground(Rect position, int lines, List<BSException> exceptions) {
        float lineHeight = GetTextAreaStyle().lineHeight;
        float exceptionLineHeight = GetExceptionTextStyle().lineHeight;
        float totalHeight = 0;

        for (int i = 0; i <= lines; i++) {
            float currentHeight = lineHeight;
            EditorGUI.DrawRect(new Rect(position.x, position.y + totalHeight, MAX_WIDTH, currentHeight),
                // (i % 2 == 0) ? new Color(0.23f, 0.23f, 0.23f) : new Color(0.2f, 0.2f, 0.2f));
                (i % 2 == 0) ? new Color32(44, 43, 45, 255) : new Color32(35, 34, 36, 255));
            
            totalHeight += currentHeight;
        }
    }

    void DrawText(Rect position, string text, Color color) {
        GUIStyle style = GetTextStyle(color);
        EditorGUI.LabelField(position, text, style);
        position.x += style.CalcSize(new GUIContent(text)).x;
    }

    void DrawExceptions(List<BSException> exceptions) {
        GUIStyle style = GetExceptionTextStyle();

        Rect rect = GUILayoutUtility.GetRect(MAX_WIDTH, style.lineHeight * exceptions.Count);

        for (int i = 0; i < exceptions.Count; i++) {
            EditorGUI.LabelField(new Rect(rect.x, rect.y + i * style.lineHeight, rect.width, style.lineHeight), $"라인 {exceptions[i].line + 1}: {exceptions[i].Message}", style);
        }
    }

    void DrawLinePointer(Rect position, List<LinePointer> linePointers) {
        float lineHeight = GetTextAreaStyle().lineHeight;
        foreach (LinePointer line in linePointers) {
            EditorGUI.DrawRect(new Rect(position.x, position.y + lineHeight * line.line, position.width, lineHeight), new Color(0.168f, 0.7f, 0.94f, 0.7f));
        }
    }

    Texture2D GetStatusTexture(ScriptExecutorState state) {
        switch (state) {
            case ScriptExecutorState.READY:
                return EditorGUIUtility.IconContent("d_winbtn_mac_min").image as Texture2D;
            case ScriptExecutorState.RUNNING:
                return EditorGUIUtility.IconContent("d_winbtn_mac_max").image as Texture2D;
        }
        return EditorGUIUtility.IconContent("d_winbtn_mac_close").image as Texture2D;
    }

    string GetStatusText(ScriptExecutorState state) {
        switch (state) {
            case ScriptExecutorState.READY:
                return "실행 대기중";
            case ScriptExecutorState.RUNNING:
                return "실행 중";
            case ScriptExecutorState.SYNTAX_ERROR:
                return "구문 오류";
            case ScriptExecutorState.FAILED:
                return "런타임 에러";
        }
        return "UNKNOWN";
    }

    GUIStyle GetStatusStyle() {
        GUIStyle style = new GUIStyle();
        style.normal.textColor = Color.white;
        style.fontSize = 12;
        style.stretchWidth = false;
        style.stretchHeight = false;
        style.padding = new RectOffset(2, 2, 2, 2);
        style.fontStyle = UnityEngine.FontStyle.Bold;
        return style;
    }

    GUIStyle GetTextAreaStyle() {
        return GetTextStyle(new Color(0, 0, 0, 0));
    }

    GUIStyle GetTextStyle(Color color) {
        GUIStyle style = new GUIStyle();
        style.normal.textColor = color;
        style.fontSize = 15;
        style.stretchWidth = false;
        style.stretchHeight = false;
        style.fontStyle = UnityEngine.FontStyle.Bold;
        return style;
    }

    GUIStyle GetExceptionTextStyle() {
        GUIStyle style = new GUIStyle();
        style.normal.textColor = Color.red;
        style.fontSize = 12;
        style.fontStyle = UnityEngine.FontStyle.Bold;
        return style;
    }
}
