using System;
using NUnit.Framework;
using Specimen.Core.Items;
using Specimen.Core.Loot;

namespace Specimen.Tests.Core.Loot
{
    [TestFixture]
    public class LootEntryTests
    {
        [Test]
        public void AvailableFromMinRing()
        {
            var entry = new LootEntry(LootFixtures.Relic, 1f, minRing: 2);
            Assert.That(entry.AvailableIn(1), Is.False);
            Assert.That(entry.AvailableIn(2), Is.True);
            Assert.That(entry.AvailableIn(3), Is.True);
        }

        [Test]
        public void InvalidArgumentsThrow()
        {
            Assert.Throws<ArgumentNullException>(() => new LootEntry(null, 1f));
            Assert.Throws<ArgumentOutOfRangeException>(() => new LootEntry(LootFixtures.Scrap, 0f));
            Assert.Throws<ArgumentOutOfRangeException>(() => new LootEntry(LootFixtures.Scrap, 1f, minRing: 4));
            Assert.Throws<ArgumentException>(() => new LootEntry(LootFixtures.Scrap, 1f, minQuantity: 3, maxQuantity: 2));
        }

        [Test]
        public void QuantityCannotExceedStackSize()
        {
            Assert.Throws<ArgumentException>(() => new LootEntry(LootFixtures.Blade, 1f, maxQuantity: 2));
            Assert.Throws<ArgumentException>(() => new LootEntry(LootFixtures.Scrap, 1f, maxQuantity: 21));
        }

        [TestCase(0, false)]
        [TestCase(1, true)]
        [TestCase(3, true)]
        [TestCase(4, false)]
        public void DepthRingValidity(int ring, bool valid)
        {
            Assert.That(DepthRings.IsValid(ring), Is.EqualTo(valid));
        }

        [Test]
        public void DropBuildsFreshStack()
        {
            var stack = new LootDrop(LootFixtures.Scrap, 4).ToStack();
            Assert.That(stack.Quantity, Is.EqualTo(4));
            Assert.That(stack.Definition, Is.SameAs(LootFixtures.Scrap));
            var blade = new LootDrop(LootFixtures.Blade, 1).ToStack();
            Assert.That(blade.Item.Durability.Current, Is.EqualTo(Rarity.Rare.ScaleDurability(50)));
        }
    }
}
