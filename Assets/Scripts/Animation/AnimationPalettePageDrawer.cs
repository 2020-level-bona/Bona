#if UNITY_EDITOR
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomPropertyDrawer(typeof(AnimationPalettePage))]
public class AnimationPalettePageDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        using (new EditorGUI.PropertyScope(position, label, property)) {
            position.y += 4;

            List<AnimationStateNamePair> animationStateNamePairs = GetSerializedAnimationStateNamePairs(property);

            GUIStyle style = GetGUIStyle();

            SerializedProperty conditionProp = property.FindPropertyRelative("condition");
            SerializedProperty pairsProp = property.FindPropertyRelative("pairs");

            conditionProp.stringValue = EditorGUI.TextField(new Rect(position.x, position.y, position.width, style.lineHeight + 2), "조건식", conditionProp.stringValue);
            position.y += style.lineHeight + 4 + 8;

            for (int i = 0; i < pairsProp.arraySize; i++) {
                SerializedProperty pairProp = pairsProp.GetArrayElementAtIndex(i);
                SerializedProperty stateNameProp = pairProp.FindPropertyRelative("stateName");
                SerializedProperty aliasProp = pairProp.FindPropertyRelative("alias");
                
                aliasProp.stringValue = EditorGUI.TextField(new Rect(position.x, position.y + 2, position.width, style.lineHeight + 2), stateNameProp.stringValue, aliasProp.stringValue);
                position.y += style.lineHeight + 4;
            }
        }
    }

    List<AnimationStateNamePair> GetSerializedAnimationStateNamePairs(SerializedProperty property) {
        SerializedProperty pairsProp = property.FindPropertyRelative("pairs");
        List<AnimationStateNamePair> pairs = new List<AnimationStateNamePair>();
        for (int i = 0; i < pairsProp.arraySize; i++) {
            SerializedProperty pair = pairsProp.GetArrayElementAtIndex(i);
            pairs.Add(new AnimationStateNamePair(pair.FindPropertyRelative("stateName").stringValue, pair.FindPropertyRelative("alias").stringValue));
        }
        return pairs;
    }

    public static float CalcHeight(SerializedProperty property) {
        SerializedProperty pairsProp = property.FindPropertyRelative("pairs");
        return (GetGUIStyle().lineHeight + 4) * (pairsProp.arraySize + 1) + 24;
    }

    static GUIStyle GetGUIStyle() {
        GUIStyle style = new GUIStyle();
        return style;
    }
}
#endif