using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathController : MonoBehaviour
{
    public PathType type = PathType.LINEAR;

    void OnDrawGizmos() {
        Path path = GetPath();

        Gizmos.color = Color.yellow;
        for (int i = 0; i < path.Points.Length; i++) {
            Gizmos.DrawSphere(path.Points[i], 0.2f);
        }

        Gizmos.color = Color.red;
        IPathSolver solver = path.Solver;
        for (float t = 0; t < solver.GetMaxT(); t += 0.1f) {
            Gizmos.DrawLine(solver.CalcPosition(t), solver.CalcPosition(t + 0.1f));
        }
    }

    public Path GetPath() {
        Vector2[] points = new Vector2[transform.childCount];
        for (int i = 0; i < transform.childCount; i++) {
            points[i] = transform.GetChild(i).transform.position;
        }
        
        return new Path(type, points);
    }
}
