using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZIndex : MonoBehaviour, ZIndexable
{
    public float offset = 0;
    public int alwaysBehindFloor = -1;
    public int alwaysFrontFloor = -1;

    public bool ShouldZIndex() {
        return true;
    }

    public Vector2 GetCoordinate() {
        return transform.position + Vector3.up * offset;
    }

    public Transform GetTransform() {
        return transform;
    }
}
