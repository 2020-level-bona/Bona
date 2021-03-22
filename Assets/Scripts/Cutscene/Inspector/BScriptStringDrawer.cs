using System;
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

        DrawBackground(position, prevString);

        codeProperty.stringValue = EditorGUI.TextArea(position, prevString, GetTextAreaStyle());

        float lineHeight = GetTextAreaStyle().lineHeight;
        foreach (char c in prevString) {
            if (c == '\n') {
                position.x = original.x;
                position.y += lineHeight;
                continue;
            }
            GUIStyle textStyle = GetTextStyle();

            EditorGUI.LabelField(position, c.ToString(), textStyle);

            position.x += textStyle.CalcSize(new GUIContent(c.ToString())).x;
        }
        // EditorGUI.EndProperty();
    }

    void DrawBackground(Rect position, string code) {
        int lines = 0;
        foreach (char c in code) {
            if (c == '\n')
                lines++;
        }

        float lineHeight = GetTextAreaStyle().lineHeight;

        for (int i = 0; i < lines; i++) {
            EditorGUI.DrawRect(new Rect(position.x, position.y + i * lineHeight, position.width, lineHeight),
                (i % 2 == 0) ? new Color(0.23f, 0.23f, 0.23f) : new Color(0.2f, 0.2f, 0.2f));
        }        
    }

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label) {
        return GetTextAreaStyle().CalcHeight(new GUIContent(property.FindPropertyRelative("code").stringValue), 10); // Text Wrapping을 하지 않으므로 width는 의미없다.
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
}
