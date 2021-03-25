using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class AreaScriptTrigger : ScriptTrigger
{
    Camera cam;
    Collider2D coll;
    Movable playerMovable;

    void Awake() {
        cam = Camera.main;
        coll = GetComponent<Collider2D>();
    }

    void Start() {
        playerMovable = FindObjectOfType<Level>().GetSpawnedCharacter(CharacterType.BONA).GetComponent<Movable>();
    }

    void Update() {
        if (coll.OverlapPoint(playerMovable.transform.position))
            Run();
    }
}
