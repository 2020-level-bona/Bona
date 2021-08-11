using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor.Animations;
#endif

[RequireComponent(typeof(Animator))]
public class AnimationPalette : MonoBehaviour
{
    [SerializeField]
    AnimationPalettePage[] palettePages;
#if UNITY_EDITOR
    void OnValidate() {
        if (palettePages == null)
            return;
        
        // 존재하지 않는 stateName을 가진 Pair를 모두 제거
        List<string> stateNames = GetAllStates();
        foreach (AnimationPalettePage page in palettePages) {
            page.pairs.RemoveAll(x => !stateNames.Contains(x.stateName));
        }

        // 존재하는 stateName들 중 Pair에는 포함되어 있지 않은 stateName들을 새롭게 추가
        foreach (string stateName in stateNames) {
            foreach (AnimationPalettePage page in palettePages) {
                bool found = false;
                foreach (AnimationStateNamePair pair in page.pairs) {
                    if (pair.stateName == stateName) {
                        found = true;
                        break;
                    }
                }

                if (!found) {
                    page.pairs.Add(new AnimationStateNamePair(stateName, ""));
                }
            }
        }
    }

    List<string> GetAllStates() {
        List<string> states = new List<string>();
        AnimatorController animatorController = GetComponent<Animator>().runtimeAnimatorController as AnimatorController;
        foreach (ChildAnimatorState childAnimatorState in animatorController.layers[0].stateMachine.states)
            states.Add(childAnimatorState.state.name);
        return states;
    }
#endif
    public string GetState(string stateNameOrAlias) {
        foreach (AnimationPalettePage palettePage in palettePages) {
            if (palettePage.IsAvailable()) {
                string stateName = stateNameOrAlias;
                if (palettePage.GetStateName(stateNameOrAlias) != null)
                    stateName = palettePage.GetStateName(stateNameOrAlias);
                return stateName;
            }
        }
        return stateNameOrAlias;
    }
}
