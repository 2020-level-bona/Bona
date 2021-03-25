using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class AreaTrigger : Trigger
{
    Game game;
    Collider2D coll;
    bool triggered = false;

    void Awake() {
        game = FindObjectOfType<Game>();
        coll = GetComponent<Collider2D>();
        EventManager.Instance.OnPlayerMove += UpdateMovement;
    }

    void UpdateMovement(Vector2 position) {
        bool activated = coll.OverlapPoint(position);
        if (!triggered && activated) {
            Invoke();
            triggered = true;
        } else if (triggered && !activated) {
            triggered = false;
        }
    }

    void OnDisable() {
        triggered = false;
    }
}
