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

        List<BSException> exceptions = GetExceptions(property);

        DrawBackground(position, prevString, exceptions);
        DrawLinePointer(original, property.FindPropertyRelative("linePointer").intValue);

        codeProperty.stringValue = EditorGUI.TextArea(position, prevString, GetTextAreaStyle());

        float lineHeight = GetTextAreaStyle().lineHeight;
        float exceptionLineHeight = GetExceptionTextStyle().lineHeight;
        int line = 0;

        foreach (char c in prevString) {
            if (c == '\n') {
                position.x = original.x;
                position.y += lineHeight;
                line++;
                continue;
            }
            GUIStyle textStyle = GetTextStyle();

            EditorGUI.LabelField(position, c.ToString(), textStyle);

            position.x += textStyle.CalcSize(new GUIContent(c.ToString())).x;
        }

        DrawExceptions(new Rect(original.x, position.y + lineHeight, position.width, position.height), exceptions);
        // EditorGUI.EndProperty();
    }

    void DrawBackground(Rect position, string code, List<BSException> exceptions) {
        int lines = 0;
        foreach (char c in code) {
            if (c == '\n')
                lines++;
        }

        float lineHeight = GetTextAreaStyle().lineHeight;
        float exceptionLineHeight = GetExceptionTextStyle().lineHeight;
        float totalHeight = 0;

        for (int i = 0; i < lines; i++) {
            float currentHeight = lineHeight;
            EditorGUI.DrawRect(new Rect(position.x, position.y + totalHeight, position.width, currentHeight),
                (i % 2 == 0) ? new Color(0.23f, 0.23f, 0.23f) : new Color(0.2f, 0.2f, 0.2f));
            
            totalHeight += currentHeight;
        }
    }

    void DrawExceptions(Rect position, List<BSException> exceptions) {
        GUIStyle style = GetExceptionTextStyle();
        for (int i = 0; i < exceptions.Count; i++) {
            EditorGUI.LabelField(new Rect(position.x, position.y + i * style.lineHeight, position.width, position.height), $"라인 {exceptions[i].line}: {exceptions[i].Message}", style);
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
        GUIStyle style = new GUIStyle();
        style.normal.textColor = new Color(0, 0, 0, 0);
        style.fontSize = 18;
        return style;
    }

    GUIStyle GetTextStyle() {
        GUIStyle style = new GUIStyle();
        style.normal.textColor = Color.white;
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
