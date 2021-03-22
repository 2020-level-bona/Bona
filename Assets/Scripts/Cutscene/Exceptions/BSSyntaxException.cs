using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BSSyntaxException : BSException
{
    public BSSyntaxException(int line) : base(line) { }
    public BSSyntaxException(int line, string message) : base(line, message) { }
    public BSSyntaxException(int line, string message, System.Exception inner) : base(line, message, inner) { }
    protected BSSyntaxException(
        System.Runtime.Serialization.SerializationInfo info,
        System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
}
