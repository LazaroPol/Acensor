using NUnit.Framework;
using Specimen.Core.Combat.Attacks;
using Specimen.Core.Movement;

namespace Specimen.Tests.Core.Combat.Attacks
{
    [TestFixture]
    public class MeleeArcTests
    {
        private static readonly PlanarVector Origin = PlanarVector.Zero;

        [TestCase(0f, 1.5f, true)]
        [TestCase(0f, 2.5f, false)]
        [TestCase(1.5f, 0f, false)]
        [TestCase(0.5f, 1.5f, true)]
        [TestCase(0f, -1f, false)]
        public void HitsOnlyInsideRangeAndArc(float x, float z, bool hits)
            => Assert.That(MeleeArc.Contains(Origin, 0f, new PlanarVector(x, z), 2f, 90f), Is.EqualTo(hits));

        [Test]
        public void FacingRotatesTheArc()
            => Assert.That(MeleeArc.Contains(Origin, 90f, new PlanarVector(1.5f, 0f), 2f, 90f), Is.True);

        [Test]
        public void TargetRadiusExtendsReach()
            => Assert.That(MeleeArc.Contains(Origin, 0f, new PlanarVector(0f, 2.3f), 2f, 90f, 0.4f), Is.True);

        [Test]
        public void OverlappingTargetIsAlwaysHit()
            => Assert.That(MeleeArc.Contains(Origin, 180f, Origin, 2f, 10f), Is.True);

        [TestCase(350f, 10f, 20f)]
        [TestCase(10f, 350f, -20f)]
        [TestCase(0f, 180f, 180f)]
        public void DeltaAngleWraps(float from, float to, float expected)
            => Assert.That(MeleeArc.DeltaAngle(from, to), Is.EqualTo(expected).Within(1e-4f));
    }
}
