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

    public Token(int lineNumber, string str, int startIndex) {
        this.lineNumber = lineNumber;
        this.str = str;
        this.startIndex = startIndex;
        this.color = Color.white;
    }

    public static implicit operator string(Token token) => token.str;
}
