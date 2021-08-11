using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterFollowManager : MonoBehaviour
{
    // key follows value
    Dictionary<CharacterType, CharacterType> table = new Dictionary<CharacterType, CharacterType>();
    Level level;
    bool init; // late init

    void Awake() {
        foreach (string key in Session.Follow.GetKeys()) {
            CharacterType k, v;
            if (!Enum.TryParse<CharacterType>(key, out k) || !Enum.TryParse<CharacterType>(Session.Follow.GetString(key), out v)) continue;
            table[k] = v;
        }
        level = FindObjectOfType<Level>();
    }

    void Start() {
        EventManager.Instance.OnPreSave += Save;
    }

    void Update() {
        if (!init) {
            foreach (var p in table) {
                if (level.GetSpawnedCharacter(p.Value) != null && level.GetSpawnedCharacter(p.Key) == null) {
                    level.SpawnCharacter(p.Key, Vector2.zero);
                }
            }
            foreach (var p in table) {
                level.GetSpawnedCharacter(p.Key).movable.MoveTo(level.GetSpawnedCharacter(p.Value).movable.position);
            }
            init = true;
        }
        foreach (var p in table) {
            Character follower = level.GetSpawnedCharacter(p.Key);
            Character target = level.GetSpawnedCharacter(p.Value);
            if (follower == null || target == null) continue;
            if (Vector2.Distance(follower.movable.position, target.movable.position) >= 2f) {
                follower.movable.MoveDirection((target.movable.position - follower.movable.position).normalized);
            }
        }
    }

    void Save() {
        Session.Follow.Clear();
        foreach (var p in table) {
            Session.Follow.Set(p.Key.ToString(), p.Value.ToString());
        }
    }

    public void Follow(Character follower, Character target) {
        table[follower.type] = target.type;
    }

    public void UnFollow(Character follower) {
        table.Remove(follower.type);
    }
}
