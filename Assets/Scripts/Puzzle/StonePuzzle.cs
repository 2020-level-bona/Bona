using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StonePuzzle : MonoBehaviour
{
    public RectTransform[] positions;
    public Image[] stoneImages;
    Canvas canvas;
    [HideInInspector] List<int>[] adj = new List<int>[14];
    // 네모, 세모, 하트, 동그라미, 별
    [HideInInspector] int[] stoneIndex = {9, 13, 11, 10, 8};
    [HideInInspector] int[] stoneDest = {0, 1, 2, 13, 12};
    [HideInInspector] int clickedStone = -1;
    [HideInInspector] bool done = false;

    void AddEdge(int i, int j) {
        adj[i].Add(j);
        adj[j].Add(i);
    }

    void Start() {
        canvas = GetComponentInParent<Canvas>();

        for (int i = 0; i < 14; i++) adj[i] = new List<int>();

        AddEdge(0, 3);
        AddEdge(3, 4);
        AddEdge(4, 11);
        AddEdge(11, 13);
        AddEdge(4, 5);
        AddEdge(5, 6);
        AddEdge(6, 1);
        AddEdge(6, 7);
        AddEdge(7, 8);
        AddEdge(8, 9);
        AddEdge(9, 2);
        AddEdge(9, 12);
        AddEdge(9, 10);
    }

    int GetMouseIndex() {
        Vector2 mousePos;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(canvas.transform as RectTransform, Input.mousePosition, canvas.worldCamera, out mousePos);

        for (int i = 0; i < positions.Length; i++) if (positions[i].rect.Contains((Vector2) mousePos - positions[i].anchoredPosition)) return i;
        return -1;
    }

    void TryToMoveStone(int stone, int index) {
        if (done) return;
        for (int i = 0; i < 5; i++) if (stoneIndex[i] == index) return;
        stoneIndex[stone] = index;
        stoneImages[stone].rectTransform.anchoredPosition = positions[index].anchoredPosition;
        if (Check()) {
            PuzzleUI puzzleUI = GetComponentInParent<PuzzleUI>();
            puzzleUI.OnSolve();
            done = true;
        }
    }

    bool Check() {
        for (int i = 0; i < 5; i++) if (stoneIndex[i] != stoneDest[i]) return false;
        return true;
    }

    void Update() {
        int mouseIndex = GetMouseIndex();
        if (Input.GetMouseButtonDown(0)) {
            clickedStone = -1;
            for (int i = 0; i < 5; i++) if (stoneIndex[i] == mouseIndex) clickedStone = i;
        }
        if (Input.GetMouseButton(0) && clickedStone != -1) {
            if (adj[stoneIndex[clickedStone]].Contains(mouseIndex)) TryToMoveStone(clickedStone, mouseIndex);
        }
    }
}
