using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteEffector : MonoBehaviour
{
    SpriteRenderer spriteRenderer;

    void Awake() {
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
    }

    public void Show(float duration = 1f) {
        SpriteRenderer spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        
        Tween.Add(gameObject, x => {
            Color color = spriteRenderer.color;
            color.a = x;
            spriteRenderer.color = color;
        }, 0f, 1f, duration);
    }

    public void Hide(float duration = 1f) {
        SpriteRenderer spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        
        Tween.Add(gameObject, x => {
            Color color = spriteRenderer.color;
            color.a = x;
            spriteRenderer.color = color;
        }, 1f, 0f, duration);
    }
}
