using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Collider2D))]
public class MouseClickTrigger : Trigger
{
    Game game;
    Camera cam;
    Collider2D coll;

    void Awake() {
        game = FindObjectOfType<Game>();
        coll = GetComponent<Collider2D>();
        cam = Camera.main;
    }

    void Update() {
        if (Input.GetMouseButtonDown(0)) {
            Vector2 worldPos = cam.ScreenToWorldPoint(Input.mousePosition);
            if (coll.OverlapPoint(worldPos)) {
                Invoke();
            }
        }
    }
}
