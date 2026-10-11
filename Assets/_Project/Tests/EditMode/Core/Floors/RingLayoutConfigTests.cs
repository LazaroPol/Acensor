using System;
using NUnit.Framework;
using Specimen.Core.Floors;

namespace Specimen.Tests.Core.Floors
{
    public class RingLayoutConfigTests
    {
        [Test]
        public void Default_HasThreeRings()
        {
            Assert.That(RingLayoutConfig.Default.RingCount, Is.EqualTo(3));
        }

        [TestCase(0)]
        [TestCase(4)]
        public void RingCountOutsideOneToThree_Throws(int rings)
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => new RingLayoutConfig(new int[rings], 0f, 0f));
        }

        [Test]
        public void NonPositiveRoomCount_Throws()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => new RingLayoutConfig(new[] { 2, 0 }, 0f, 0f));
        }

        [TestCase(-0.1f, 0f)]
        [TestCase(0f, 1.1f)]
        public void ProbabilityOutOfRange_Throws(float lateral, float back)
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => new RingLayoutConfig(new[] { 1 }, lateral, back));
        }

        [Test]
        public void Corridor_ToItself_Throws()
        {
            Assert.Throws<ArgumentException>(() => new Corridor(2, 2));
        }

        [Test]
        public void Corridor_IsUndirected()
        {
            Assert.That(new Corridor(3, 1), Is.EqualTo(new Corridor(1, 3)));
        }
    }
}
