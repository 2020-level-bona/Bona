using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BSCharacterNotSpawnedException : BSException
{
    public BSCharacterNotSpawnedException(int line) : base(line) { }
    public BSCharacterNotSpawnedException(int line, string message) : base(line, message) { }
    public BSCharacterNotSpawnedException(int line, string message, System.Exception inner) : base(line, message, inner) { }
    protected BSCharacterNotSpawnedException(
        System.Runtime.Serialization.SerializationInfo info,
        System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
}