using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ItemDefinition")]
public class ItemDefinition : ScriptableObject
{
    public ItemType itemType;

    public Sprite sprite;

    public bool stackable;
}
