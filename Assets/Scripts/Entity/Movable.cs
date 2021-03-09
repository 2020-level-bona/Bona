using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movable : MonoBehaviour
{
    protected Level level;
    Vector2 lastPosition; // 속도 계산에 사용

    public int currentFloor {get; private set;} = 1;
    public Vector2 position {
        get{
            return transform.position;
        }
    }
    public Vector2 velocity {get; private set;}

    protected virtual void Awake() {
        level = FindObjectOfType<Level>();

        lastPosition = transform.position;
    }

    protected virtual void LateUpdate() {
        velocity = ((Vector2) transform.position - lastPosition) / Time.deltaTime;
        lastPosition = (Vector2) transform.position;
    }

    public void MoveTo(Vector2 position) {
        int floor = level.GetFloor(position);
        if (floor > 0)
            currentFloor = floor;

        if (currentFloor > 0) {
            Vector2 nextPosition = level.floorPolygons[currentFloor - 1].ClosestPoint(position);
            transform.position = new Vector3(nextPosition.x, nextPosition.y, transform.position.z);
        }
    }

    public void MoveDelta(Vector2 positionDelta) {
        MoveTo((Vector2) transform.position + positionDelta);
    }
}
