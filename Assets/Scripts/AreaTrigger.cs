using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class AreaTrigger : Trigger
{
    Game game;
    Movable playerMovable;
    Collider2D coll;
    bool triggered = false;

    void Awake() {
        game = FindObjectOfType<Game>();
        coll = GetComponent<Collider2D>();
    }

    void Start() {
        playerMovable = FindObjectOfType<Level>().GetSpawnedCharacter(CharacterType.BONA).GetComponent<Movable>();
    }

    void Update() {
        bool activated = coll.OverlapPoint(playerMovable.transform.position) && !game.IsPlayingCutscene;
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
