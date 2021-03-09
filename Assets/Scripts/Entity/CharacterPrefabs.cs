using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "CharacterPrefabs")]
public class CharacterPrefabs : ScriptableObject
{
    [Serializable]
    public struct CharacterPrefabEntry {
        public CharacterType type;
        public GameObject prefab;
    }
    
    public List<CharacterPrefabEntry> prefabs;

    public GameObject GetPrefab(CharacterType type) {
        foreach (CharacterPrefabEntry entry in prefabs) {
            if (entry.type == type)
                return entry.prefab;
        }
        return null;
    }
}
