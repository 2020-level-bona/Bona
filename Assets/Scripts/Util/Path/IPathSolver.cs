using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPathSolver
{
    float GetNextT(float prevT, float moveLength);
    float GetMaxT();
    Vector2 CalcPosition(float t);
    bool HasEnoughPoints();
}
