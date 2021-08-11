using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chat
{
    public string Message;
    public bool Global = true;
    public bool Displaying = true;
    public string IsPrompt = null; // null이 아니면 플레이어가 선택한 결과를 해당 변수에 저장

    public Chat(string Message, string IsPrompt = null, bool Global = true) {
        Message = Message.Replace("\\n", "\n");
        this.Message = $"\"{Message}\"";
        this.IsPrompt = IsPrompt;
        this.Global = Global;
    }
}
