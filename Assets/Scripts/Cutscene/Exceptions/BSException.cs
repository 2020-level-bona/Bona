using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BSException : System.Exception
{
    public BSException() { }
    public BSException(string message) : base(message) { }
    public BSException(string message, System.Exception inner) : base(message, inner) { }
    protected BSException(
        System.Runtime.Serialization.SerializationInfo info,
        System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
}
