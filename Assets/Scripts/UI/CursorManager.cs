﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorManager : MonoBehaviour
{
    public Texture2D normalCursorTexture, hoverCursorTexture;
    bool hovered = false;
    Camera cam;
    Game game;

    void Awake() {
        Cursor.SetCursor(normalCursorTexture, Vector2.zero, CursorMode.ForceSoftware);
        cam = Camera.main;
        game = FindObjectOfType<Game>();
    }

    void Update() {
        bool newHovered = false;
        if (game.overlayCanvas == 0) {
            foreach (MouseClickTrigger trigger in FindObjectsOfType<MouseClickTrigger>()) {
                Switcher switcher = trigger.GetComponent<Switcher>();
                if (switcher && switcher.hided) continue;
                if (trigger.coll.OverlapPoint(cam.ScreenToWorldPoint(Input.mousePosition))) {
                    newHovered = true;
                    break;
                }
            }
        }
        
        if (hovered != newHovered) {
            Cursor.SetCursor(newHovered ? hoverCursorTexture : normalCursorTexture, Vector2.zero, CursorMode.ForceSoftware);
            hovered = newHovered;
        }
    }
}
