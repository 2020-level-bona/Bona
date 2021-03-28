using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movable : MonoBehaviour
{
    protected Level level;
    Vector2 lastPosition; // 속도 계산에 사용

    public Vector2 size = new Vector2(1f, 2f);
    public float baseSpeed = 5f;
    public float speedMultiplier = 1f;
    public float speed => baseSpeed * speedMultiplier;
    public bool ignoreRoad = false;

    public int currentFloor {get; private set;} = 1;
    public Vector2 position {
        get{
            return transform.position;
        }
    }
    public Vector2 velocity {get; private set;}

    void OnDrawGizmos() {
        Gizmos.color = Color.blue;
        Bounds bounds = GetBounds();
        Gizmos.DrawWireCube((bounds.min + bounds.max) / 2f, bounds.max - bounds.min);
    }

    void Awake() {
        level = FindObjectOfType<Level>();

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
        
        if (ignoreRoad) {
            transform.position = new Vector3(position.x, position.y, transform.position.z);
            return;
        }

        if (currentFloor > 0) {
            Vector2 nextPosition = level.floorPolygons[currentFloor - 1].ClosestPoint(position);
            transform.position = new Vector3(nextPosition.x, nextPosition.y, transform.position.z);
        }

        // FIXME : 이게 무슨 짓거리야
        if (GetComponent<Character>() != null && GetComponent<Character>().type == CharacterType.BONA)
            EventManager.Instance.OnPlayerMove?.Invoke(transform.position);
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
}
