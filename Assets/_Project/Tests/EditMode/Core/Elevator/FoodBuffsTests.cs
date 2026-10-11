using System;
using NUnit.Framework;
using Specimen.Core.Elevator;
using Specimen.Core.Stats;

namespace Specimen.Tests.Core.Elevator
{
    [TestFixture]
    public class FoodBuffsTests
    {
        private const float Eps = 1e-4f;

        private sealed class FixedLevels : IModuleLevels
        {
            public int Kitchen;
            public int LevelOf(ModuleType type) => type == ModuleType.Kitchen ? Kitchen : 0;
        }

        private static readonly Meal Stew = new Meal("guiso", StatType.MaxHealth, ModifierKind.Flat, 20f, 60f);
        private static readonly Meal Skewer = new Meal("brocheta", StatType.Damage, ModifierKind.PercentAdd, 0.1f, 30f);

        private StatSheet _sheet;
        private FixedLevels _levels;
        private FoodBuffs _buffs;

        [SetUp]
        public void SetUp()
        {
            _sheet = new StatSheet();
            _sheet.Define(StatType.MaxHealth, 100f);
            _sheet.Define(StatType.Damage, 10f);
            _levels = new FixedLevels { Kitchen = 1 };
            _buffs = new FoodBuffs(_sheet, _levels, new KitchenConfig(0.5f));
        }

        [Test]
        public void Eat_AppliesModifierWithBaseDuration()
        {
            Assert.That(_buffs.Eat(Stew), Is.True);
            Assert.That(_sheet.ValueOf(StatType.MaxHealth), Is.EqualTo(120f).Within(Eps));
            Assert.That(_buffs.RemainingOf(Stew), Is.EqualTo(60f).Within(Eps));
        }

        [Test]
        public void WithoutKitchen_EatingDoesNothing()
        {
            _levels.Kitchen = 0;
            Assert.That(_buffs.Eat(Stew), Is.False);
            Assert.That(_buffs.ActiveCount, Is.EqualTo(0));
            Assert.That(_sheet.ValueOf(StatType.MaxHealth), Is.EqualTo(100f).Within(Eps));
        }

        [Test]
        public void DurationScalesWithKitchenLevel()
        {
            _levels.Kitchen = 3;
            _buffs.Eat(Stew);
            Assert.That(_buffs.RemainingOf(Stew), Is.EqualTo(120f).Within(Eps));
        }

        [Test]
        public void Expiry_RemovesModifierAndNotifies()
        {
            Meal expired = null;
            _buffs.BuffExpired += m => expired = m;
            _buffs.Eat(Stew);
            _buffs.Tick(59f);
            Assert.That(_buffs.IsActive(Stew), Is.True);
            _buffs.Tick(1f);
            Assert.That(expired, Is.SameAs(Stew));
            Assert.That(_buffs.IsActive(Stew), Is.False);
            Assert.That(_sheet.ValueOf(StatType.MaxHealth), Is.EqualTo(100f).Within(Eps));
        }

        [Test]
        public void EatingSameMeal_RefreshesWithoutStacking()
        {
            _buffs.Eat(Stew);
            _buffs.Tick(50f);
            _buffs.Eat(Stew);
            Assert.That(_buffs.RemainingOf(Stew), Is.EqualTo(60f).Within(Eps));
            Assert.That(_sheet.ValueOf(StatType.MaxHealth), Is.EqualTo(120f).Within(Eps));
        }

        [Test]
        public void DifferentMeals_Stack_AndExpireIndependently()
        {
            _buffs.Eat(Stew);
            _buffs.Eat(Skewer);
            Assert.That(_buffs.ActiveCount, Is.EqualTo(2));
            Assert.That(_sheet.ValueOf(StatType.Damage), Is.EqualTo(11f).Within(Eps));
            _buffs.Tick(30f);
            Assert.That(_buffs.IsActive(Skewer), Is.False);
            Assert.That(_sheet.ValueOf(StatType.Damage), Is.EqualTo(10f).Within(Eps));
            Assert.That(_sheet.ValueOf(StatType.MaxHealth), Is.EqualTo(120f).Within(Eps));
        }

        [Test]
        public void NegativeDelta_Throws()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => _buffs.Tick(-1f));
        }

        [Test]
        public void Meal_RejectsInvalidDefinition()
        {
            Assert.Throws<ArgumentOutOfRangeException>(
                () => new Meal("x", StatType.Damage, ModifierKind.Flat, 1f, 0f));
            Assert.Throws<ArgumentException>(() => new Meal(" ", StatType.Damage, ModifierKind.Flat, 1f, 5f));
        }
    }
}
