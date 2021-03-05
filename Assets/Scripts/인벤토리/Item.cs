using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : ICloneable
{
    public readonly ItemType type;

    public int quantity;

    public Item(ItemType type, int quantity = 1) {
        this.type = type;
        this.quantity = quantity;
    }

    public Sprite GetSprite() {
        return ItemFactory.Instance.GetItemDefinition(type).sprite;
    }

    public bool IsStackable() {
        return ItemFactory.Instance.GetItemDefinition(type).stackable;
    }

    public object Clone() {
        return MemberwiseClone();
    }
}
