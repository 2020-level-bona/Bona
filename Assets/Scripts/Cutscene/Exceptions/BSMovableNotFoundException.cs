using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BSMovableNotFoundException : BSException
{
    public BSMovableNotFoundException() { }
    public BSMovableNotFoundException(string message) : base(message) { }
    public BSMovableNotFoundException(string message, System.Exception inner) : base(message, inner) { }
    protected BSMovableNotFoundException(
        System.Runtime.Serialization.SerializationInfo info,
        System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
}