using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Vector2 velocityScale = new Vector2(1f, 10f);

    List<PolygonCollider2D> floorPolygons;
    public int currentFloor = 1;

    void Start() {
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

    void Update() {
        Vector3 current = transform.position;

        Vector3 velocity = new Vector3(Input.GetAxis("Horizontal") * velocityScale.x, Input.GetAxis("Vertical") * velocityScale.y, 0);
        if (velocity.magnitude == 0)
            return;
        
        Vector3 delta = velocity * Time.deltaTime;
        Vector3 next = current + delta;

        /*RaycastHit2D hit = Physics2D.Raycast(current, velocity);
        // Debug.Log(hit.distance);
        if (hit) {
            
            delta = velocity.normalized * Mathf.Min(hit.distance - 0.1f, delta.magnitude);
            transform.position = current + delta;
        }
        else {
            transform.position = current + delta;
        }*/

        // Debug.Log(current + delta + " : " + polygon.OverlapPoint(new Vector2(next.x, next.y)));

        if (floorPolygons[currentFloor - 1].OverlapPoint(current + delta)) {
            transform.position = current + delta;
        } else {
            Vector3 point = floorPolygons[currentFloor - 1].ClosestPoint(current + delta) ;
            /*RaycastHit2D hit = Physics2D.Raycast(current + velocity.normalized * 0.1f, -velocity, 0.2f);
            if (hit) {
                Debug.DrawRay(hit.point, hit.normal, Color.blue);

                Vector3 newDelta = Vector3.Project(delta, Vector2.Perpendicular(hit.normal));

                transform.position = new Vector3(hit.point.x + newDelta.x, hit.point.y + newDelta.y, current.z);
            }*/

            transform.position = new Vector3(point.x, point.y, current.z);
        }

        /*RaycastHit velocityHit;
        if (velocity.x != 0 && Physics.Raycast(current + Vector3.up * 1f, new Vector3(velocity.x, 0, 0), out velocityHit)) {
            if (velocityHit.distance < 0.3f)
                velocityHit.distance = 0f;
            delta.x = Mathf.Clamp(delta.x, -velocityHit.distance, velocityHit.distance);
        }
        if (velocity.z != 0 && Physics.Raycast(current + Vector3.up * 1f, new Vector3(0, 0, velocity.z), out velocityHit)) {
            if (velocityHit.distance < 0.3f)
                velocityHit.distance = 0f;
            delta.z = Mathf.Clamp(delta.z, -velocityHit.distance, velocityHit.distance);
            Debug.Log(velocityHit.distance);
        }

        Vector3 next = current + delta;

        RaycastHit raycastHit;
        if (Physics.Raycast(next + Vector3.up * 0.5f, Vector3.down, out raycastHit)) {
            next = raycastHit.point;

            transform.position = next;
        }*/
    }

    PolygonCollider2D GetCurrentFloor() {
        return floorPolygons[currentFloor - 1];
    }
}