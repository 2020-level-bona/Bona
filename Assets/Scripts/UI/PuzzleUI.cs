using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleUI : MonoBehaviour
{
    public string variablePath;
    Game game;

    void Awake() {
        game = FindObjectOfType<Game>();
    }

    void Start() {
        if (game != null) game.overlayCanvas++;
    }

    public void OnSolve() {
        Session.Instance.Set(variablePath, true);
        StartCoroutine(Close());
    }

    public void OnClose() {
        Destroy(gameObject);
        game.overlayCanvas--;
    }

    IEnumerator Close() {
        yield return new WaitForSeconds(2);
        OnClose();
    }
}
