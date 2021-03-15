using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(PolygonCollider2D))]
public class MouseClickTrigger : Trigger
{
    Game game;
    Camera cam;
    PolygonCollider2D coll;

    void Awake() {
        game = FindObjectOfType<Game>();
        coll = GetComponent<PolygonCollider2D>();
        cam = Camera.main;
    }

    void Update() {
        if (Input.GetMouseButtonDown(0) && !game.IsPlayingCutscene) {
            Vector2 worldPos = cam.ScreenToWorldPoint(Input.mousePosition);
            if (coll.OverlapPoint(worldPos)) {
                Event.Invoke();
            }
        }
    }
}
