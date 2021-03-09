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
