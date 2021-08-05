using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorManager : MonoBehaviour
{
    public Texture2D normalCursorTexture, hoverCursorTexture;
    bool hovered = false;
    Camera cam;

    void Awake() {
        Cursor.SetCursor(normalCursorTexture, Vector2.zero, CursorMode.Auto);
        cam = Camera.main;
    }

    void Update() {
        bool newHovered = false;
        foreach (MouseClickTrigger trigger in FindObjectsOfType<MouseClickTrigger>()) {
            if (trigger.coll.OverlapPoint(cam.ScreenToWorldPoint(Input.mousePosition))) {
                newHovered = true;
                break;
            }
        }
        if (hovered != newHovered) {
            Cursor.SetCursor(newHovered ? hoverCursorTexture : normalCursorTexture, Vector2.zero, CursorMode.Auto);
            hovered = newHovered;
        }
    }
}
