using System;
using System.Collections.Generic;
using NUnit.Framework;
using Specimen.Core.Randomness;

namespace Specimen.Tests.Core.Randomness
{
    public class Pcg32DistributionTests
    {
        [Test]
        public void IntRange_StaysWithinBounds_AndCoversAllValues()
        {
            var rng = new Pcg32Random(7);
            var seen = new HashSet<int>();
            for (int i = 0; i < 2000; i++)
            {
                int v = rng.Range(-3, 4);
                Assert.That(v, Is.InRange(-3, 3));
                seen.Add(v);
            }
            Assert.That(seen.Count, Is.EqualTo(7));
        }

        [Test]
        public void IntRange_RejectsEmptyRange() => Assert.Throws<ArgumentException>(() => new Pcg32Random(1).Range(5, 5));

        [Test]
        public void Value_IsInUnitInterval_AndRoughlyUniform()
        {
            var rng = new Pcg32Random(99);
            double sum = 0;
            const int n = 20000;
            for (int i = 0; i < n; i++)
            {
                float v = rng.Value();
                Assert.That(v, Is.GreaterThanOrEqualTo(0f).And.LessThan(1f));
                sum += v;
            }
            Assert.That(sum / n, Is.EqualTo(0.5).Within(0.02));
        }

        [Test]
        public void FloatRange_StaysWithinBounds()
        {
            var rng = new Pcg32Random(3);
            for (int i = 0; i < 1000; i++) Assert.That(rng.Range(2f, 3f), Is.GreaterThanOrEqualTo(2f).And.LessThan(3f));
        }

        [Test]
        public void Chance_RespectsExtremes()
        {
            var rng = new Pcg32Random(5);
            for (int i = 0; i < 200; i++)
            {
                Assert.That(rng.Chance(0f), Is.False);
                Assert.That(rng.Chance(1f), Is.True);
            }
        }

        [Test]
        public void Pick_ReturnsElementOfList_AndRejectsEmpty()
        {
            var rng = new Pcg32Random(11);
            var items = new[] { "a", "b", "c" };
            for (int i = 0; i < 50; i++) Assert.That(items, Does.Contain(rng.Pick(items)));
            Assert.Throws<ArgumentException>(() => rng.Pick(new string[0]));
        }

        [Test]
        public void WeightedIndex_NeverPicksZeroWeight_AndFollowsProportions()
        {
            var rng = new Pcg32Random(2024);
            var weights = new[] { 0f, 1f, 3f };
            var counts = new int[3];
            const int n = 20000;
            for (int i = 0; i < n; i++) counts[rng.WeightedIndex(weights)]++;
            Assert.That(counts[0], Is.EqualTo(0));
            Assert.That((double)counts[2] / n, Is.EqualTo(0.75).Within(0.02));
        }

        [Test]
        public void WeightedIndex_RejectsInvalidWeights()
        {
            var rng = new Pcg32Random(1);
            Assert.Throws<ArgumentException>(() => rng.WeightedIndex(new[] { 0f, 0f }));
            Assert.Throws<ArgumentOutOfRangeException>(() => rng.WeightedIndex(new[] { 1f, -1f }));
        }
    }
}
