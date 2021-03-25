﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour
{
    public List<PolygonCollider2D> floorPolygons {get; private set;}

    public CharacterPrefabs characterPrefabs;

    Dictionary<CharacterType, Character> spawnedCharacters = new Dictionary<CharacterType, Character>();

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

    public void RegisterSpawnedCharacter(CharacterType type, Character character) {
        if (spawnedCharacters.ContainsKey(type))
            throw new System.Exception($"CharacterType={type} 에 대해 이미 스폰된 캐릭터가 존재합니다.");
        
        spawnedCharacters.Add(type, character);
    }

    public Character GetSpawnedCharacter(CharacterType type) {
        Character character = null;
        spawnedCharacters.TryGetValue(type, out character);
        return character;
    }

    public Character SpawnCharacter(CharacterType type, Vector2 position) {
        if (GetSpawnedCharacter(type) != null)
            throw new System.Exception($"CharacterType={type} 에 대해 이미 스폰된 캐릭터가 존재합니다.");

        GameObject gameObject = characterPrefabs.GetPrefab(type);
        if (gameObject != null) {
            gameObject = Instantiate(gameObject);
            gameObject.transform.position = position;
            
            Character character = gameObject.GetComponent<Character>();
            if (character != null)
                return character;
            else
                Destroy(gameObject);
        }
        return null;
    }

    public Movable GetMovable(string name) {
        name = name.ToLower();
        foreach (Movable movable in FindObjectsOfType<Movable>()) {
            if (movable.gameObject.name.ToLower() == name)
                return movable;
        }
        return null;
    }

    public Marker GetMarker(string name) {
        name = name.ToLower();
        foreach (Marker marker in FindObjectsOfType<Marker>()) {
            if (marker.gameObject.name.ToLower() == name)
                return marker;
        }
        return null;
    }
}
