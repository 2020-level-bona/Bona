using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour
{
    public List<PolygonCollider2D> floorPolygons {get; private set;}

    void Awake() {
        floorPolygons = new List<PolygonCollider2D>();

        int floorIndex = 1;
        while(true) {
            GameObject gameObject = GameObject.Find("Floor" + floorIndex);
            if (gameObject == null)
                break;
            
            PolygonCollider2D polygon = gameObject.GetComponent<PolygonCollider2D>();
            if (polygon == null)
                break;
            
            floorPolygons.Add(polygon);

            // Add Holes
            for (int i = 0; i < gameObject.transform.childCount; i++) {
                PolygonCollider2D hole = gameObject.transform.GetChild(i).GetComponent<PolygonCollider2D>();
                if (hole != null) {
                    polygon.pathCount++;
                    polygon.SetPath(polygon.pathCount - 1, hole.GetPath(0));
                }
            }

            floorIndex++;
        }
    }

    public int GetFloor(Vector2 position) {
        for (int i = 0; i < floorPolygons.Count; i++) {
            if (floorPolygons[i].OverlapPoint(position))
                return i + 1;
        }
        return 0;
    }
}
