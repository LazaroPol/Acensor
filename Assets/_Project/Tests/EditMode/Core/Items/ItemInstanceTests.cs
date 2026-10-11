using System;
using NUnit.Framework;
using Specimen.Core.Items;

namespace Specimen.Tests.Core.Items
{
    [TestFixture]
    public class ItemInstanceTests
    {
        private static readonly ItemDefinition Pick =
            new ItemDefinition("pico", "Pico", ItemCategory.Tool, Rarity.Rare, 20);

        [Test]
        public void CreateNewStartsAtRarityMaxDurability()
        {
            var item = ItemInstance.CreateNew(Pick);
            Assert.That(item.Durability.Max, Is.EqualTo(30));
            Assert.That(item.Durability.Current, Is.EqualTo(30));
            Assert.That(item.Id, Is.EqualTo("pico"));
            Assert.That(item.Rarity, Is.EqualTo(Rarity.Rare));
        }

        [Test]
        public void BrokenItemIsNotUsableUntilRepaired()
        {
            var item = ItemInstance.CreateNew(Pick);
            item.Durability.Wear(30);
            Assert.That(item.IsUsable, Is.False);
            item.Durability.Repair(1);
            Assert.That(item.IsUsable, Is.True);
        }

        [Test]
        public void EachInstanceWearsIndependently()
        {
            var a = ItemInstance.CreateNew(Pick);
            var b = ItemInstance.CreateNew(Pick);
            a.Durability.Wear(10);
            Assert.That(b.Durability.Current, Is.EqualTo(30));
        }

        [Test]
        public void NonDegradableItemIsAlwaysUsable()
        {
            var gem = new ItemDefinition("gema", "Gema", ItemCategory.Material, Rarity.Legendary, maxStack: 5);
            var item = ItemInstance.CreateNew(gem);
            item.Durability.Wear(999);
            Assert.That(item.IsUsable, Is.True);
        }

        [Test]
        public void NullArgumentsThrow()
        {
            Assert.Throws<ArgumentNullException>(() => ItemInstance.CreateNew(null));
            Assert.Throws<ArgumentNullException>(() => new ItemInstance(Pick, null));
        }
    }
}
