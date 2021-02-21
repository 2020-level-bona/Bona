using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZIndex : MonoBehaviour
{
    [HideInInspector]
    public float z = 0f;

    public float y = 0f;
    public float angle = 0f;

    public int alwaysBehindFloor = -1;
    public int alwaysFrontFloor = -1;

    void Awake() {
        z = transform.position.z;
    }

    void OnDrawGizmos() {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(new Vector3(-10, y - 10 * Mathf.Tan(angle * Mathf.Deg2Rad), 0), new Vector3(10, y + 10 * Mathf.Tan(angle * Mathf.Deg2Rad), 0));
    }

    public bool IsBehindOf(Vector2 position) {
        return y + Mathf.Tan(angle * Mathf.Deg2Rad) * position.x > position.y;
    }

}
