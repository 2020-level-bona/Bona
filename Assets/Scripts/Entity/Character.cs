using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : Movable
{
    public virtual CharacterType type {
        get {
            return CharacterType.UNKNOWN;
        }
    }

    public float height = 2f;

    Level level;

    public int currentFloor {get; private set;} = 1;

    protected virtual void Awake() {
        level = FindObjectOfType<Level>();
    }

    protected virtual void Start() {
        level.RegisterSpawnedCharacter(type, this);
    }

    protected virtual void OnDestroy() {
        level.UnregisterSpawnedCharacter(type);
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
}
