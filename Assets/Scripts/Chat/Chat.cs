using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chat
{
    public string Message;

    public Transform Anchor;

    public Chat(string Message, Transform Anchor) {
        this.Message = $"\"{Message}\"";
        this.Anchor = Anchor;
    }

}
