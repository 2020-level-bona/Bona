using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorController : MonoBehaviour
{
    public Animator animator;
    SpriteRenderer spriteRenderer;

    bool flipped = false;

    void Start()
    {
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void Play(AnimatorState state)
    {
        if (state.ShouldFlip)
            spriteRenderer.flipX = state.FlipValue;
        
        animator.Play(state.Hash);
    }
}
