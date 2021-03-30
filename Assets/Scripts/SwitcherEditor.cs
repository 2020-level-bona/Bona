using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditorInternal;

[CustomEditor(typeof(Switcher))]
public class SwitcherEditor : Editor
{
    ReorderableList reorderableList;

    void OnEnable() {
        var prop = serializedObject.FindProperty("pageConfigs");

        reorderableList = new ReorderableList(serializedObject, prop);
        reorderableList.elementHeight = 150;
        reorderableList.drawElementCallback = (rect, index, isActive, isFocused) => {
            var element = prop.GetArrayElementAtIndex(index);
            rect.height -= 4;
            rect.y += 2;
            EditorGUI.PropertyField(rect, element);
        };
        reorderableList.drawHeaderCallback = (rect) => EditorGUI.LabelField(rect, "페이지 목록");
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();
        reorderableList.DoLayoutList();
        serializedObject.ApplyModifiedProperties();
    }
}
