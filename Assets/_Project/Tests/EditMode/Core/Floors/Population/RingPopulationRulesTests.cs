using System;
using NUnit.Framework;
using Specimen.Core.Floors.Population;

namespace Specimen.Tests.Core.Floors.Population
{
    public class RingPopulationRulesTests
    {
        [Test]
        public void Default_GrowsWithDepth()
        {
            var rules = RingPopulationRules.Default;
            for (int ring = 2; ring <= 3; ring++)
            {
                Assert.That(rules.For(ring).Threat, Is.GreaterThan(rules.For(ring - 1).Threat));
                Assert.That(rules.For(ring).MaxLoot, Is.GreaterThan(rules.For(ring - 1).MaxLoot));
                Assert.That(rules.For(ring).MaxEnemies, Is.GreaterThan(rules.For(ring - 1).MaxEnemies));
            }
        }

        [TestCase(0)]
        [TestCase(4)]
        public void For_RejectsInvalidRing(int ring)
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => RingPopulationRules.Default.For(ring));
        }

        [Test]
        public void Rules_RejectWrongCountNullsAndDecreasingThreat()
        {
            var q = new RingQuota(1, 1, 0, 0, 2);
            Assert.Throws<ArgumentException>(() => new RingPopulationRules(new[] { q, q }));
            Assert.Throws<ArgumentException>(() => new RingPopulationRules(new[] { q, null, q }));
            Assert.Throws<ArgumentException>(() =>
                new RingPopulationRules(new[] { q, new RingQuota(1, 1, 0, 0, 1), q }));
        }

        [Test]
        public void Quota_RejectsInvalidRanges()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => new RingQuota(-1, 1, 0, 0, 1));
            Assert.Throws<ArgumentOutOfRangeException>(() => new RingQuota(2, 1, 0, 0, 1));
            Assert.Throws<ArgumentOutOfRangeException>(() => new RingQuota(0, 0, 3, 2, 1));
            Assert.Throws<ArgumentOutOfRangeException>(() => new RingQuota(0, 0, 0, 0, 0));
        }
    }
}
