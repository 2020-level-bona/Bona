using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryUI : MonoBehaviour, ISlotListener
{
    public GameObject InventoryUIPanel;
    public GameObject slotPrefab;
    public Transform slotParent;

    bool activeInventory = false;
    
    private void Start() {
        Player player = FindObjectOfType<Player>();
        Inventory inventory = player.inventory;

        for (int i = 0; i < Inventory.NUM_SLOTS; i++) {
            GameObject gameObject = Instantiate(slotPrefab);
            Slot slot = gameObject.GetComponent<Slot>();
            slot.Initialize(i, this);

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
    }

    public void OnSlotClick(int index) {
        // TODO
    }
}
