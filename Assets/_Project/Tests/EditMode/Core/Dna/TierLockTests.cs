using System;
using NUnit.Framework;
using Specimen.Core.Dna;

namespace Specimen.Tests.Core.Dna
{
    [TestFixture]
    public class TierLockTests
    {
        [TestCase(1, 0)]
        [TestCase(2, 5)]
        [TestCase(3, 12)]
        [TestCase(4, 20)]
        [TestCase(5, 30)]
        public void DefaultThresholdsMatchGdd(int tier, int expected)
            => Assert.That(TierLock.CreateDefault().RequiredPoints(tier), Is.EqualTo(expected));

        [Test]
        public void TierOutOfRangeThrows()
        {
            var tl = TierLock.CreateDefault();
            Assert.Throws<ArgumentOutOfRangeException>(() => tl.RequiredPoints(0));
            Assert.Throws<ArgumentOutOfRangeException>(() => tl.RequiredPoints(6));
        }

        [Test]
        public void EmptyThresholdsThrow() => Assert.Throws<ArgumentException>(() => new TierLock());

        [Test]
        public void DefaultTreeHasTwoNodesPerTierAndBranch()
        {
            var count = 0;
            foreach (var _ in DnaTree.CreateDefault().Nodes) count++;
            Assert.That(count, Is.EqualTo(5 * 5 * 2));
        }

        [Test]
        public void DuplicateNodeIdThrows()
        {
            var n = new DnaNode("x", DnaBranch.Potencia, 1, 1);
            Assert.Throws<ArgumentException>(() => new DnaTree(new[] { n, n }));
        }
    }
}
