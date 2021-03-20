using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class AreaTrigger : Trigger
{
    Game game;
    Player player;
    Collider2D coll;

    void Awake() {
        game = FindObjectOfType<Game>();
        player = FindObjectOfType<Player>();
        coll = GetComponent<Collider2D>();
    }

    void Update() {
        if (coll.OverlapPoint(player.transform.position) && !game.IsPlayingCutscene) {
            Event.Invoke();
        }
    }
}
