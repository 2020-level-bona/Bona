using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BSSyntaxException : BSException
{
    public BSSyntaxException() { }
    public BSSyntaxException(string message) : base(message) { }
    public BSSyntaxException(string message, System.Exception inner) : base(message, inner) { }
    protected BSSyntaxException(
        System.Runtime.Serialization.SerializationInfo info,
        System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
}
