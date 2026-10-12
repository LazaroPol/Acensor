using System;
using NUnit.Framework;
using Specimen.Core.Animation;

namespace Specimen.Tests.Core.Animation
{
    [TestFixture]
    public class LocomotionBlendTests
    {
        private readonly LocomotionBlend _blend = new LocomotionBlend(2f, 4f, 6f);

        [TestCase(0f, 0f)]
        [TestCase(-3f, 0f)]
        [TestCase(1f, 1f / 6f)]
        [TestCase(2f, 1f / 3f)]
        [TestCase(3f, 0.5f)]
        [TestCase(4f, 2f / 3f)]
        [TestCase(6f, 1f)]
        [TestCase(20f, 1f)]
        public void MapsSpeedToBlend(float speed, float expected)
            => Assert.That(_blend.Normalize(speed), Is.EqualTo(expected).Within(1e-4f));

        [Test]
        public void NaNIsIdle() => Assert.That(_blend.Normalize(float.NaN), Is.EqualTo(0f));

        [Test]
        public void RejectsNonIncreasingSpeeds()
        {
            Assert.Throws<ArgumentException>(() => new LocomotionBlend(4f, 4f, 6f));
            Assert.Throws<ArgumentException>(() => new LocomotionBlend(0f, 4f, 6f));
        }
    }
}
