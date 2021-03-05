using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MouseClickTrigger : Trigger
{
    Camera cam;
    PolygonCollider2D coll;

    void Awake() {
        coll = GetComponent<PolygonCollider2D>();
        cam = Camera.main;
    }

    void Update() {
        if (Input.GetMouseButtonDown(0)) {
            Vector2 worldPos = cam.ScreenToWorldPoint(Input.mousePosition);
            if (coll.OverlapPoint(worldPos)) {
                Event.Invoke();
            }
        }
    }
}
