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
    AnimationPalette animationPalette;
    List<IAnimationController> animationControllers;
    Trigger trigger;
    ChatRenderer chatRenderer;

    void Awake() {
        level = FindObjectOfType<Level>();
        level.RegisterSpawnedCharacter(type, this);

        movable = FindObjectOfType<Movable>();
        animator = GetComponentInChildren<Animator>();
        animationPalette = GetComponentInChildren<AnimationPalette>();
        animationControllers = new List<IAnimationController>();
        animationControllers.Add(new WalkAnimationController(movable));

        trigger = GetComponent<Trigger>();
    }

    void Start() {
        if (trigger)
            trigger.AddListener(() => EventManager.Instance.OnCharacterClicked.Invoke(type));
    }

    void Update() {
        animationControllers.RemoveAll(x => x.HasDone());
        if (animationControllers.Count > 0)
            PlayClip(animationControllers[animationControllers.Count - 1].GetClip());
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

    void PlayClip(string clipNameOrAlias) {
        string clipName = clipNameOrAlias;
        if (animationPalette && animationPalette.GetClipName(clipNameOrAlias) != null)
            clipName = animationPalette.GetClipName(clipNameOrAlias);
        
        animator.Play(clipName);
    }
}
