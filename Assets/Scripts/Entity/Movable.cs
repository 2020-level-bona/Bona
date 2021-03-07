using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movable : MonoBehaviour
{
    public virtual void MoveTo(Vector2 position) {
        transform.position = position;
    }

    public void MoveDelta(Vector2 positionDelta) {
        MoveTo((Vector2) transform.position + positionDelta);
    }
}
