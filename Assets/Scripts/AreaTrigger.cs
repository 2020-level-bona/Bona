using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class AreaTrigger : Trigger
{
    Game game;
    Player player;
    Collider2D coll;
    bool triggered = false;

    void Awake() {
        game = FindObjectOfType<Game>();
        player = FindObjectOfType<Player>();
        coll = GetComponent<Collider2D>();
    }

    void Update() {
        bool activated = coll.OverlapPoint(player.transform.position) && !game.IsPlayingCutscene;
        if (!triggered && activated) {
            Event.Invoke();
            triggered = true;
        } else if (triggered && !activated) {
            triggered = false;
        }
    }

    void OnDisable() {
        triggered = false;
    }
}
