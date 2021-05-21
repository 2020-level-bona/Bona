using System.ComponentModel;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GiveCommand : IActionCommand
{
    public Item item;

    public const string Keyword = "GIVE";
    public bool Blocking => false;
    public int LineNumber {get;}

    public GiveCommand(ItemType itemType, int itemCount = 1) {
        item = new Item(itemType, itemCount);
    }

    public GiveCommand(CommandLineParser lineParser) {
        LineNumber = lineParser.lineNumber;

        ItemType itemType = lineParser.GetItemType(1);
        int itemCount = lineParser.GetInt(2);

        item = new Item(itemType, itemCount);
    }

    public IEnumerator GetCoroutine() {
        Game.Instance.inventory.AddItem(item);

        yield return null;
    }
}
