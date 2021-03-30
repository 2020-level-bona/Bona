using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BSException : System.Exception
{
    public int line;
    public BSException(int line) {
        this.line = line;
    }
    public BSException(int line, string message) : base(message) {
        this.line = line;
    }
    public BSException(int line, string message, System.Exception inner) : base(message, inner) {
        this.line = line;
    }
    protected BSException(
        System.Runtime.Serialization.SerializationInfo info,
        System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
}
