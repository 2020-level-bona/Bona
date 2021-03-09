using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : Movable
{
    public virtual CharacterType type {
        get => CharacterType.UNKNOWN;
    }

    public Vector2 size = new Vector2(1f, 2f);

    AnimatorController animatorController;

    SpriteRenderer spriteRenderer;

    Trigger trigger;

    protected override void Awake() {
        base.Awake();

        level.RegisterSpawnedCharacter(type, this);

        animatorController = GetComponentInChildren<AnimatorController>();

        trigger = GetComponent<Trigger>();
    }

    protected virtual void Start() {
        if (trigger)
            trigger.AddListener(() => EventManager.Instance.OnCharacterClicked.Invoke(type));
    }

    protected virtual void OnDestroy() {
        level.UnregisterSpawnedCharacter(type);
    }

#if UNITY_EDITOR
    protected virtual void Update() {
        Debug.DrawRay(transform.position - new Vector3(size.x / 2f, 0, 0), Vector2.up * size.y, Color.blue);
        Debug.DrawRay(transform.position + new Vector3(size.x / 2f, 0, 0), Vector2.up * size.y, Color.blue);
        Debug.DrawRay(transform.position - new Vector3(size.x / 2f, 0, 0), Vector2.right * size.x, Color.blue);
        Debug.DrawRay(transform.position + new Vector3(-size.x / 2f, size.y, 0), Vector2.right * size.x, Color.blue);
    }
#endif

    // 캐릭터의 원점은 중심에서 아래 지점이다.
    public Vector2 GetCenter() {
        return (Vector2) transform.position + new Vector2(0, size.y / 2f);
    }

    public Bounds GetBounds() {
        return new Bounds(GetCenter(), size);
    }

    public void Show(float duration = 1f) {
        if (spriteRenderer == null)
            spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        
        Tween.Add(gameObject, x => {
            Color color = spriteRenderer.color;
            color.a = x;
            spriteRenderer.color = color;
        }, 0f, 1f, duration);
    }

    public void Hide(float duration = 1f, bool destroy = true) {
        if (spriteRenderer == null)
            spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        
        Tween.Add(gameObject, x => {
            Color color = spriteRenderer.color;
            color.a = x;
            spriteRenderer.color = color;
        }, 1f, 0f, duration, destroy ? (Action) (() => Destroy(gameObject)) : null);
    }

    public void PlayAnimation(AnimatorState state) {
        animatorController.Play(state);
    }
}
