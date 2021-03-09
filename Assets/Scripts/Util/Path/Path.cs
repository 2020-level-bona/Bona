using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Path : MonoBehaviour
{
    public PathType type = PathType.LINEAR;

    void OnDrawGizmos() {
        Vector2[] points = GetPoints();
        IPathSolver solver = GetSolver(type);

        Gizmos.color = Color.yellow;
        for (int i = 0; i < points.Length; i++) {
            Gizmos.DrawSphere(points[i], 0.2f);
        }

        Gizmos.color = Color.red;
        for (float t = 0; t < solver.GetMaxT(points); t += 0.1f) {
            Gizmos.DrawLine(solver.GetPosition(points, t), solver.GetPosition(points, t + 0.1f));
        }
    }

    public Vector2[] GetPoints() {
        Vector2[] paths = new Vector2[transform.childCount];
        for (int i = 0; i < transform.childCount; i++) {
            paths[i] = transform.GetChild(i).transform.position;
        }
        return paths;
    }

    public IPathSolver GetSolver(PathType type) {
        switch (type) {
            case PathType.LINEAR:
                return new LinearPathSolver();
            case PathType.BEZIER:
                return new BezierPathSolver();
        }
        return null;
    }
}
