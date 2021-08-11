using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Inventory {
    public const int NUM_SLOTS = 10;

    Item[] items = new Item[NUM_SLOTS];

    IInventoryRenderer inventoryRenderer;

    public int selectedSlot = 0;

    public Inventory() {
        for (int i = 0; i < NUM_SLOTS; i++) {
            if (Session.Inventory.Contains(i.ToString()))
                items[i] = Session.Inventory.GetItem(i.ToString());
        }

        EventManager.Instance.OnPreSave += Save;
    }

    public void AddItem(Item item) {
        for (int i = 0; i < NUM_SLOTS; i++) {
            if (items[i] != null && items[i].type == item.type && item.IsStackable()) {
                items[i].quantity += item.quantity;
                break;
            }
            if(items[i] == null) {
                items[i] = item.Clone() as Item;
                break;
            }
        }

        inventoryRenderer?.Invalidate();
    }

    public bool ContainsItem(Item item) {
        int count = item.quantity;
        for (int i = 0; i < NUM_SLOTS; i++) {
            if (items[i] != null && items[i].type == item.type) {
                count -= items[i].quantity;
                if (count <= 0)
                    return true;
            }
        }
        return false;
    }

    public void RemoveItem(Item item) {
        int count = item.quantity;
        for (int i = 0; i < NUM_SLOTS; i++) {
            if (items[i] != null && items[i].type == item.type) {
                int decrease = Mathf.Min(items[i].quantity, count);

                items[i].quantity -= decrease;
                count -= decrease;

                if (items[i].quantity <= 0)
                    items[i] = null;
                
                if (count <= 0)
                    break;
            }
        }

        inventoryRenderer?.Invalidate();
    }

    public Item[] GetContents() {
        return items.Clone() as Item[];
    }

    public void Clear() {
        items = new Item[NUM_SLOTS];

        inventoryRenderer?.Invalidate();
    }

    public void SetInventoryRenderer(IInventoryRenderer inventoryRenderer) {
        this.inventoryRenderer = inventoryRenderer;
    }

    void Save() {
        for (int i = 0; i < NUM_SLOTS; i++) {
            if (items[i] != null)
                Session.Inventory.Set(i.ToString(), items[i]);
            else
                Session.Inventory.Remove(i.ToString());
        }
    }

    public Item GetSelectedItem() {
        return items[selectedSlot];
    }
}
