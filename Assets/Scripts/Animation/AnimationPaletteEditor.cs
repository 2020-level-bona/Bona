using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(AnimationPalette))]
public class AnimationPaletteEditor : Editor
{
    bool isFolded = true;

    public override void OnInspectorGUI()
    {
        AnimationPalette palette = (AnimationPalette) target;

        var states = palette.GetAllStates();
        
        string prevCondition = palette.condition;
        palette.condition = EditorGUILayout.TextField("조건식", serializedObject.FindProperty("condition").stringValue);
        if (palette.condition != prevCondition)
            EditorUtility.SetDirty(target);

        if (isFolded = EditorGUILayout.Foldout(isFolded, "States")) {
            EditorGUILayout.LabelField($"총 {states.Count}개의 State가 로드되었습니다.");

            List<string> conflicts = palette.CheckConflicts();
            if (conflicts.Count > 0) {
                EditorGUILayout.LabelField($"{conflicts.Count}개의 이름 충돌이 발견되었습니다!", GetErrorTextStyle());
                EditorGUILayout.LabelField(string.Join(", ", conflicts), GetErrorTextStyle());
                EditorGUILayout.LabelField("부여된 별칭이 모두 고유하도록 재설정해주세요.", GetErrorTextStyle());
            }

            foreach (string state in states) {
                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.LabelField(state);

                string prev = palette.GetAlias(state);

                string alias = EditorGUILayout.TextField(palette.GetAlias(state));
                if (alias == null || alias.Length == 0) {
                    if (palette.GetAlias(state) != null)
                        palette.RemoveAlias(state);
                } else {
                    palette.SetAlias(state, alias);
                }

                if (prev != palette.GetAlias(state))
                    EditorUtility.SetDirty(target);

                EditorGUILayout.EndHorizontal();
            }
        }
    }

    GUIStyle GetErrorTextStyle() {
        GUIStyle style = new GUIStyle();
        style.normal.textColor = Color.red;
        style.fontStyle = UnityEngine.FontStyle.Bold;
        return style;
    }
}
