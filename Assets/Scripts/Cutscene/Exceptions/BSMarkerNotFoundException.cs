using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BSMarkerNotFoundException : BSException
{
    public BSMarkerNotFoundException() { }
    public BSMarkerNotFoundException(string message) : base(message) { }
    public BSMarkerNotFoundException(string message, System.Exception inner) : base(message, inner) { }
    protected BSMarkerNotFoundException(
        System.Runtime.Serialization.SerializationInfo info,
        System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
}