using System;
using NUnit.Framework;
using Specimen.Core.Movement;

namespace Specimen.Tests.Core.Movement
{
    public class CameraZoomTests
    {
        [Test]
        public void Apply_PositiveScrollGetsCloser_NegativeFartherAway()
        {
            var zoom = new CameraZoom(initial: 12f, min: 6f, max: 20f, stepPerUnit: 2f);
            Assert.That(zoom.Apply(1f), Is.EqualTo(10f));
            Assert.That(zoom.Apply(-2f), Is.EqualTo(14f));
        }

        [Test]
        public void Apply_ClampsToLimits()
        {
            var zoom = new CameraZoom(12f, 6f, 20f, 5f);
            Assert.That(zoom.Apply(100f), Is.EqualTo(6f));
            Assert.That(zoom.Apply(-100f), Is.EqualTo(20f));
        }

        [Test]
        public void Constructor_ClampsInitialAndRejectsInvertedRange()
        {
            Assert.That(new CameraZoom(99f, 6f, 20f).Distance, Is.EqualTo(20f));
            Assert.Throws<ArgumentOutOfRangeException>(() => new CameraZoom(10f, 8f, 6f));
        }
    }
}
