using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BSMovableNotFoundException : BSException
{
    public BSMovableNotFoundException(int line) : base(line) { }
    public BSMovableNotFoundException(int line, string message) : base(line, message) { }
    public BSMovableNotFoundException(int line, string message, System.Exception inner) : base(line, message, inner) { }
    protected BSMovableNotFoundException(
        System.Runtime.Serialization.SerializationInfo info,
        System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
}