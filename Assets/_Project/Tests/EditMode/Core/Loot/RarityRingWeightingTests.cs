using System;
using NUnit.Framework;
using Specimen.Core.Items;
using Specimen.Core.Loot;

namespace Specimen.Tests.Core.Loot
{
    [TestFixture]
    public class RarityRingWeightingTests
    {
        [Test]
        public void FirstRingKeepsBaseWeights()
        {
            var w = new RarityRingWeighting();
            foreach (Rarity r in Enum.GetValues(typeof(Rarity)))
                Assert.That(w.Multiplier(r, 1), Is.EqualTo(1f));
        }

        [Test]
        public void CommonNeverChanges()
        {
            var w = new RarityRingWeighting();
            Assert.That(w.Multiplier(Rarity.Common, 3), Is.EqualTo(1f));
        }

        [TestCase(Rarity.Rare, 2, 2f)]
        [TestCase(Rarity.Rare, 3, 3f)]
        [TestCase(Rarity.Legendary, 3, 5f)]
        public void DeeperRingsFavourRarity(Rarity rarity, int ring, float expected)
        {
            Assert.That(new RarityRingWeighting(0.5f).Multiplier(rarity, ring), Is.EqualTo(expected).Within(1e-5f));
        }

        [Test]
        public void InvalidInputThrows()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => new RarityRingWeighting(-1f));
            Assert.Throws<ArgumentOutOfRangeException>(() => new RarityRingWeighting().Multiplier(Rarity.Rare, 0));
        }
    }
}
