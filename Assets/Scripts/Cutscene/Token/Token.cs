using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Token
{
    public int lineNumber;
    public string str;
    public int startIndex;
    public TokenType type = TokenType.NORMAL;

    public Token(int lineNumber, string str, int startIndex, TokenType type = TokenType.NORMAL) {
        this.lineNumber = lineNumber;
        this.str = str;
        this.startIndex = startIndex;
        this.type = type;
    }

    public static implicit operator string(Token token) => token.str;

    public static implicit operator Token(UnityEditor.SerializedProperty property) {
        return new Token(property.FindPropertyRelative("lineNumber").intValue,
            property.FindPropertyRelative("str").stringValue,
            property.FindPropertyRelative("startIndex").intValue,
            (TokenType) property.FindPropertyRelative("type").enumValueIndex);
    }
}
