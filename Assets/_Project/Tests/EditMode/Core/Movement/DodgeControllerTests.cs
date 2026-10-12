using NUnit.Framework;
using Specimen.Core.Movement;
using Specimen.Core.Stats;
using Specimen.Core.Vitals;

namespace Specimen.Tests.Core.Movement
{
    public class DodgeControllerTests
    {
        private ResourcePool _stamina;
        private DodgeController _dodge;

        [SetUp]
        public void SetUp()
        {
            var sheet = new StatSheet();
            sheet.Define(StatType.MaxHealth, 100f);
            sheet.Define(StatType.MaxClassResource, 100f);
            sheet.Define(StatType.MaxStamina, 100f);
            _stamina = new CharacterVitals(sheet).Stamina;
            _dodge = new DodgeController(new DodgeConfig(speed: 10f, durationSeconds: 0.2f, staminaCost: 30f,
                cooldownSeconds: 0.5f), _stamina);
        }

        [Test]
        public void TryStart_SpendsStaminaAndImpulsesAtNormalizedDirection()
        {
            Assert.That(_dodge.TryStart(new PlanarVector(0f, 0.3f)), Is.True);
            Assert.That(_stamina.Current, Is.EqualTo(70f));
            var v = _dodge.Step(0.1f);
            Assert.That(v.X, Is.EqualTo(0f).Within(1e-4));
            Assert.That(v.Z, Is.EqualTo(10f).Within(1e-4));
        }

        [Test]
        public void TryStart_WithoutDirectionOrStamina_FailsWithoutSpending()
        {
            Assert.That(_dodge.TryStart(PlanarVector.Zero), Is.False);
            _stamina.Drain(80f);
            Assert.That(_dodge.TryStart(new PlanarVector(1f, 0f)), Is.False);
            Assert.That(_stamina.Current, Is.EqualTo(20f));
        }

        [Test]
        public void Dodge_EndsAfterDurationThenWaitsForCooldown()
        {
            _dodge.TryStart(new PlanarVector(1f, 0f));
            _dodge.Step(0.25f);
            Assert.That(_dodge.IsDodging, Is.False);
            Assert.That(_dodge.Step(0.1f).Magnitude, Is.Zero);
            Assert.That(_dodge.TryStart(new PlanarVector(1f, 0f)), Is.False);
            _dodge.Step(0.5f);
            Assert.That(_dodge.IsReady, Is.True);
            Assert.That(_dodge.TryStart(new PlanarVector(1f, 0f)), Is.True);
        }

        [Test]
        public void TryStart_WhileDodging_IsRejected()
        {
            _dodge.TryStart(new PlanarVector(1f, 0f));
            Assert.That(_dodge.TryStart(new PlanarVector(0f, 1f)), Is.False);
            Assert.That(_stamina.Current, Is.EqualTo(70f));
        }
    }
}
