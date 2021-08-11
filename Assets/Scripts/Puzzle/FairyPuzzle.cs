using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FairyPuzzle : MonoBehaviour
{
    Image[] images;
    int[,] arr = new int[3, 3];
    bool done = false;

    void Start() {
        images = gameObject.GetComponentsInChildren<Image>();
        for (int i = 0; i < images.Length; i++) {
            int y = i / 3;
            int x = i % 3;
            images[i].gameObject.GetComponent<Button>().onClick.AddListener(() => Action(y, x));
            arr[y, x] = i;
        }
        arr[2, 2] = -1;

        System.Random random = new System.Random(227);
        List<Vector2Int> candidates = new List<Vector2Int>();
        for (int t = 0; t < 70; t++) {
            candidates.Clear();
            for (int i = 0; i < 3; i++) {
                for (int j = 0; j < 3; j++) {
                    if (CanAction(i, j)) candidates.Add(new Vector2Int(i, j));
                }
            }
            Vector2Int selected = candidates[random.Next(candidates.Count)];
            Action(selected.y, selected.x, false);
        }
    }
    
    void Swap(int y1, int x1, int y2, int x2) {
        int t = arr[y1, x1];
        arr[y1, x1] = arr[y2, x2];
        arr[y2, x2] = t;
        Sprite st = images[y1 * 3 + x1].sprite;
        images[y1 * 3 + x1].sprite = images[y2 * 3 + x2].sprite;
        images[y2 * 3 + x2].sprite = st;
        Color ct = images[y1 * 3 + x1].color;
        images[y1 * 3 + x1].color = images[y2 * 3 + x2].color;
        images[y2 * 3 + x2].color = ct;
    }

    bool CanAction(int y, int x) {
        return (y > 0 && arr[y-1, x] == -1) || (y < 2 && arr[y+1, x] == -1) || (x > 0 && arr[y, x-1] == -1) || (x < 2 && arr[y, x+1] == -1);
    }

    void Action(int y, int x, bool check = true) {
        if (done) return;
        if (y > 0 && arr[y-1, x] == -1) Swap(y-1, x, y, x);
        else if (y < 2 && arr[y+1, x] == -1) Swap(y+1, x, y, x);
        else if (x > 0 && arr[y, x-1] == -1) Swap(y, x-1, y, x);
        else if (x < 2 && arr[y, x+1] == -1) Swap(y, x+1, y, x);

        if (check) {
            bool pass = true;
            for (int i = 0; i < 3; i++) {
                for (int j = 0; j < 3; j++) {
                    if (i == 2 && j == 2) continue;
                    if (arr[i, j] != i * 3 + j) pass = false;
                }
            }
            if (pass) {
                PuzzleUI puzzleUI = GetComponentInParent<PuzzleUI>();
                puzzleUI.OnSolve();
                done = true;
            }
        }
    }
}
