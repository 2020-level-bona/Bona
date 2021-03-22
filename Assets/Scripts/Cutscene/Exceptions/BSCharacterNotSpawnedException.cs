using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BSCharacterNotSpawnedException : BSException
{
    public BSCharacterNotSpawnedException() { }
    public BSCharacterNotSpawnedException(string message) : base(message) { }
    public BSCharacterNotSpawnedException(string message, System.Exception inner) : base(message, inner) { }
    protected BSCharacterNotSpawnedException(
        System.Runtime.Serialization.SerializationInfo info,
        System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
}