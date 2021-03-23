using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName="BS Color Theme")]
public class BScriptColorTheme : ScriptableObject
{
    static BScriptColorTheme instance;

    public static BScriptColorTheme Instance {
        get {
            if (!instance)
                instance = Resources.Load<BScriptColorTheme>("BScript/Default Color Theme");
            return instance;
        }
    }

    public Color keywordColor;
    public Color normalColor;
    public Color nameColor;
    public Color strColor;
    public Color numColor;
    public Color commentColor;
    public Color flagColor;

    public static Color GetColor(TokenType type) {
        switch (type) {
            case TokenType.KEYWORD:
                return Instance.keywordColor;
            case TokenType.NAME:
                return Instance.nameColor;
            case TokenType.STR:
                return Instance.strColor;
            case TokenType.NUM:
                return Instance.numColor;
            case TokenType.COMMENT:
                return Instance.commentColor;
            case TokenType.FLAG:
                return Instance.flagColor;
        }
        return Instance.normalColor;
    }
}
