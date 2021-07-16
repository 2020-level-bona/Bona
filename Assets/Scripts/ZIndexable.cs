using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ZIndexable
{
    Vector2 GetCoordinate();
    bool ShouldZIndex();
    Transform GetTransform();
}
