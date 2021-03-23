using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName="BS Color Theme")]
public class BScriptColorTheme : ScriptableObject
{
    static BScriptColorTheme instance;

    public static BScriptColorTheme Instance {
        get {
            if (instance == null)
                instance = Resources.Load("BScript/Default Color Theme") as BScriptColorTheme;
            return instance;
        }
    }

    public Color keywordColor;
    public Color normalColor;
    public Color nameColor;
    public Color strColor;
    public Color numColor;
    public Color commentColor;
}
