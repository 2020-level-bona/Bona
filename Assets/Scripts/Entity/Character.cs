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
    IAnimationController animationController;
    Trigger trigger;
    ChatRenderer chatRenderer;

    void Awake() {
        level = FindObjectOfType<Level>();
        level.RegisterSpawnedCharacter(type, this);

        movable = FindObjectOfType<Movable>();

        animator = GetComponentInChildren<Animator>();

        trigger = GetComponent<Trigger>();
    }

    void Start() {
        if (trigger)
            trigger.AddListener(() => EventManager.Instance.OnCharacterClicked.Invoke(type));
    }

    void Update() {
        if (animationController != null)
            animator.Play(animationController.GetClip());
    }

    void OnDestroy() {
        level.UnregisterSpawnedCharacter(type);
    }

    public void Show(float duration = 1f) {
        SpriteRenderer spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        
        Tween.Add(gameObject, x => {
            Color color = spriteRenderer.color;
            color.a = x;
            spriteRenderer.color = color;
        }, 0f, 1f, duration);
    }

    public void Hide(float duration = 1f, bool destroy = true) {
        SpriteRenderer spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        
        Tween.Add(gameObject, x => {
            Color color = spriteRenderer.color;
            color.a = x;
            spriteRenderer.color = color;
        }, 1f, 0f, duration, destroy ? (Action) (() => Destroy(gameObject)) : null);
    }

    public void ShowMessage(Chat chat, bool global = true) {
        if (chatRenderer)
            chatRenderer.Finish();
        
        chatRenderer = FindObjectOfType<ChatManager>().Render(chat, movable, global);
    }
}
