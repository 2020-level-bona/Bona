using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class ClickScriptTrigger : ScriptTrigger
{
    Camera cam;
    Collider2D coll;

    void Awake() {
        cam = Camera.main;
        coll = GetComponent<Collider2D>();
    }

    void Update() {
        if (Input.GetMouseButtonDown(0) && coll.OverlapPoint(cam.ScreenToWorldPoint(Input.mousePosition)))
            Run();
    }
}
