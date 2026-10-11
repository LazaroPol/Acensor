using System;
using NUnit.Framework;
using Specimen.Core.Combat.Perception;
using Specimen.Core.Movement;

namespace Specimen.Tests.Core.Combat.Perception
{
    public class EnemyPerceptionTests
    {
        // Mira hacia +Z desde el origen; visión 10 m con semiángulo 45°, oído 8 m, persecución 4 m/s.
        private static readonly Observer Origin = new Observer(PlanarVector.Zero, 0f);

        private static EnemyPerception Perception() =>
            new EnemyPerception(new PerceptionConfig(10f, 45f, 8f, 4f), TrappedPerceptionModifiers.Instance);

        private static PerceivedTarget At(float x, float z, float noise = 0f, bool trapped = false) =>
            new PerceivedTarget("p1", new PlanarVector(x, z), noise, trapped);

        [Test]
        public void Vision_SeesTargetInFrontWithinRange()
        {
            Assert.That(Perception().Perceive(Origin, At(0f, 9f)), Is.EqualTo(Awareness.Seen));
            Assert.That(Perception().Perceive(Origin, At(5f, 6f)), Is.EqualTo(Awareness.Seen));
        }

        [Test]
        public void Vision_IgnoresTargetsBehindOutsideConeOrTooFar()
        {
            Assert.That(Perception().Perceive(Origin, At(0f, -3f)), Is.EqualTo(Awareness.None));
            Assert.That(Perception().Perceive(Origin, At(5f, 1f)), Is.EqualTo(Awareness.None));
            Assert.That(Perception().Perceive(Origin, At(0f, 11f)), Is.EqualTo(Awareness.None));
        }

        [Test]
        public void Vision_FollowsObserverFacing()
        {
            var facingEast = new Observer(PlanarVector.Zero, 90f);
            Assert.That(Perception().Perceive(facingEast, At(9f, 0f)), Is.EqualTo(Awareness.Seen));
            Assert.That(Perception().Perceive(facingEast, At(0f, 9f)), Is.EqualTo(Awareness.None));
        }

        [Test]
        public void Hearing_DetectsByNoiseScaledRange_InAnyDirection()
        {
            Assert.That(Perception().Perceive(Origin, At(0f, -4f, noise: 0.5f)), Is.EqualTo(Awareness.Heard));
            Assert.That(Perception().Perceive(Origin, At(0f, -5f, noise: 0.5f)), Is.EqualTo(Awareness.None));
            Assert.That(Perception().Perceive(Origin, At(0f, -1f, noise: 0f)), Is.EqualTo(Awareness.None));
        }

        [Test]
        public void Seen_TakesPriorityOverHeard()
        {
            Assert.That(Perception().Perceive(Origin, At(0f, 2f, noise: 1f)), Is.EqualTo(Awareness.Seen));
        }

        [Test]
        public void Trapped_DoublesVisionRange_AndSpeedsUpChase()
        {
            Assert.That(Perception().Perceive(Origin, At(0f, 19f, trapped: true)), Is.EqualTo(Awareness.Seen));
            Assert.That(Perception().Perceive(Origin, At(0f, 21f, trapped: true)), Is.EqualTo(Awareness.None));
            Assert.That(Perception().ChaseSpeed(At(0f, 1f, trapped: true)), Is.EqualTo(6f).Within(1e-4f));
            Assert.That(Perception().ChaseSpeed(At(0f, 1f)), Is.EqualTo(4f).Within(1e-4f));
        }

        [Test]
        public void Detected_ReturnsOnlyPerceivedTargets()
        {
            var seen = new PerceivedTarget("a", new PlanarVector(0f, 5f), 0f, false);
            var heard = new PerceivedTarget("b", new PlanarVector(0f, -2f), 1f, false);
            var hidden = new PerceivedTarget("c", new PlanarVector(0f, -30f), 1f, false);
            var ids = Perception().Detected(Origin, new[] { seen, heard, hidden });
            Assert.That(ids, Is.EqualTo(new[] { seen, heard }));
        }

        [Test]
        public void Config_AndTarget_RejectInvalidValues()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => new PerceptionConfig(-1f, 45f, 8f, 4f));
            Assert.Throws<ArgumentOutOfRangeException>(() => new PerceptionConfig(10f, 181f, 8f, 4f));
            Assert.Throws<ArgumentOutOfRangeException>(() => new PerceptionConfig(10f, 45f, -1f, 4f));
            Assert.Throws<ArgumentOutOfRangeException>(() => new PerceptionConfig(10f, 45f, 8f, -1f));
            Assert.Throws<ArgumentOutOfRangeException>(() => At(0f, 0f, noise: 1.5f));
            Assert.Throws<ArgumentNullException>(() => new EnemyPerception(null, TrappedPerceptionModifiers.Instance));
        }
    }
}
