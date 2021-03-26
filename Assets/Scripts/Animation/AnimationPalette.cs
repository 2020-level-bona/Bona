using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class AnimationPalette : MonoBehaviour
{
    public List<AnimationClipNamePair> pairs = new List<AnimationClipNamePair>();

    void OnValidate() {
        List<string> clipNames = new List<string>();
        foreach (AnimationClip clip in GetComponent<Animator>().runtimeAnimatorController.animationClips)
            clipNames.Add(clip.name);
        
        pairs.RemoveAll(x => !clipNames.Contains(x.clipName));
    }

    public List<string> CheckConflicts() {
        List<string> conflictedNames = new List<string>();
        for (int i = 0; i < pairs.Count; i++) {
            for (int j = i + 1; j < pairs.Count; j++) {
                if (pairs[i].alias == pairs[j].alias && !conflictedNames.Contains(pairs[i].alias))
                    conflictedNames.Add(pairs[i].alias);
            }
        }
        return conflictedNames;
    }

    public string GetClipName(string alias) {
        foreach (AnimationClipNamePair pair in pairs) {
            if (pair.alias == alias)
                return pair.clipName;
        }
        return null;
    }

    public string GetAlias(string clipName) {
        foreach (AnimationClipNamePair pair in pairs) {
            if (pair.clipName == clipName)
                return pair.alias;
        }
        return null;
    }

    public void RemoveAlias(string clipName) {
        pairs.RemoveAll(x => x.clipName == clipName);
    }

    public void SetAlias(string clipName, string alias) {
        foreach (AnimationClipNamePair pair in pairs) {
            if (pair.clipName == clipName) {
                pair.alias = alias;
                return;
            }
        }
        pairs.Add(new AnimationClipNamePair(clipName, alias));
    }
}

[System.Serializable]
public class AnimationClipNamePair {
    public string clipName;
    public string alias;

    public AnimationClipNamePair(string clipName, string alias) {
        this.clipName = clipName;
        this.alias = alias;
    }
}