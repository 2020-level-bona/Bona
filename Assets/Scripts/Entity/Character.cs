using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : Movable
{
    public const CharacterType type = CharacterType.UNKNOWN;

    public float height = 2f;

#if UNITY_EDITOR
    void Update() {
        Debug.DrawRay(transform.position, Vector2.up * height, Color.blue);
    }
#endif
}
