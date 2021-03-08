using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vector2Lerp : ILerpable<Vector2>
{
    public Vector2 from, to;

    public Vector2Lerp(Vector2 from, Vector2 to) {
        this.from = from;
        this.to = to;
    }

    public Vector2 Lerp(float t) {
        return Vector2.Lerp(from, to, t);
    }
}
