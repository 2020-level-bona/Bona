using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomPropertyDrawer(typeof(BScriptString))]
public class BScriptStringDrawer : PropertyDrawer
{
    // 절대 EditorGUILayout, GUILayoutUtility 쓰지말고 EditorGUI만 쓰자. 유니티 버그있음.
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        Rect original = position;
        // Move this up
        // EditorGUI.BeginProperty(new Rect(position.x, position.y, position.width, 1000), label, property);

        SerializedProperty codeProperty = property.FindPropertyRelative("code");
        string prevString = codeProperty.stringValue;

        List<Token> tokens = GetTokens(property);
        // 토큰을 등장 순서에 따라 정렬
        tokens.Sort((a, b) => {
            if (a.lineNumber == b.lineNumber)
                return a.startIndex.CompareTo(b.startIndex);
            return a.lineNumber.CompareTo(b.lineNumber);
        });

        List<BSException> exceptions = GetExceptions(property);

        DrawBackground(position, tokens[tokens.Count - 1].lineNumber, exceptions);
        DrawLinePointer(original, property.FindPropertyRelative("linePointer").intValue);

        codeProperty.stringValue = EditorGUI.TextArea(position, prevString, GetTextAreaStyle());

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
        DrawExceptions(new Rect(original.x, position.y + lineHeight, position.width, position.height), exceptions);
        // EditorGUI.EndProperty();
    }

    void DrawBackground(Rect position, int lines, List<BSException> exceptions) {
        float lineHeight = GetTextAreaStyle().lineHeight;
        float exceptionLineHeight = GetExceptionTextStyle().lineHeight;
        float totalHeight = 0;

        for (int i = 0; i <= lines; i++) {
            float currentHeight = lineHeight;
            EditorGUI.DrawRect(new Rect(position.x, position.y + totalHeight, position.width, currentHeight),
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

    void DrawExceptions(Rect position, List<BSException> exceptions) {
        GUIStyle style = GetExceptionTextStyle();
        for (int i = 0; i < exceptions.Count; i++) {
            EditorGUI.LabelField(new Rect(position.x, position.y + i * style.lineHeight, position.width, position.height), $"라인 {exceptions[i].line + 1}: {exceptions[i].Message}", style);
        }
    }

    void DrawLinePointer(Rect position, int line) {
        if (line < 0)
            return;
        float lineHeight = GetTextAreaStyle().lineHeight;
        EditorGUI.DrawRect(new Rect(position.x, position.y + lineHeight * line, position.width, lineHeight), new Color(0.168f, 0.7f, 0.94f, 0.7f));
    }

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label) {
        float totalHeight = 0;
        totalHeight += GetTextAreaStyle().CalcHeight(new GUIContent(property.FindPropertyRelative("code").stringValue), 10); // Text Wrapping을 하지 않으므로 width는 의미없다.
        totalHeight += GetExceptionTextStyle().lineHeight * GetExceptions(property).Count;
        return totalHeight;
    }

    GUIStyle GetTextAreaStyle() {
        return GetTextStyle(new Color(0, 0, 0, 0));
    }

    GUIStyle GetTextStyle(Color color) {
        GUIStyle style = new GUIStyle();
        style.normal.textColor = color;
        style.fontSize = 18;
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

    List<Token> GetTokens(SerializedProperty property) {
        SerializedProperty list = property.FindPropertyRelative("tokens");
        int count = property.FindPropertyRelative("tokenCount").intValue;

        List<Token> tokens = new List<Token>();
        for (int i = 0; i < count; i++) {
            tokens.Add(list.GetArrayElementAtIndex(i));
        }
        return tokens;
    }

    List<BSException> GetExceptions(SerializedProperty property) {
        SerializedProperty list = property.FindPropertyRelative("exceptions");
        int count = property.FindPropertyRelative("exceptionCount").intValue;

        List<BSException> exceptions = new List<BSException>();
        for (int i = 0; i < count; i++) {
            SerializedProperty ex = list.GetArrayElementAtIndex(i);
            exceptions.Add(new BSException(ex.FindPropertyRelative("line").intValue, ex.FindPropertyRelative("message").stringValue));
        }
        return exceptions;
    }

    List<BSException> GetExceptionsOnLine(List<BSException> exceptions, int line) {
        return exceptions.FindAll(x => x.line == line);
    }
}
