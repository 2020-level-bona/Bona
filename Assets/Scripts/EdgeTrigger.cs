using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EdgeTrigger : MonoBehaviour
{
    Player player;

    EdgeCollider2D edgeCollider;
    bool activated = false;

    void Start()
    {
        player = FindObjectOfType<Player>();
        edgeCollider = GetComponent<EdgeCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 playerPosition = player.transform.position;
        Vector2 closest = edgeCollider.ClosestPoint(playerPosition);
        if (Vector2.Distance(playerPosition, closest) <= 0.25f) {
            if (!activated) {
                activated = true;
                Activate();
            }
        }
        else {
            if (activated) {
                activated = false;
                Deactivate();
            }
        }
    }

    void Activate() {
        if (player.currentFloor == 1) {
            player.currentFloor = 2;
        } else {
            player.currentFloor = 1;
        }
        Debug.Log("Change Floor to " + player.currentFloor);
    }

    void Deactivate() {

    }
}
