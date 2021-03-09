using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPathSolver
{
    float GetNextT(Vector2[] points, float prevT, float moveLength);
    float GetMaxT(Vector2[] points);
    Vector2 GetPosition(Vector2[] points, float t);
}
