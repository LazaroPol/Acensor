using NUnit.Framework;
using Specimen.Core.Movement;
using Specimen.Core.Stats;
using Specimen.Core.Vitals;

namespace Specimen.Tests.Core.Movement
{
    [TestFixture]
    public class PlanarMoverTests
    {
        private const float Eps = 1e-4f;
        private ResourcePool _stamina;
        private PlanarMover _mover;

        [SetUp]
        public void SetUp()
        {
            var config = new MovementConfig(4f, 1.5f, 20f, 10f);
            _stamina = new ResourcePool(new StatSheet().Define(StatType.MaxStamina, 100f));
            _mover = new PlanarMover(config, new SprintController(_stamina, config));
        }

        [Test]
        public void ForwardInputMovesAlongCameraYaw()
        {
            var velocity = _mover.Step(new PlanarVector(0f, 1f), 90f, false, 0.1f);
            Assert.That(velocity.X, Is.EqualTo(4f).Within(Eps));
            Assert.That(velocity.Z, Is.EqualTo(0f).Within(Eps));
        }

        [Test]
        public void DiagonalInputDoesNotExceedWalkSpeed()
        {
            var velocity = _mover.Step(new PlanarVector(1f, 1f), 0f, false, 0.1f);
            Assert.That(velocity.Magnitude, Is.EqualTo(4f).Within(Eps));
        }

        [Test]
        public void SprintMultipliesSpeedAndSpendsStamina()
        {
            var velocity = _mover.Step(new PlanarVector(0f, 1f), 0f, true, 1f);
            Assert.That(velocity.Z, Is.EqualTo(6f).Within(Eps));
            Assert.That(_mover.IsSprinting, Is.True);
            Assert.That(_stamina.Current, Is.EqualTo(80f).Within(Eps));
        }

        [Test]
        public void IdleInputGivesZeroAndKeepsStamina()
        {
            var velocity = _mover.Step(new PlanarVector(0.001f, 0f), 0f, true, 1f);
            Assert.That(velocity.Magnitude, Is.EqualTo(0f));
            Assert.That(_stamina.IsFull, Is.True);
        }
    }
}
