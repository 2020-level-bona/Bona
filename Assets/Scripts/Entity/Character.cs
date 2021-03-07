using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : Movable
{
    public const CharacterType type = CharacterType.UNKNOWN;

    public float height = 2f;

    Level level;

    public int currentFloor {get; private set;} = 1;

    protected virtual void Awake() {
        level = FindObjectOfType<Level>();
    }

#if UNITY_EDITOR
    protected virtual void Update() {
        Debug.DrawRay(transform.position, Vector2.up * height, Color.blue);
    }
#endif

    public override void MoveTo(Vector2 position) {
        int floor = level.GetFloor(position);
        if (floor > 0)
            currentFloor = floor;

        if (currentFloor > 0) {
            base.MoveTo(level.floorPolygons[currentFloor - 1].ClosestPoint(position));
        }
    }

    public void MoveDelta(Vector2 positionDelta) {
        MoveTo((Vector2) transform.position + positionDelta);
    }
}
