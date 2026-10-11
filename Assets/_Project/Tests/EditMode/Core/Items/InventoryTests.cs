using System;
using NUnit.Framework;
using Specimen.Core.Items;

namespace Specimen.Tests.Core.Items
{
    [TestFixture]
    public class InventoryTests
    {
        private static readonly ItemDefinition Ore =
            new ItemDefinition("mena", "Mena", ItemCategory.Material, Rarity.Common, 0, 10);
        private static readonly ItemDefinition Pick =
            new ItemDefinition("pico", "Pico", ItemCategory.Tool, Rarity.Common, 20);

        private static ItemInstance OreItem => ItemInstance.CreateNew(Ore);

        [Test]
        public void StartsEmptyWithGivenCapacity()
        {
            var inv = new Inventory(4);
            Assert.That(inv.Capacity, Is.EqualTo(4));
            Assert.That(inv.FreeSlots, Is.EqualTo(4));
            Assert.That(inv.UsedSlots, Is.Zero);
        }

        [Test]
        public void RejectsNonPositiveCapacity()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => new Inventory(0));
        }

        [Test]
        public void StackableItemsFillExistingStackBeforeNewSlot()
        {
            var inv = new Inventory(3);
            inv.Add(OreItem, 6);
            inv.Add(OreItem, 6);
            Assert.That(inv.UsedSlots, Is.EqualTo(2));
            Assert.That(inv.Stacks[0].Quantity, Is.EqualTo(10));
            Assert.That(inv.Stacks[1].Quantity, Is.EqualTo(2));
            Assert.That(inv.CountOf("mena"), Is.EqualTo(12));
        }

        [Test]
        public void AddReturnsOnlyWhatFits()
        {
            var inv = new Inventory(2);
            Assert.That(inv.Add(OreItem, 25), Is.EqualTo(20));
            Assert.That(inv.FreeSlots, Is.Zero);
            Assert.That(inv.Add(OreItem), Is.Zero);
        }

        [Test]
        public void SpaceForCountsFreeSlotsAndPartialStacks()
        {
            var inv = new Inventory(3);
            inv.Add(OreItem, 7);
            inv.Add(ItemInstance.CreateNew(Pick));
            Assert.That(inv.SpaceFor(OreItem), Is.EqualTo(13));
            Assert.That(inv.SpaceFor(ItemInstance.CreateNew(Pick)), Is.EqualTo(1));
        }

        [Test]
        public void DegradableItemsTakeOneSlotEach()
        {
            var inv = new Inventory(3);
            inv.Add(ItemInstance.CreateNew(Pick));
            inv.Add(ItemInstance.CreateNew(Pick));
            Assert.That(inv.UsedSlots, Is.EqualTo(2));
            Assert.That(inv.CountOf("pico"), Is.EqualTo(2));
        }

        [Test]
        public void DegradableInstanceCannotBeDuplicated()
        {
            var inv = new Inventory(3);
            var pick = ItemInstance.CreateNew(Pick);
            Assert.Throws<ArgumentOutOfRangeException>(() => inv.Add(pick, 2));
            inv.Add(pick);
            Assert.Throws<InvalidOperationException>(() => inv.Add(pick));
        }

        [Test]
        public void RemoveEmptiesNewestStacksAndFreesSlots()
        {
            var inv = new Inventory(3);
            inv.Add(OreItem, 15);
            Assert.That(inv.Remove("mena", 7), Is.EqualTo(7));
            Assert.That(inv.UsedSlots, Is.EqualTo(1));
            Assert.That(inv.CountOf("mena"), Is.EqualTo(8));
        }

        [Test]
        public void RemoveReturnsOnlyWhatExists()
        {
            var inv = new Inventory(2);
            inv.Add(OreItem, 3);
            Assert.That(inv.Remove("mena", 5), Is.EqualTo(3));
            Assert.That(inv.Remove("nada"), Is.Zero);
            Assert.That(inv.UsedSlots, Is.Zero);
        }

        [Test]
        public void RemoveInstanceTakesThatExactItem()
        {
            var inv = new Inventory(3);
            var worn = ItemInstance.CreateNew(Pick);
            inv.Add(ItemInstance.CreateNew(Pick));
            inv.Add(worn);
            Assert.That(inv.RemoveInstance(worn), Is.True);
            Assert.That(inv.Stacks[0].Item, Is.Not.SameAs(worn));
            Assert.That(inv.RemoveInstance(worn), Is.False);
        }
    }
}
