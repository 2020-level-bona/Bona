using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditorInternal;

[CustomEditor(typeof(AnimationPalette))]
public class AnimationPaletteEditor : Editor
{
    ReorderableList reorderableList;

    void OnEnable() {
        var prop = serializedObject.FindProperty("palettePages");

        reorderableList = new ReorderableList(serializedObject, prop);
        // reorderableList.elementHeight = 300;
        reorderableList.elementHeightCallback = (index) => {
            var element = prop.GetArrayElementAtIndex(index);
            return AnimationPalettePageDrawer.CalcHeight(element);
        };
        reorderableList.drawElementCallback = (rect, index, isActive, isFocused) => {
            var element = prop.GetArrayElementAtIndex(index);
            EditorGUI.PropertyField(rect, element);
        };
        reorderableList.drawHeaderCallback = (rect) => EditorGUI.LabelField(rect, "페이지 목록");
    }

    public override void OnInspectorGUI() {
        serializedObject.Update();
        reorderableList.DoLayoutList();
        serializedObject.ApplyModifiedProperties();
    }

    // public override void OnInspectorGUI()
    // {
    //     AnimationPalette palette = (AnimationPalette) target;

    //     var states = palette.GetAllStates();
        
    //     string prevCondition = palette.condition;
    //     palette.condition = EditorGUILayout.TextField("조건식", serializedObject.FindProperty("condition").stringValue);
    //     if (palette.condition != prevCondition)
    //         EditorUtility.SetDirty(target);

    //     if (isFolded = EditorGUILayout.Foldout(isFolded, "States")) {
    //         EditorGUILayout.LabelField($"총 {states.Count}개의 State가 로드되었습니다.");

    //         List<string> conflicts = palette.CheckConflicts();
    //         if (conflicts.Count > 0) {
    //             EditorGUILayout.LabelField($"{conflicts.Count}개의 이름 충돌이 발견되었습니다!", GetErrorTextStyle());
    //             EditorGUILayout.LabelField(string.Join(", ", conflicts), GetErrorTextStyle());
    //             EditorGUILayout.LabelField("부여된 별칭이 모두 고유하도록 재설정해주세요.", GetErrorTextStyle());
    //         }

    //         foreach (string state in states) {
    //             EditorGUILayout.BeginHorizontal();
    //             EditorGUILayout.LabelField(state);

    //             string prev = palette.GetAlias(state);

    //             string alias = EditorGUILayout.TextField(palette.GetAlias(state));
    //             if (alias == null || alias.Length == 0) {
    //                 if (palette.GetAlias(state) != null)
    //                     palette.RemoveAlias(state);
    //             } else {
    //                 palette.SetAlias(state, alias);
    //             }

    //             if (prev != palette.GetAlias(state))
    //                 EditorUtility.SetDirty(target);

    //             EditorGUILayout.EndHorizontal();
    //         }
    //     }
    // }

    GUIStyle GetErrorTextStyle() {
        GUIStyle style = new GUIStyle();
        style.normal.textColor = Color.red;
        style.fontStyle = UnityEngine.FontStyle.Bold;
        return style;
    }
}
