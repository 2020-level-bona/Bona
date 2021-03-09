using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LinearPathSolver : IPathSolver
{
    public float GetNextT(Vector2[] points, float prevT, float moveLength) {
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

    public float GetMaxT(Vector2[] points) {
        return points.Length - 1;
    }

    public Vector2 GetPosition(Vector2[] points, float t) {
        t = Mathf.Clamp(t, 0f, points.Length - 1);

        int index = Mathf.FloorToInt(t);
        if (index == points.Length - 1)
            return points[points.Length - 1];
        
        Vector2 A = points[index];
        Vector2 B = points[index + 1];

        return Vector2.Lerp(A, B, t - index);
    }
}
