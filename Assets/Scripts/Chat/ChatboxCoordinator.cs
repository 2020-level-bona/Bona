using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChatboxCoordinator
{

    Vector2 canvasSize;
    Camera camera;

    public ChatboxCoordinator(RectTransform canvasRectTransform, Camera camera) {
        canvasSize = canvasRectTransform.rect.size;
        this.camera = camera;
    }

    public Vector2 CalculateRectPosition(Vector2 lowerCenterWorldPos, Vector2 chatboxSize) {
        Vector2 lowerCenterScreenPos = camera.WorldToScreenPoint(lowerCenterWorldPos);
        float x = Mathf.Clamp(lowerCenterScreenPos.x, chatboxSize.x / 2f, canvasSize.x - chatboxSize.x / 2f);
        float y = Mathf.Clamp(lowerCenterScreenPos.y, 0, canvasSize.y - chatboxSize.y);
        return new Vector2(x, y);
    }

}
