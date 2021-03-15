using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Path
{
    public PathType Type;
    public Vector2[] Points;
    public IPathSolver Solver;

    public Path(PathType type, Vector2[] points) {
        Type = type;
        Points = points;
        Solver = GetSolver(Type, Points);

        if (!Solver.HasEnoughPoints())
            throw new System.Exception("경로의 제어점의 개수가 충분하지 않습니다.");
    }

    static IPathSolver GetSolver(PathType Type, Vector2[] Points) {
        switch (Type) {
            case PathType.LINEAR:
                return new LinearPathSolver(Points);
            case PathType.BEZIER:
                return new BezierPathSolver(Points);
        }
        return null;
    }
}
