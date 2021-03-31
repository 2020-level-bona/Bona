using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BSMarkerNotFoundException : BSException
{
    public BSMarkerNotFoundException(int line) : base(line) { }
    public BSMarkerNotFoundException(int line, string message) : base(line, message) { }
    public BSMarkerNotFoundException(int line, string message, System.Exception inner) : base(line, message, inner) { }
    protected BSMarkerNotFoundException(
        System.Runtime.Serialization.SerializationInfo info,
        System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
}