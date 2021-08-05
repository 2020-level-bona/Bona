using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryUI : MonoBehaviour, ISlotListener, IInventoryRenderer
{
    public GameObject InventoryUIPanel;
    public GameObject slotPrefab;
    public Transform slotParent;

    bool activeInventory = false;

    bool invalidated = true;
    
    Inventory inventory;

    Slot[] slots;
    
    private void Start() {
        inventory = FindObjectOfType<Game>().inventory;
        inventory.SetInventoryRenderer(this);

        slots = new Slot[Inventory.NUM_SLOTS];
        for (int i = 0; i < Inventory.NUM_SLOTS; i++) {
            GameObject gameObject = Instantiate(slotPrefab);
            slots[i] = gameObject.GetComponent<Slot>();
            slots[i].Initialize(i, this);

            gameObject.transform.SetParent(slotParent);
            gameObject.transform.localScale = Vector3.one; // WTF?
        }

        InventoryUIPanel.SetActive(activeInventory);
    }
    
    private void Update() {
        if(Input.GetKeyDown(KeyCode.I)) {
            activeInventory = !activeInventory;
            InventoryUIPanel.SetActive(activeInventory);
        }

        if (invalidated)
            DrawItems();
    }

    public void OnSlotClick(int index) {
        // TODO
        for (int i = 0; i < slots.Length; i++) {
            slots[i].SetSelected(i == index);
        }
    }

    public void Invalidate() {
        invalidated = true;
    }

    void DrawItems() {
        Item[] contents = inventory.GetContents();
        for (int i = 0; i < Inventory.NUM_SLOTS; i++) {
            slots[i].DrawItem(contents[i]);
        }

        invalidated = false;
    }
}
