using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chat
{
    // 캐릭터의 머리 위에서 얼마만큼의 높이를 더한 지점에 대화를 띄울지 결정한다. 0인 경우 머리 바로 위에 대화를 띄운다.
    public const float CHATBOX_ADDITIONAL_HEIGHT = 0.3f;

    public string Message;

    public Character Character;

    Vector2 lastKnownPosition;

    public Chat(string Message, Character Character) {
        this.Message = $"\"{Message}\"";
        this.Character = Character;

        lastKnownPosition = GetAnchorPosition();
    }

    public Vector2 GetAnchorPosition() {
        if (Character) {
            Bounds bounds = Character.GetBounds();
            return new Vector2((bounds.min.x + bounds.max.x) / 2f, bounds.max.y + CHATBOX_ADDITIONAL_HEIGHT);
        }
        return lastKnownPosition;
    }
}
