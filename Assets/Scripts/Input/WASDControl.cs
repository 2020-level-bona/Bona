using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Movable))]
public class WASDControl : MonoBehaviour
{
    Movable movable;

    void Awake() {
        movable = GetComponent<Movable>();
    }

    void Update() {
        movable.MoveDirection(new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")));
    }
}
