using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.Animations;

[RequireComponent(typeof(Animator))]
public class AnimationPalette : MonoBehaviour
{
    public string condition;
    public List<AnimationStateNamePair> pairs = new List<AnimationStateNamePair>();

    void OnValidate() {
        List<string> stateNames = GetAllStates();
        
        pairs.RemoveAll(x => !stateNames.Contains(x.stateName));
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

    public List<string> GetAllStates() {
        List<string> states = new List<string>();
        AnimatorController animatorController = GetComponent<Animator>().runtimeAnimatorController as AnimatorController;
        foreach (ChildAnimatorState childAnimatorState in animatorController.layers[0].stateMachine.states)
            states.Add(childAnimatorState.state.name);
        return states;
    }

    public string GetStateName(string alias) {
        foreach (AnimationStateNamePair pair in pairs) {
            if (pair.alias == alias)
                return pair.stateName;
        }
        return null;
    }

    public string GetAlias(string stateName) {
        foreach (AnimationStateNamePair pair in pairs) {
            if (pair.stateName == stateName)
                return pair.alias;
        }
        return null;
    }

    public void RemoveAlias(string stateName) {
        pairs.RemoveAll(x => x.stateName == stateName);
    }

    public void SetAlias(string stateName, string alias) {
        foreach (AnimationStateNamePair pair in pairs) {
            if (pair.stateName == stateName) {
                pair.alias = alias;
                return;
            }
        }
        pairs.Add(new AnimationStateNamePair(stateName, alias));
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