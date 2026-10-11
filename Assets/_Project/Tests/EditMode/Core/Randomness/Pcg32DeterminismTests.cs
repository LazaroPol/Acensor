using System;
using System.Collections.Generic;
using NUnit.Framework;
using Specimen.Core.Randomness;

namespace Specimen.Tests.Core.Randomness
{
    public class Pcg32DeterminismTests
    {
        [Test]
        public void SameSeed_ProducesSameSequence()
        {
            var a = new Pcg32Random(1234);
            var b = new Pcg32Random(1234);
            for (int i = 0; i < 100; i++) Assert.That(a.NextUInt(), Is.EqualTo(b.NextUInt()));
        }

        [Test]
        public void DifferentSeeds_Diverge()
        {
            var a = new Pcg32Random(1);
            var b = new Pcg32Random(2);
            int equal = 0;
            for (int i = 0; i < 50; i++) if (a.NextUInt() == b.NextUInt()) equal++;
            Assert.That(equal, Is.LessThan(2));
        }

        [Test]
        public void ReferenceVector_MatchesPcg32Specification()
        {
            // Vector oficial de pcg32-demo: seed 42, stream 54.
            var rng = new Pcg32Random(42, 54);
            uint[] expected = { 0xa15c02b7, 0x7b47f409, 0xba1d3330, 0x83d2f293, 0xbfa4784b, 0xcbed606e };
            foreach (var e in expected) Assert.That(rng.NextUInt(), Is.EqualTo(e));
        }

        [Test]
        public void Fork_IsDeterministic_AndIndependentPerSalt()
        {
            var a1 = new Pcg32Random(500).Fork(1);
            var a2 = new Pcg32Random(500).Fork(1);
            var b = new Pcg32Random(500).Fork(2);
            int v1 = a1.Range(0, int.MaxValue), v2 = a2.Range(0, int.MaxValue), v3 = b.Range(0, int.MaxValue);
            Assert.That(v1, Is.EqualTo(v2));
            Assert.That(v1, Is.Not.EqualTo(v3));
        }
    }
}
