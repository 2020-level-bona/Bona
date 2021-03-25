using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemFactory
{
    static ItemFactory instance;

    public static ItemFactory Instance {
        get {
            if (instance == null) {
                instance = new ItemFactory();
            }
            return instance;
        }
    }

    Dictionary<ItemType, ItemDefinition> itemTable;

    private ItemFactory() {
        itemTable = new Dictionary<ItemType, ItemDefinition>();

        Resources.LoadAll<ItemDefinition>("ItemDefs");
        
        foreach (ItemDefinition itemDefinition in Resources.FindObjectsOfTypeAll<ItemDefinition>()) {
            itemTable.Add(itemDefinition.itemType, itemDefinition);
        }
    }

    public ItemDefinition GetItemDefinition(ItemType type) {
        return itemTable[type];
    }
}
