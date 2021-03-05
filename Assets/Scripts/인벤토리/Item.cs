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

    public override bool Equals(object obj)
    {
        return this.Equals(obj as Item);
    }

    public bool Equals(Item item) {
        if (System.Object.ReferenceEquals(item, null))
            return false;
        
        if (System.Object.ReferenceEquals(this, item))
            return true;
        
        if (this.GetType() != item.GetType())
            return false;
        
        return type == item.type && quantity == item.quantity;
    }

    public override int GetHashCode()
    {
        return (int) type * 1000 + quantity;
    }

    public static bool operator ==(Item lhs, Item rhs) {
        if (System.Object.ReferenceEquals(lhs, null)) {
            if (System.Object.ReferenceEquals(rhs, null))
                return true;
            return false;
        }
        return lhs.Equals(rhs);
    }

    public static bool operator !=(Item lhs, Item rhs) {
        return !(lhs == rhs);
    }
}
