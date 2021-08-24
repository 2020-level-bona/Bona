using System.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZIndexer : MonoBehaviour
{
    // ZIndex를 가진 오브젝트는 [+Z_INDEX_RANGE, -Z_INDEX_RANGE]범위에서 Z 좌표가 변경됨
    const float Z_INDEX_RANGE = 5f;

    public float angle = 0f;

    // Update is called once per frame
    void LateUpdate()
    {
        UpdateZIndices();
    }

    void OnDrawGizmos() {
        Gizmos.color = Color.red;
        foreach (ZIndexable zIndexable in GetZIndexables()) {
            Gizmos.DrawLine(new Vector3(-10, -10 * Mathf.Tan(angle * Mathf.Deg2Rad) + getDepth(zIndexable.GetCoordinate()), 0), new Vector3(10, 10 * Mathf.Tan(angle * Mathf.Deg2Rad) + getDepth(zIndexable.GetCoordinate()), 0));
        }
    }

    void UpdateZIndices() {
        var zIndexables = GetZIndexables();
        Array.Sort(zIndexables, (ZIndexable a, ZIndexable b) => {
            float aDepth = getDepth(a.GetCoordinate());
            float bDepth = getDepth(b.GetCoordinate());
            return aDepth.CompareTo(bDepth);
        });
        for (int i = 0; i < zIndexables.Length; i++) {
            Transform transform = zIndexables[i].GetTransform();
            Vector3 position = transform.position;
            position.z = Mathf.Lerp(-Z_INDEX_RANGE, Z_INDEX_RANGE, i / (float) zIndexables.Length);
            transform.position = position;
        }
    }

    float getDepth(Vector2 pos) {
        return pos.y - Mathf.Tan(angle * Mathf.Deg2Rad) * pos.x;
    }

    ZIndexable[] GetZIndexables() {
        return FindObjectsOfType<MonoBehaviour>().OfType<ZIndexable>().Where((it) => it.ShouldZIndex()).ToArray();
    }
}