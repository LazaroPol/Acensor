using System;
using NUnit.Framework;
using Specimen.Core.Items;

namespace Specimen.Tests.Core.Items
{
    [TestFixture]
    public class RarityRulesTests
    {
        [Test]
        public void MultiplierGrowsWithRarity()
        {
            var values = (Rarity[])Enum.GetValues(typeof(Rarity));
            for (var i = 1; i < values.Length; i++)
                Assert.That(values[i].DurabilityMultiplier(), Is.GreaterThan(values[i - 1].DurabilityMultiplier()));
        }

        [TestCase(Rarity.Common, 10, 10)]
        [TestCase(Rarity.Uncommon, 10, 13)]
        [TestCase(Rarity.Rare, 10, 15)]
        [TestCase(Rarity.Epic, 10, 20)]
        [TestCase(Rarity.Legendary, 10, 30)]
        [TestCase(Rarity.Legendary, 0, 0)]
        public void ScaleDurability(Rarity rarity, int baseValue, int expected)
        {
            Assert.That(rarity.ScaleDurability(baseValue), Is.EqualTo(expected));
        }

        [Test]
        public void UnknownRarityThrows()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => ((Rarity)99).DurabilityMultiplier());
        }
    }
}
