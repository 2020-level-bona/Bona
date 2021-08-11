using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AntPuzzle : MonoBehaviour
{
    Image[] images;

    int[] count = new int[25];
    bool done = false;

    void Start() {
        images = gameObject.GetComponentsInChildren<Image>();
        System.Random random = new System.Random(227);
        for (int i = 0; i < images.Length; i++) {
            Image image = images[i];
            Button button = image.gameObject.GetComponent<Button>();
            button.onClick.AddListener(() => Rotate(image));
            int cnt = random.Next(4);
            for (int j = 0; j < cnt; j++) image.rectTransform.Rotate(Vector3.forward, 90f);
            count[i] = cnt;
        }
    }

    void Rotate(Image image) {
        if (done) return;
        image.rectTransform.Rotate(Vector3.forward, 90f);
        bool pass = true;
        for (int i = 0; i < 25; i++) {
            if (count[i] != 0) {
                pass = false;
                break;
            }
        }
        if (pass) {
            PuzzleUI puzzleUI = GetComponentInParent<PuzzleUI>();
            puzzleUI.OnSolve();
            done = true;
        }
    }
}
