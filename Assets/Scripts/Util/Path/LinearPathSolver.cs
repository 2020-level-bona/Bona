using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LinearPathSolver : IPathSolver
{
    Vector2[] points;

    public LinearPathSolver(Vector2[] points) {
        this.points = points;
    }

    public float GetNextT(float prevT, float moveLength) {
        prevT = Mathf.Clamp(prevT, 0f, points.Length - 1);
        moveLength = Mathf.Max(moveLength, 0f);

        int index = Mathf.FloorToInt(prevT);
        if (index == points.Length - 1)
            return points.Length - 1;
        
        Vector2 A = points[index];
        Vector2 B = points[index + 1];

        float t = prevT + moveLength / (B - A).magnitude;
        t = Mathf.Min(t, index + 1);

        return t;
    }

    public float GetMaxT() {
        return Mathf.Max(points.Length - 1, 0);
    }

    public Vector2 CalcPosition(float t) {
        t = Mathf.Clamp(t, 0f, points.Length - 1);

        int index = Mathf.FloorToInt(t);
        if (index == points.Length - 1)
            return points[points.Length - 1];
        
        Vector2 A = points[index];
        Vector2 B = points[index + 1];

        return Vector2.Lerp(A, B, t - index);
    }
}
