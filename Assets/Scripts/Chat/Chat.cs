using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chat
{
    public string Message;
    public bool Global = true;
    public bool Displaying = true;

    public Chat(string Message, bool Global = true) {
        this.Message = $"\"{Message}\"";
        this.Global = Global;
    }
}
