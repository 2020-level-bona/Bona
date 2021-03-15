using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests {
    public class InventoryTest {
        [Test]
        public void 인벤토리_아이템_추가() {
            Inventory inventory = new Inventory();

            inventory.AddItem(new Item(ItemType.WATERFAIRY));
            inventory.AddItem(new Item(ItemType.WATERFAIRY, 3));
            inventory.AddItem(new Item(ItemType.DUMMY));
            inventory.AddItem(new Item(ItemType.WATERFAIRY, 2));

            Item[] contents = inventory.GetContents();

            Assert.True(contents[0] == new Item(ItemType.WATERFAIRY, 6));
            Assert.True(contents[1] == new Item(ItemType.DUMMY));
            Assert.True(contents[2] == null);
        }

        [Test]
        public void 인벤토리_Stackable_추가() {
            Inventory inventory = new Inventory();

            Assert.True(new Item(ItemType.WATERFAIRY).IsStackable());
            Assert.False(new Item(ItemType.DUMMY).IsStackable());

            inventory.AddItem(new Item(ItemType.DUMMY));
            inventory.AddItem(new Item(ItemType.DUMMY));
            inventory.AddItem(new Item(ItemType.WATERFAIRY));
            inventory.AddItem(new Item(ItemType.WATERFAIRY));
            
            Item[] contents = inventory.GetContents();

            Assert.True(contents[0] == new Item(ItemType.DUMMY));
            Assert.True(contents[1] == new Item(ItemType.DUMMY));
            Assert.True(contents[2] == new Item(ItemType.WATERFAIRY, 2));
            Assert.True(contents[3] == null);
        }

        [Test]
        public void 인벤토리_Contains() {
            Inventory inventory = new Inventory();

            inventory.AddItem(new Item(ItemType.WATERFAIRY, 3));
            inventory.AddItem(new Item(ItemType.DUMMY));

            Assert.True(inventory.ContainsItem(new Item(ItemType.WATERFAIRY, 2)));
            Assert.True(inventory.ContainsItem(new Item(ItemType.WATERFAIRY, 3)));
            Assert.False(inventory.ContainsItem(new Item(ItemType.WATERFAIRY, 4)));

            Assert.True(inventory.ContainsItem(new Item(ItemType.DUMMY)));
            Assert.False(inventory.ContainsItem(new Item(ItemType.DUMMY, 2)));
        }

        [Test]
        public void 인벤토리_초기화() {
            Inventory inventory = new Inventory();

            inventory.AddItem(new Item(ItemType.WATERFAIRY));
            inventory.AddItem(new Item(ItemType.DUMMY));

            Assert.True(inventory.ContainsItem(new Item(ItemType.WATERFAIRY)));
            Assert.True(inventory.ContainsItem(new Item(ItemType.DUMMY)));

            inventory.Clear();

            Assert.False(inventory.ContainsItem(new Item(ItemType.WATERFAIRY)));
            Assert.False(inventory.ContainsItem(new Item(ItemType.DUMMY)));
        }

        [Test]
        public void 인벤토리_제거() {
            Inventory inventory = new Inventory();

            inventory.AddItem(new Item(ItemType.WATERFAIRY, 3));
            inventory.AddItem(new Item(ItemType.DUMMY));

            inventory.RemoveItem(new Item(ItemType.WATERFAIRY, 1));

            Assert.True(inventory.ContainsItem(new Item(ItemType.WATERFAIRY, 2)));
            Assert.False(inventory.ContainsItem(new Item(ItemType.WATERFAIRY, 3)));

            inventory.RemoveItem(new Item(ItemType.WATERFAIRY, 100));

            Assert.False(inventory.ContainsItem(new Item(ItemType.WATERFAIRY)));

            inventory.RemoveItem(new Item(ItemType.DUMMY));

            Assert.False(inventory.ContainsItem(new Item(ItemType.DUMMY)));
        }
    }
}