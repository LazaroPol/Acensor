using NUnit.Framework;
using Specimen.Core.Abilities;
using Specimen.Core.Classes;
using Specimen.Core.Stats;
using Specimen.Core.Vitals;

namespace Specimen.Tests.Core.Abilities
{
    [TestFixture]
    public class AbilitySlotTests
    {
        private CharacterVitals _vitals;

        [SetUp]
        public void SetUp()
        {
            var sheet = new StatSheet();
            sheet.Define(StatType.MaxHealth, 100f);
            sheet.Define(StatType.MaxClassResource, 50f);
            sheet.Define(StatType.MaxStamina, 40f);
            _vitals = new CharacterVitals(sheet);
        }

        private AbilitySlot Slot(float res = 20f, float sta = 10f, float cd = 3f)
            => new AbilitySlot(new AbilityDefinition("t", "Test", res, sta, cd), PoolCostPayer.From(_vitals));

        [Test]
        public void ActivatingChargesBothCostsAndStartsCooldown()
        {
            var slot = Slot();
            int fired = 0;
            slot.Activated += _ => fired++;
            Assert.That(slot.TryActivate(), Is.EqualTo(AbilityResult.Activated));
            Assert.That(_vitals.ClassResource.Current, Is.EqualTo(30f));
            Assert.That(_vitals.Stamina.Current, Is.EqualTo(30f));
            Assert.That(slot.IsReady, Is.False);
            Assert.That(fired, Is.EqualTo(1));
        }

        [Test]
        public void OnCooldownDoesNotCharge()
        {
            var slot = Slot();
            slot.TryActivate();
            Assert.That(slot.TryActivate(), Is.EqualTo(AbilityResult.OnCooldown));
            Assert.That(_vitals.ClassResource.Current, Is.EqualTo(30f));
        }

        [Test]
        public void CooldownTicksBackToReady()
        {
            var slot = Slot(cd: 3f);
            slot.TryActivate();
            slot.Tick(2f);
            Assert.That(slot.Cooldown.Fraction, Is.EqualTo(1f / 3f).Within(1e-5f));
            slot.Tick(1.5f);
            Assert.That(slot.IsReady, Is.True);
            Assert.That(slot.TryActivate(), Is.EqualTo(AbilityResult.Activated));
        }

        [Test]
        public void MissingResourceChargesNothing()
        {
            Assert.That(Slot(res: 60f).TryActivate(), Is.EqualTo(AbilityResult.NotEnoughResource));
            Assert.That(_vitals.Stamina.Current, Is.EqualTo(40f));
        }

        [Test]
        public void MissingStaminaChargesNothing()
        {
            var slot = Slot(sta: 45f);
            Assert.That(slot.TryActivate(), Is.EqualTo(AbilityResult.NotEnoughStamina));
            Assert.That(_vitals.ClassResource.Current, Is.EqualTo(50f));
            Assert.That(slot.IsReady, Is.True);
        }

        [Test]
        public void EveryKitHasASignatureAbility()
        {
            foreach (ClassKitId kit in System.Enum.GetValues(typeof(ClassKitId)))
                Assert.That(DefaultClassAbilities.SignatureOf(kit), Is.Not.Null, kit.ToString());
        }

        [Test]
        public void DefinitionRejectsInvalidValues()
        {
            Assert.Throws<System.ArgumentException>(() => new AbilityDefinition("", "X", 0f, 0f, 0f));
            Assert.Throws<System.ArgumentOutOfRangeException>(() => new AbilityDefinition("x", "X", -1f, 0f, 0f));
        }
    }
}
