using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomPropertyDrawer(typeof(PageConfig))]
public class PageConfigDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label) {
        using (new EditorGUI.PropertyScope(position, label, property)) {
            float lineHeight = EditorGUIUtility.singleLineHeight;
            position.height = lineHeight;

            var conditionProperty = property.FindPropertyRelative("condition");
            var hideProperty = property.FindPropertyRelative("hide");
            var positonProperty = property.FindPropertyRelative("position");
            var spriteProperty = property.FindPropertyRelative("sprite");
            var triggerExecutorProperty = property.FindPropertyRelative("triggerExecutor");
            var autoExecutorProperty = property.FindPropertyRelative("autoExecutor");
            
            conditionProperty.stringValue = EditorGUI.TextField(position, "조건식", conditionProperty.stringValue);
            position.y += lineHeight + 4;

            hideProperty.boolValue = EditorGUI.Toggle(position, "숨김", hideProperty.boolValue);
            position.y += lineHeight + 4;

            positonProperty.stringValue = EditorGUI.TextField(position, "위치 (마커 또는 좌표)", positonProperty.stringValue);
            position.y += lineHeight + 4;

            spriteProperty.objectReferenceValue = EditorGUI.ObjectField(position, "스프라이트", spriteProperty.objectReferenceValue, typeof(Sprite), false);
            position.y += lineHeight + 4;

            BScriptExecutor triggerExecutor = triggerExecutorProperty.objectReferenceValue as BScriptExecutor;
            triggerExecutorProperty.objectReferenceValue = EditorGUI.ObjectField(position, "트리거 스크립트 " + ((triggerExecutor != null) ? $"[{triggerExecutor.uniqueId}]" : ""), triggerExecutorProperty.objectReferenceValue, typeof(BScriptExecutor), true);
            position.y += lineHeight + 4;

            BScriptExecutor autoExecutor = autoExecutorProperty.objectReferenceValue as BScriptExecutor;
            autoExecutorProperty.objectReferenceValue = EditorGUI.ObjectField(position, "오토 스크립트 " + ((autoExecutor != null) ? $"[{autoExecutor.uniqueId}]" : ""), autoExecutorProperty.objectReferenceValue, typeof(BScriptExecutor), true);
            position.y += lineHeight + 4;
        }
    }
}
