using System;
using NUnit.Framework;
using Specimen.Core.Combat;

namespace Specimen.Tests.Core.Combat
{
    public class CombatRulesTests
    {
        [Test]
        public void Hit_RejectsNegativeAmount()
            => Assert.Throws<ArgumentOutOfRangeException>(() => new Hit(-1f, DamageType.Fire));

        [Test]
        public void Resistances_DefaultToZero_AndClamp()
        {
            var r = new Resistances().Set(DamageType.Fire, 2f).Set(DamageType.Toxic, -5f);
            Assert.That(r.Of(DamageType.Shock), Is.EqualTo(0f));
            Assert.That(r.Of(DamageType.Fire), Is.EqualTo(Resistances.Max));
            Assert.That(r.Of(DamageType.Toxic), Is.EqualTo(Resistances.Min));
            Assert.That(r.MultiplierFor(DamageType.Toxic), Is.EqualTo(2f));
        }

        [Test]
        public void Resistances_RejectNaN()
            => Assert.Throws<ArgumentOutOfRangeException>(() => new Resistances().Set(DamageType.Fire, float.NaN));

        [Test]
        public void Armor_HasDiminishingReturns()
        {
            var armor = new ArmorMitigation(100f);
            Assert.That(armor.ReductionFor(0f), Is.EqualTo(0f));
            Assert.That(armor.ReductionFor(100f), Is.EqualTo(0.5f).Within(1e-6f));
            Assert.That(armor.ReductionFor(1000f), Is.LessThan(1f));
            Assert.That(armor.ReductionFor(200f) - armor.ReductionFor(100f),
                Is.LessThan(armor.ReductionFor(100f) - armor.ReductionFor(0f)));
        }

        [Test]
        public void Armor_OnlyAffectsPhysical()
        {
            var armor = new ArmorMitigation();
            Assert.That(armor.MultiplierFor(DamageType.Fire, 500f), Is.EqualTo(1f));
            Assert.That(armor.MultiplierFor(DamageType.Physical, 500f), Is.LessThan(1f));
        }

        [TestCase(-0.1f, 1.5f)]
        [TestCase(1.1f, 1.5f)]
        [TestCase(float.NaN, 1.5f)]
        [TestCase(0.5f, 0.9f)]
        public void CritRules_RejectInvalidValues(float chance, float multiplier)
            => Assert.Throws<ArgumentOutOfRangeException>(() => new CritRules(chance, multiplier));

        [Test]
        public void DefenseProfile_RejectsNegativeArmor()
            => Assert.Throws<ArgumentOutOfRangeException>(() => new DefenseProfile(-1f, Resistances.None));
    }
}
