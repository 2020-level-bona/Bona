using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    public CharacterType type = CharacterType.UNKNOWN;

    Level level;
    public Movable movable {get; private set;}
    Animator animator;
    AnimationPalette[] animationPalettes;
    List<IAnimationController> animationControllers;
    Trigger trigger;
    ChatRenderer chatRenderer;

    void Awake() {
        level = FindObjectOfType<Level>();
        level.RegisterSpawnedCharacter(type, this);

        movable = GetComponent<Movable>();
        animator = GetComponentInChildren<Animator>();
        animationPalettes = animator.GetComponents<AnimationPalette>();
        animationControllers = new List<IAnimationController>();
        animationControllers.Add(new WalkAnimationController(movable));

        trigger = GetComponent<Trigger>();
    }

    void Start() {
        if (trigger)
            trigger.AddListener(() => EventManager.Instance.OnCharacterClicked?.Invoke(type));
    }

    void Update() {
        animationControllers.RemoveAll(x => x.HasDone());
        if (animationControllers.Count > 0)
            PlayState(animationControllers[animationControllers.Count - 1].GetState());
    }

    public void ShowMessage(Chat chat, bool global = true) {
        if (chatRenderer)
            chatRenderer.Finish();
        
        chatRenderer = FindObjectOfType<ChatManager>().Render(chat, movable, global);
    }

    public void AddAnimationController(IAnimationController animationController) {
        animationControllers.Add(animationController);
    }

    public void RemoveAnimationController(IAnimationController animationController) {
        animationControllers.Remove(animationController);
    }

    // WalkAnimationController를 제외한 모든 IAnimationController를 삭제한다.
    public void ClearAnimationControllers() {
        animationControllers.RemoveAll(x => !(x is WalkAnimationController));
    }

    void PlayState(string stateNameOrAlias) {
        string stateName = stateNameOrAlias;
        foreach (AnimationPalette animationPalette in animationPalettes) {
            if (animationPalette.IsAvailable() && animationPalette.GetStateName(stateNameOrAlias) != null)
                stateName = animationPalette.GetStateName(stateNameOrAlias);
        }
        animator.Play(stateName);
    }
}
