using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SlidePuzzle : MonoBehaviour
{
    public RectTransform[] positions;
    public RectTransform[] blockImages;
    Canvas canvas;
    int clickedBlock = -1;
    Vector2Int clickOffset;
    bool done = false;
    List<Block> blocks = new List<Block>();
    bool[,] blocked = new bool[5, 6];

    const int SIZE_X = 5;
    const int SIZE_Y = 6;

    void Start() {
        canvas = GetComponentInParent<Canvas>();

        blocks.Add(new Block(0, 0, 1, 1, MoveType.FREE));
        blocks.Add(new Block(1, 0, 1, 2, MoveType.VERTICAL));
        blocks.Add(new Block(2, 0, 2, 1, MoveType.HORIZONTAL));
        blocks.Add(new Block(4, 1, 1, 2, MoveType.VERTICAL));
        blocks.Add(new Block(1, 2, 3, 1, MoveType.HORIZONTAL));
        blocks.Add(new Block(0, 3, 1, 2, MoveType.VERTICAL));
        blocks.Add(new Block(1, 3, 2, 1, MoveType.HORIZONTAL));
        blocks.Add(new Block(3, 3, 1, 2, MoveType.VERTICAL));
        blocks.Add(new Block(1, 4, 2, 1, MoveType.HORIZONTAL));
        
        blocked[0, 5] = blocked[1, 5] = blocked[3, 5] = blocked[4, 5] = true;
    }

    Vector2Int GetMousePosition() {
        Vector2 mousePos;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(canvas.transform as RectTransform, Input.mousePosition, canvas.worldCamera, out mousePos);

        for (int i = 0; i < positions.Length; i++) if (positions[i].rect.Contains((Vector2) mousePos - positions[i].anchoredPosition)) return new Vector2Int(i % SIZE_X, i / SIZE_X);
        return new Vector2Int(-1, -1);
    }

    int GetBlockAt(int x, int y) {
        for (int i = 0; i < blocks.Count; i++) if (blocks[i].Contains(x, y)) return i;
        return -1;
    }

    void TryToMoveBlock(int i, int x, int y) {
        Block block = blocks[i];
        if (Mathf.Abs(x - block.x) + Mathf.Abs(y - block.y) != 1) return;
        if (block.moveType == MoveType.HORIZONTAL && Mathf.Abs(y - block.y) > 0) return;
        if (block.moveType == MoveType.VERTICAL && Mathf.Abs(x - block.x) > 0) return;
        if (x < 0 || x + block.sizeX > SIZE_X || y < 0 || y + block.sizeY > SIZE_Y) return;
        for (int j = 0; j < block.sizeX; j++) for (int k = 0; k < block.sizeY; k++) {
            if (blocked[x + j, y + k]) return;
            if (GetBlockAt(x + j, y + k) != -1 && GetBlockAt(x + j, y + k) != i) return;
        }

        block.x = x;
        block.y = y;
        blockImages[i].anchoredPosition = positions[x + y * SIZE_X].anchoredPosition;

        if (Check()) {
            PuzzleUI puzzleUI = GetComponentInParent<PuzzleUI>();
            puzzleUI.OnSolve();
            done = true;
        }
    }

    bool Check() {
        return blocks[0].x == 2 && blocks[0].y == 5;
    }

    void Update() {
        Vector2Int mousePos = GetMousePosition();
        if (Input.GetMouseButtonDown(0)) {
            clickedBlock = GetBlockAt(mousePos.x, mousePos.y);
            if (clickedBlock != -1) clickOffset = new Vector2Int(mousePos.x - blocks[clickedBlock].x, mousePos.y - blocks[clickedBlock].y);
        }
        if (Input.GetMouseButton(0) && clickedBlock != -1) {
            TryToMoveBlock(clickedBlock, mousePos.x - clickOffset.x, mousePos.y - clickOffset.y);
        }
    }
}

class Block {
    public int x, y;
    public int sizeX, sizeY;
    public MoveType moveType;
    public Block(int x, int y, int sizeX, int sizeY, MoveType moveType) {
        this.x = x;
        this.y = y;
        this.sizeX = sizeX;
        this.sizeY = sizeY;
        this.moveType = moveType;
    }

    public bool Contains(int px, int py) {
        return x <= px && px < x + sizeX && y <= py && py < y + sizeY;
    }
}

enum MoveType {
    HORIZONTAL, VERTICAL, FREE
}