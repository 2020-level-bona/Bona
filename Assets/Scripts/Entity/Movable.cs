using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movable : MonoBehaviour, ZIndexable
{
    protected Level level;
    Vector2 lastPosition; // 속도 계산에 사용

    public Vector2 size = new Vector2(1f, 2f);
    public float baseSpeed = 5f;
    public float speedMultiplier = 1f;
    public float speed => baseSpeed * speedMultiplier;
    public bool ignoreRoad = false;
    public bool useFlip = true;
    SpriteRenderer spriteRenderer;

    public int currentFloor {get; private set;} = 1;
    public Vector2 position {
        get{
            return transform.position;
        }
    }
    public Vector2 velocity {get; private set;}

    public bool zIndex = false;

    void OnDrawGizmos() {
        Gizmos.color = Color.blue;
        Bounds bounds = GetBounds();
        Gizmos.DrawWireCube((bounds.min + bounds.max) / 2f, bounds.max - bounds.min);
    }

    void Awake() {
        level = FindObjectOfType<Level>();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();

        lastPosition = transform.position;
    }

#if UNITY_EDITOR
    void Update() {
        Debug.DrawRay(transform.position - new Vector3(size.x / 2f, 0, 0), Vector2.up * size.y, Color.blue);
        Debug.DrawRay(transform.position + new Vector3(size.x / 2f, 0, 0), Vector2.up * size.y, Color.blue);
        Debug.DrawRay(transform.position - new Vector3(size.x / 2f, 0, 0), Vector2.right * size.x, Color.blue);
        Debug.DrawRay(transform.position + new Vector3(-size.x / 2f, size.y, 0), Vector2.right * size.x, Color.blue);
    }
#endif

    void LateUpdate() {
        velocity = ((Vector2) transform.position - lastPosition) / Time.deltaTime;
        lastPosition = (Vector2) transform.position;
    }

    public void MoveTo(Vector2 position) {
        int floor = level.GetFloor(position);
        if (floor > 0)
            currentFloor = floor;
        
        if (transform.position.x < position.x)
            SetFace(Face.EAST);
        else if (transform.position.x > position.x)
            SetFace(Face.WEST);
        
        if (ignoreRoad) {
            transform.position = new Vector3(position.x, position.y, transform.position.z);
            return;
        }

        if (currentFloor > 0) {
            Vector2 nextPosition = level.floorPolygons[currentFloor - 1].ClosestPoint(position);
            transform.position = new Vector3(nextPosition.x, nextPosition.y, transform.position.z);
        }
    }

    public void MoveDelta(Vector2 positionDelta) {
        MoveTo((Vector2) transform.position + positionDelta);
    }

    public void MoveDirection(Vector2 direction) {
        MoveDelta(direction * speed * Time.deltaTime);
    }

    // 오브젝트의 원점은 중심에서 아래 지점이다.
    public Vector2 GetCenter() {
        return (Vector2) transform.position + new Vector2(0, size.y / 2f);
    }

    public Bounds GetBounds() {
        return new Bounds(GetCenter(), size);
    }

    public void SetFace(Face face) {
        if (!useFlip || !spriteRenderer)
            return;
        if (face == Face.WEST)
            spriteRenderer.flipX = false;
        else if (face == Face.EAST)
            spriteRenderer.flipX = true;
    }

    public bool ShouldZIndex() {
        return zIndex;
    }

    public Vector2 GetCoordinate() {
        return transform.position;
    }

    public Transform GetTransform() {
        return transform;
    }
}
