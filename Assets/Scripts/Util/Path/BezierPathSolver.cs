using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BezierPathSolver : IPathSolver
{
    public float GetNextT(Vector2[] points, float prevT, float moveLength) {
        prevT = Mathf.Clamp(prevT, 0f, points.Length - 2);
        moveLength = Mathf.Max(moveLength, 0f);

        int index = Mathf.FloorToInt(prevT);
        if (index == points.Length - 2)
            return points.Length - 2;
        
        Vector2 A = (points[index] + points[index + 1]) / 2f;
        Vector2 B = points[index + 1];
        Vector2 C = (points[index + 1] + points[index + 2]) / 2f;

        // 미분
        Vector2 D0 = -2 * A + 2 * B;
        Vector2 D1 = 2 * A - 4 * B + 2 * C;

        float t = prevT + moveLength / (D0 + D1 * (prevT - index)).magnitude;
        t = Mathf.Min(t, index + 1);

        return t;
    }

    public float GetMaxT(Vector2[] points) {
        return (points.Length - 2);
    }

    public Vector2 GetPosition(Vector2[] points, float t) {
        t = Mathf.Clamp(t, 0f, (points.Length - 2));

        int index = Mathf.FloorToInt(t);
        if (index == (points.Length - 2))
            return (points[points.Length - 2] + points[points.Length - 1]) / 2f;
        
        Vector2 A = (points[index] + points[index + 1]) / 2f;
        Vector2 B = points[index + 1];
        Vector2 C = (points[index + 1] + points[index + 2]) / 2f;

        Vector2 T = Vector2.Lerp(A, B, t - index);
        Vector2 S = Vector2.Lerp(B, C, t - index);
        return Vector2.Lerp(T, S, t - index);
    }
}
