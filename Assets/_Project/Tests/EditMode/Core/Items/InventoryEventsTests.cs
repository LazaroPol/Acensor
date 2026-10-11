using System;
using NUnit.Framework;
using Specimen.Core.Items;

namespace Specimen.Tests.Core.Items
{
    [TestFixture]
    public class InventoryEventsTests
    {
        private static readonly ItemDefinition Herb =
            new ItemDefinition("hierba", "Hierba", ItemCategory.Material, Rarity.Common, 0, 5);

        [Test]
        public void ChangedFiresOnlyWhenContentMoves()
        {
            var inv = new Inventory(1);
            int calls = 0;
            inv.Changed += () => calls++;
            inv.Add(ItemInstance.CreateNew(Herb), 5);
            inv.Add(ItemInstance.CreateNew(Herb));
            inv.Remove("otra");
            Assert.That(calls, Is.EqualTo(1));
            inv.Remove("hierba");
            Assert.That(calls, Is.EqualTo(2));
        }

        [Test]
        public void StackRejectsQuantityAboveMax()
        {
            Assert.Throws<ArgumentOutOfRangeException>(
                () => new ItemStack(ItemInstance.CreateNew(Herb), 6));
        }

        [Test]
        public void StackMergesOnlySameStackableItem()
        {
            var stack = new ItemStack(ItemInstance.CreateNew(Herb), 2);
            var other = new ItemDefinition("raiz", "Raíz", ItemCategory.Material, Rarity.Common, 0, 5);
            Assert.That(stack.CanMerge(ItemInstance.CreateNew(Herb)), Is.True);
            Assert.That(stack.CanMerge(ItemInstance.CreateNew(other)), Is.False);
            Assert.That(stack.Space, Is.EqualTo(3));
        }

        [Test]
        public void InvalidQuantitiesThrow()
        {
            var inv = new Inventory(1);
            Assert.Throws<ArgumentOutOfRangeException>(() => inv.Add(ItemInstance.CreateNew(Herb), 0));
            Assert.Throws<ArgumentOutOfRangeException>(() => inv.Remove("hierba", -1));
            Assert.Throws<ArgumentNullException>(() => inv.Add(null));
        }
    }
}
