using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Movable))]
public class SpriteAutoFlipper : MonoBehaviour
{
    Movable movable;
    SpriteRenderer spriteRenderer;

    void Awake() {
        movable = GetComponent<Movable>();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
    }

    void Update() {
        if (Mathf.Abs(movable.velocity.x) > 1e-5f)
            spriteRenderer.flipX = movable.velocity.x > 0f;
    }
}
