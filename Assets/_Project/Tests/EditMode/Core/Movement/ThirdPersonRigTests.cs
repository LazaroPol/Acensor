using System;
using NUnit.Framework;
using Specimen.Core.Movement;

namespace Specimen.Tests.Core.Movement
{
    [TestFixture]
    public class ThirdPersonRigTests
    {
        private const float Eps = 1e-3f;

        [Test]
        public void OffsetSitsBehindAndAboveTarget()
        {
            var offset = new ThirdPersonRig(10f, 30f, 0f).Offset();
            Assert.That(offset.X, Is.EqualTo(0f).Within(Eps));
            Assert.That(offset.Y, Is.EqualTo(5f).Within(Eps));
            Assert.That(offset.Z, Is.EqualTo(-8.660f).Within(Eps));
        }

        [Test]
        public void YawRotatesTheOffsetAroundTheTarget()
        {
            var offset = new ThirdPersonRig(10f, 90f, 90f).Offset();
            Assert.That(offset.Y, Is.EqualTo(10f).Within(Eps));
            offset = new ThirdPersonRig(10f, 30f, 90f).Offset();
            Assert.That(offset.X, Is.EqualTo(-8.660f).Within(Eps));
            Assert.That(offset.Z, Is.EqualTo(0f).Within(Eps));
        }

        [Test]
        public void FollowConvergesExponentiallyToDesired()
        {
            var rig = new ThirdPersonRig(10f, 30f, 0f, (float)Math.Log(2));
            var target = new CameraPoint(4f, 0f, 0f);
            var start = rig.Desired(new CameraPoint(0f, 0f, 0f));
            var next = rig.Follow(start, target, 1f);
            Assert.That(next.X, Is.EqualTo(2f).Within(Eps));
            Assert.That(next.Y, Is.EqualTo(5f).Within(Eps));
        }

        [Test]
        public void ZeroSharpnessSnapsToDesired()
        {
            var rig = new ThirdPersonRig(10f, 30f, 0f, 0f);
            var next = rig.Follow(new CameraPoint(0f, 0f, 0f), new CameraPoint(1f, 2f, 3f), 0.01f);
            Assert.That(next.X, Is.EqualTo(1f).Within(Eps));
            Assert.That(next.Y, Is.EqualTo(7f).Within(Eps));
        }

        [TestCase(0f)]
        [TestCase(91f)]
        public void RejectsInvalidPitch(float pitch) =>
            Assert.Throws<ArgumentOutOfRangeException>(() => new ThirdPersonRig(10f, pitch));
    }
}
