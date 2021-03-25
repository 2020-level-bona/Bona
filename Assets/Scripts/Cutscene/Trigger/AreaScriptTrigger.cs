using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class AreaScriptTrigger : ScriptTrigger
{
    Camera cam;
    Collider2D coll;

    void Awake() {
        cam = Camera.main;
        coll = GetComponent<Collider2D>();
        EventManager.Instance.OnPlayerMove += UpdateMovement;
    }

    void UpdateMovement(Vector2 position) {
        if (coll.OverlapPoint(position))
            Run();
    }
}
