using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Token
{
    public int lineNumber;
    public string str;
    public int startIndex;
    public Color color;

    public Token(int lineNumber, string str, int startIndex) : this(lineNumber, str, startIndex, Color.white) { }

    public Token(int lineNumber, string str, int startIndex, Color color) {
        this.lineNumber = lineNumber;
        this.str = str;
        this.startIndex = startIndex;
        this.color = color;
    }

    public static implicit operator string(Token token) => token.str;

    public static implicit operator Token(UnityEditor.SerializedProperty property) {
        return new Token(property.FindPropertyRelative("lineNumber").intValue,
            property.FindPropertyRelative("str").stringValue,
            property.FindPropertyRelative("startIndex").intValue,
            property.FindPropertyRelative("color").colorValue);
    }
}
