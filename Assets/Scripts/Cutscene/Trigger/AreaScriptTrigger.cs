using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class AreaScriptTrigger : ScriptTrigger
{
    Camera cam;
    Collider2D coll;
    Player player;

    void Awake() {
        cam = Camera.main;
        coll = GetComponent<Collider2D>();
        player = FindObjectOfType<Player>();
    }

    void Update() {
        if (coll.OverlapPoint(player.transform.position))
            Run();
    }
}
