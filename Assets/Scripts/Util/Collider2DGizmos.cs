using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collider2DGizmos
{
    public static void Draw(Collider2D coll) {
        if (coll is BoxCollider2D) {
            BoxCollider2D box = coll as BoxCollider2D;
            Gizmos.DrawWireCube((Vector2) box.transform.position + box.offset, box.size);
        } else if (coll is CircleCollider2D) {
            CircleCollider2D circle = coll as CircleCollider2D;
            
            Vector2 center = (Vector2) circle.transform.position + circle.offset;
            float radius = circle.radius;
            for (int i = 0; i < 32; i++) {
                Gizmos.DrawLine(center + new Vector2(Mathf.Cos(i / 32f * 2 * Mathf.PI), Mathf.Sin(i / 32f * 2 * Mathf.PI)) * radius
                    , center + new Vector2(Mathf.Cos((i + 1) / 32f * 2 * Mathf.PI), Mathf.Sin((i + 1) / 32f * 2 * Mathf.PI)) * radius);
            }
        } else if (coll is PolygonCollider2D) {
            PolygonCollider2D poly = coll as PolygonCollider2D;
            Vector2 center = (Vector2) poly.transform.position + poly.offset;
            for (int i = 0; i < poly.pathCount; i++) {
                Vector2[] points = poly.GetPath(i);
                for (int j = 0; j < points.Length; j++)
                    Gizmos.DrawLine(center + points[j], center + points[(j + 1) % points.Length]);
            }
        }
    }
}
