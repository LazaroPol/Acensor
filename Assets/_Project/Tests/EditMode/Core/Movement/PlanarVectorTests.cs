using NUnit.Framework;
using Specimen.Core.Movement;

namespace Specimen.Tests.Core.Movement
{
    [TestFixture]
    public class PlanarVectorTests
    {
        private const float Eps = 1e-4f;

        [Test]
        public void ClampMagnitudeLimitsDiagonalInputToOne()
        {
            var clamped = new PlanarVector(1f, 1f).ClampMagnitude(1f);
            Assert.That(clamped.Magnitude, Is.EqualTo(1f).Within(Eps));
            Assert.That(clamped.X, Is.EqualTo(clamped.Z).Within(Eps));
        }

        [Test]
        public void ClampMagnitudeKeepsShortVectors()
        {
            var v = new PlanarVector(0.3f, 0.4f).ClampMagnitude(1f);
            Assert.That(v.X, Is.EqualTo(0.3f).Within(Eps));
            Assert.That(v.Z, Is.EqualTo(0.4f).Within(Eps));
        }

        [TestCase(0f, 0f, 1f)]
        [TestCase(90f, 1f, 0f)]
        [TestCase(180f, 0f, -1f)]
        [TestCase(-90f, -1f, 0f)]
        public void RotateByYawFollowsUnityConvention(float yaw, float x, float z)
        {
            var forward = PlanarVector.FromYaw(yaw);
            Assert.That(forward.X, Is.EqualTo(x).Within(Eps));
            Assert.That(forward.Z, Is.EqualTo(z).Within(Eps));
        }

        [Test]
        public void YawDegreesIsInverseOfFromYaw()
        {
            Assert.That(PlanarVector.FromYaw(45f).YawDegrees, Is.EqualTo(45f).Within(Eps));
            Assert.That(new PlanarVector(1f, 0f).YawDegrees, Is.EqualTo(90f).Within(Eps));
        }
    }
}
