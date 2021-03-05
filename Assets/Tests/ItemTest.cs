using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests {
    public class ItemTest {
        [Test]
        public void 아이템_복사() {
            Item a = new Item(ItemType.WATERFAIRY, 3);
            Item b = a.Clone() as Item;

            Assert.False(Object.ReferenceEquals(a, b));
            Assert.True(a == b);
        }

        [Test]
        public void 아이템_비교() {
            Item a = new Item(ItemType.WATERFAIRY, 1);
            Item b = new Item(ItemType.WATERFAIRY, 2);
            Item c = new Item(ItemType.DUMMY, 3);
            Item d = new Item(ItemType.DUMMY, 3);

            Assert.False(a == b);
            Assert.False(a == c);
            Assert.True(c == d);
        }

        [Test]
        public void 아이템_null_비교() {
            Item a = new Item(ItemType.DUMMY);

            Assert.False(a == null);
            Assert.False(null == a);
        }
    }
}