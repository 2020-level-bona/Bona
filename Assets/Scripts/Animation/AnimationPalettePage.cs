using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class AnimationPalettePage
{
    public string condition;
    public List<AnimationStateNamePair> pairs = new List<AnimationStateNamePair>();

    public string GetStateName(string alias) {
        if (alias == null || alias.Length == 0)
            return null;
        
        foreach (AnimationStateNamePair pair in pairs) {
            if (pair.alias == alias)
                return pair.stateName;
        }
        return null;
    }

    public List<string> CheckConflicts() {
        List<string> conflictedNames = new List<string>();
        for (int i = 0; i < pairs.Count; i++) {
            for (int j = i + 1; j < pairs.Count; j++) {
                if (pairs[i].alias != null && pairs[i].alias.Length > 0 && pairs[i].alias == pairs[j].alias && !conflictedNames.Contains(pairs[i].alias))
                    conflictedNames.Add(pairs[i].alias);
            }
        }
        return conflictedNames;
    }

    public string GetAlias(string stateName) {
        foreach (AnimationStateNamePair pair in pairs) {
            if (pair.stateName == stateName)
                return (pair.alias != null && pair.alias.Length > 0) ? pair.alias : null;
        }
        return null;
    }

    public void RemoveAlias(string stateName) {
        foreach (AnimationStateNamePair pair in pairs) {
            if (pair.stateName == stateName) {
                pair.alias = "";
                return;
            }
        }
    }

    public void SetAlias(string stateName, string alias) {
        foreach (AnimationStateNamePair pair in pairs) {
            if (pair.stateName == stateName) {
                pair.alias = alias;
                return;
            }
        }
    }

    public bool IsAvailable() {
        return condition == null || condition.Length == 0 || Expression.CastAsBool(Expression.Eval(condition));
    }
}

[System.Serializable]
public class AnimationStateNamePair {
    public string stateName;
    public string alias;

    public AnimationStateNamePair(string stateName, string alias) {
        this.stateName = stateName;
        this.alias = alias;
    }
}