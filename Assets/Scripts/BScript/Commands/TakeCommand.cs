using System.ComponentModel;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TakeCommand : IActionCommand
{
    public Item item;

    public const string Keyword = "TAKE";
    public bool Blocking => false;
    public int LineNumber {get;}

    public TakeCommand(ItemType itemType, int itemCount = 1) {
        item = new Item(itemType, itemCount);
    }

    public TakeCommand(CommandLineParser lineParser) {
        LineNumber = lineParser.lineNumber;

        ItemType itemType = lineParser.GetItemType(1);
        int itemCount = lineParser.GetInt(2);

        item = new Item(itemType, itemCount);
    }

    public IEnumerator GetCoroutine() {
        Game.Instance.inventory.RemoveItem(item);

        yield return null;
    }
}
