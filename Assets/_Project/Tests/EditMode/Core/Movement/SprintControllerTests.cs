using NUnit.Framework;
using Specimen.Core.Movement;
using Specimen.Core.Stats;
using Specimen.Core.Vitals;

namespace Specimen.Tests.Core.Movement
{
    [TestFixture]
    public class SprintControllerTests
    {
        private ResourcePool _stamina;
        private SprintController _sprint;

        [SetUp]
        public void SetUp()
        {
            var sheet = new StatSheet();
            _stamina = new ResourcePool(sheet.Define(StatType.MaxStamina, 100f));
            _sprint = new SprintController(_stamina, new MovementConfig(4f, 1.5f, 20f, 10f));
        }

        [Test]
        public void SprintingDrainsStaminaPerSecond()
        {
            Assert.That(_sprint.Update(true, true, 0.5f), Is.True);
            Assert.That(_stamina.Current, Is.EqualTo(90f).Within(1e-4f));
        }

        [TestCase(false, true)]
        [TestCase(true, false)]
        public void NoSprintWithoutRequestOrMovement(bool wants, bool moving)
        {
            Assert.That(_sprint.Update(wants, moving, 1f), Is.False);
            Assert.That(_stamina.IsFull, Is.True);
        }

        [Test]
        public void CannotStartBelowMinimumStamina()
        {
            _stamina.Drain(95f);
            Assert.That(_sprint.Update(true, true, 0.1f), Is.False);
            Assert.That(_stamina.Current, Is.EqualTo(5f).Within(1e-4f));
        }

        [Test]
        public void OngoingSprintContinuesBelowMinimumAndStopsWhenEmpty()
        {
            _stamina.Drain(88f);
            Assert.That(_sprint.Update(true, true, 0.5f), Is.True);
            Assert.That(_sprint.Update(true, true, 0.5f), Is.True);
            Assert.That(_stamina.IsEmpty, Is.True);
            Assert.That(_sprint.Update(true, true, 0.1f), Is.False);
            Assert.That(_sprint.IsSprinting, Is.False);
        }
    }
}
