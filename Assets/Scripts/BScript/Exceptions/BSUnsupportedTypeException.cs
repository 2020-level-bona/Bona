using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class BSUnsupportedTypeException : BSException
{
    public BSUnsupportedTypeException(int line) : base(line) { }
    public BSUnsupportedTypeException(int line, string message) : base(line, message) { }
    public BSUnsupportedTypeException(int line, string message, System.Exception inner) : base(line, message, inner) { }
    protected BSUnsupportedTypeException(
        System.Runtime.Serialization.SerializationInfo info,
        System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
}
