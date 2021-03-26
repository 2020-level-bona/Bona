using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(AnimationPalette))]
public class AnimationPaletteEditor : Editor
{
    public override void OnInspectorGUI()
    {
        AnimationPalette palette = (AnimationPalette) target;

        Animator animator = palette.GetComponent<Animator>();
        var clips = animator.runtimeAnimatorController.animationClips;

        EditorGUILayout.LabelField($"총 {clips.Length}개의 애니메이션 클립이 로드되었습니다.");
        
        List<string> conflicts = palette.CheckConflicts();
        if (conflicts.Count > 0) {
            EditorGUILayout.LabelField($"{conflicts.Count}개의 이름 충돌이 발견되었습니다!", GetErrorTextStyle());
            EditorGUILayout.LabelField(string.Join(", ", conflicts), GetErrorTextStyle());
            EditorGUILayout.LabelField("부여된 별칭이 모두 고유하도록 재설정해주세요.", GetErrorTextStyle());
        }

        foreach (AnimationClip clip in clips) {
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField(clip.name);

            string alias = EditorGUILayout.TextField(palette.GetAlias(clip.name));
            if (alias == null || alias.Length == 0) {
                if (palette.GetAlias(clip.name) != null)
                    palette.RemoveAlias(clip.name);
            } else {
                palette.SetAlias(clip.name, alias);
            }

            EditorGUILayout.EndHorizontal();
        }
    }

    GUIStyle GetErrorTextStyle() {
        GUIStyle style = new GUIStyle();
        style.normal.textColor = Color.red;
        style.fontStyle = UnityEngine.FontStyle.Bold;
        return style;
    }
}
