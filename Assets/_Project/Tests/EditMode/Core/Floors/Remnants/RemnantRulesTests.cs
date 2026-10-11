using System;
using NUnit.Framework;
using Specimen.Core.Floors.Remnants;

namespace Specimen.Tests.Core.Floors.Remnants
{
    public class RemnantRulesTests
    {
        [Test]
        public void Default_GrowsWithDepth()
        {
            var rules = RemnantRules.Default;
            for (int ring = 2; ring <= 3; ring++)
            {
                Assert.That(rules.For(ring).RemnantChance, Is.GreaterThan(rules.For(ring - 1).RemnantChance));
                Assert.That(rules.For(ring).FragmentChance, Is.GreaterThan(rules.For(ring - 1).FragmentChance));
            }
        }

        [TestCase(-0.1f)]
        [TestCase(1.1f)]
        [TestCase(float.NaN)]
        public void Odds_RejectInvalidProbabilities(float p)
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => new RingRemnantOdds(p, 0f));
            Assert.Throws<ArgumentOutOfRangeException>(() => new RingRemnantOdds(0f, p));
        }

        [Test]
        public void Rules_RejectWrongCountNullsAndDecreasingOdds()
        {
            var ok = new RingRemnantOdds(0.5f, 0.5f);
            Assert.Throws<ArgumentNullException>(() => new RemnantRules(null));
            Assert.Throws<ArgumentException>(() => new RemnantRules(new[] { ok, ok }));
            Assert.Throws<ArgumentException>(() => new RemnantRules(new[] { ok, null, ok }));
            Assert.Throws<ArgumentException>(() =>
                new RemnantRules(new[] { ok, ok, new RingRemnantOdds(0.1f, 0.5f) }));
            Assert.Throws<ArgumentException>(() =>
                new RemnantRules(new[] { ok, ok, new RingRemnantOdds(0.5f, 0.1f) }));
        }

        [TestCase(0)]
        [TestCase(4)]
        public void For_RejectsRingsOutsideDepth(int ring)
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => RemnantRules.Default.For(ring));
        }
    }
}
