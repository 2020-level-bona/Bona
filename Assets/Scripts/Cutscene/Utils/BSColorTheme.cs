using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 리소스 로드 문제를 해결하지 못하여 그냥 하드코딩하도록 하겠습니다.
public class BScriptColorTheme
{
    public static Color GetColor(TokenType type) {
        switch (type) {
            case TokenType.KEYWORD:
                return new Color(1f, 0.38039216f, 0.53333336f);
            case TokenType.NAME:
                return new Color(0.47058824f, 0.8627451f, 0.9098039f);
            case TokenType.STR:
                return new Color(1f, 0.84705883f, 0.4f);
            case TokenType.NUM:
                return new Color(0.67058825f, 0.6156863f, 0.9490196f);
            case TokenType.COMMENT:
                return new Color(0.6627451f, 0.8627451f, 0.4627451f);
            case TokenType.FLAG:
                return new Color(0.9882353f, 0.59607846f, 0.40392157f);
        }
        return new Color(0.965f, 0.965f, 0.965f);
    }
}
