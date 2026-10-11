using System;
using NUnit.Framework;
using Specimen.Core.Stats;
using Specimen.Core.Vitals;

namespace Specimen.Tests.Core.Vitals
{
    [TestFixture]
    public class FatigueTests
    {
        private const float Eps = 1e-4f;
        private static readonly FatigueConfig Config = new FatigueConfig(1, 2, 0.1f, 5);

        private static Stat MaxStamina() => new Stat(100f);

        [Test]
        public void StartsAtZeroWithoutTouchingStamina()
        {
            var stamina = MaxStamina();
            var fatigue = new Fatigue(stamina, Config);
            Assert.That(fatigue.Points, Is.Zero);
            Assert.That(stamina.Value, Is.EqualTo(100f));
            Assert.That(stamina.Modifiers, Is.Empty);
        }

        [Test]
        public void FloorAddsPointsAndReducesMaxStamina()
        {
            var stamina = MaxStamina();
            var fatigue = new Fatigue(stamina, Config);
            Assert.That(fatigue.AccumulateFloor(), Is.EqualTo(1));
            Assert.That(stamina.Value, Is.EqualTo(90f).Within(Eps));
            Assert.That(stamina.Modifiers.Count, Is.EqualTo(1));
        }

        [Test]
        public void TrappedAddsExtraFatigue()
        {
            var stamina = MaxStamina();
            var fatigue = new Fatigue(stamina, Config);
            Assert.That(fatigue.AccumulateFloor(wasTrapped: true), Is.EqualTo(3));
            Assert.That(stamina.Value, Is.EqualTo(70f).Within(Eps));
        }

        [Test]
        public void IsCappedAtMaxPoints()
        {
            var stamina = MaxStamina();
            var fatigue = new Fatigue(stamina, Config);
            fatigue.Add(4);
            Assert.That(fatigue.AccumulateFloor(wasTrapped: true), Is.EqualTo(1));
            Assert.That(fatigue.IsExhausted, Is.True);
            Assert.That(stamina.Value, Is.EqualTo(50f).Within(Eps));
        }

        [Test]
        public void RecoverReducesFatigueAndRestoresStamina()
        {
            var stamina = MaxStamina();
            var fatigue = new Fatigue(stamina, Config);
            fatigue.Add(3);
            Assert.That(fatigue.Recover(5), Is.EqualTo(3));
            Assert.That(fatigue.Points, Is.Zero);
            Assert.That(stamina.Value, Is.EqualTo(100f).Within(Eps));
            Assert.That(stamina.Modifiers, Is.Empty);
        }

        [Test]
        public void StaminaPoolIsClampedWhenFatigueGrows()
        {
            var sheet = new StatSheet();
            sheet.Define(StatType.MaxHealth, 100f);
            sheet.Define(StatType.MaxClassResource, 50f);
            sheet.Define(StatType.MaxStamina, 80f);
            var vitals = new CharacterVitals(sheet);
            var fatigue = new Fatigue(sheet.Get(StatType.MaxStamina), Config);
            fatigue.Add(2);
            Assert.That(vitals.Stamina.Current, Is.EqualTo(64f).Within(Eps));
        }

        [Test]
        public void ChangedFiresOnlyOnRealChanges()
        {
            var fatigue = new Fatigue(MaxStamina(), Config);
            int calls = 0;
            fatigue.Changed += _ => calls++;
            fatigue.Recover(1);
            fatigue.Add(2);
            fatigue.Clear();
            fatigue.Clear();
            Assert.That(calls, Is.EqualTo(2));
        }

        [Test]
        public void KeepsOtherModifiersIntact()
        {
            var stamina = MaxStamina();
            stamina.AddModifier(StatModifier.Flat(20f, "botas"));
            var fatigue = new Fatigue(stamina, Config);
            fatigue.Add(1);
            fatigue.Clear();
            Assert.That(stamina.Value, Is.EqualTo(120f).Within(Eps));
        }

        [Test]
        public void InvalidInputsThrow()
        {
            var fatigue = new Fatigue(MaxStamina(), Config);
            Assert.Throws<ArgumentNullException>(() => new Fatigue(null, Config));
            Assert.Throws<ArgumentOutOfRangeException>(() => fatigue.Add(-1));
            Assert.Throws<ArgumentOutOfRangeException>(() => fatigue.Recover(-1));
            Assert.Throws<ArgumentOutOfRangeException>(() => new FatigueConfig(1, 1, 0.3f, 5));
            Assert.Throws<ArgumentOutOfRangeException>(() => new FatigueConfig(1, 1, 0.1f, 0));
        }
    }
}
