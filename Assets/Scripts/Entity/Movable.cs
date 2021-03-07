using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movable : MonoBehaviour
{
    void MoveTo(Vector2 position) {
        transform.position = position;
    }
}
