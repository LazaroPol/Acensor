using System;
using System.Linq;
using NUnit.Framework;
using Specimen.Core.Items;
using Specimen.Core.Loot;
using Specimen.Core.Randomness;

namespace Specimen.Tests.Core.Loot
{
    [TestFixture]
    public class LootTableTests
    {
        [Test]
        public void LockedEntriesWeighZero()
        {
            var weights = LootFixtures.Table().WeightsFor(1);
            Assert.That(weights, Is.EqualTo(new[] { 10f, 2f, 0f }));
        }

        [Test]
        public void DeepRingWeightsApplyMultiplier()
        {
            var weights = LootFixtures.Table().WeightsFor(3);
            Assert.That(weights, Is.EqualTo(new[] { 10f, 6f, 5f }));
        }

        [Test]
        public void RareChanceGrowsWithDepth()
        {
            var table = LootFixtures.Table();
            Assert.That(table.ChanceOf(1, 2), Is.GreaterThan(table.ChanceOf(1, 1)));
            Assert.That(table.ChanceOf(1, 3) / table.ChanceOf(0, 3),
                Is.GreaterThan(table.ChanceOf(1, 2) / table.ChanceOf(0, 2)));
            Assert.That(table.ChanceOf(0, 3), Is.LessThan(table.ChanceOf(0, 1)));
        }

        [Test]
        public void RollNeverReturnsLockedItems()
        {
            var drops = LootFixtures.Table().Roll(1, 500, new Pcg32Random(7));
            Assert.That(drops.Any(d => d.Item == LootFixtures.Relic), Is.False);
            Assert.That(drops.Count, Is.EqualTo(500));
        }

        [Test]
        public void RollQuantitiesStayInRange()
        {
            var drops = LootFixtures.Table().Roll(3, 500, new Pcg32Random(11));
            foreach (var d in drops.Where(d => d.Item == LootFixtures.Scrap))
                Assert.That(d.Quantity, Is.InRange(2, 5));
            Assert.That(drops.Where(d => d.Item != LootFixtures.Scrap).All(d => d.Quantity == 1));
            Assert.That(drops.Any(d => d.Item == LootFixtures.Relic), Is.True);
        }

        [Test]
        public void SameSeedSameLoot()
        {
            var a = LootFixtures.Table().Roll(2, 50, new Pcg32Random(3)).Select(d => d.ToString());
            var b = LootFixtures.Table().Roll(2, 50, new Pcg32Random(3)).Select(d => d.ToString());
            Assert.That(a, Is.EqualTo(b));
        }

        [Test]
        public void EmptyRingThrowsOnRoll()
        {
            var table = new LootTable(new[] { new LootEntry(LootFixtures.Relic, 1f, minRing: 3) }, new RarityRingWeighting());
            Assert.That(table.HasLootIn(1), Is.False);
            Assert.That(table.ChanceOf(0, 1), Is.EqualTo(0f));
            Assert.Throws<InvalidOperationException>(() => table.Roll(1, new Pcg32Random(1)));
        }

        [Test]
        public void InvalidConstructionThrows()
        {
            var w = new RarityRingWeighting();
            Assert.Throws<ArgumentException>(() => new LootTable(Array.Empty<LootEntry>(), w));
            Assert.Throws<ArgumentException>(() => new LootTable(new LootEntry[] { null }, w));
            Assert.Throws<ArgumentNullException>(() => new LootTable(new[] { new LootEntry(LootFixtures.Scrap, 1f) }, null));
            Assert.Throws<ArgumentOutOfRangeException>(() => LootFixtures.Table().WeightsFor(4));
            Assert.Throws<ArgumentOutOfRangeException>(() => LootFixtures.Table().Roll(1, -1, new Pcg32Random(1)));
        }

        [Test]
        public void CustomWeightingIsInjected()
        {
            var table = LootFixtures.Table(new FlatWeighting());
            Assert.That(table.WeightsFor(3), Is.EqualTo(new[] { 10f, 2f, 1f }));
        }

        private sealed class FlatWeighting : IRingWeighting
        {
            public float Multiplier(Rarity rarity, int ring) => 1f;
        }
    }
}
