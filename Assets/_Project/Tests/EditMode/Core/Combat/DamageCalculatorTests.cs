using System;
using NUnit.Framework;
using Specimen.Core.Combat;
using Specimen.Core.Randomness;

namespace Specimen.Tests.Core.Combat
{
    public class DamageCalculatorTests
    {
        private static DamageCalculator Calc(ulong seed = 1) => new DamageCalculator(new Pcg32Random(seed));

        [Test]
        public void NoDefenses_NoCrit_DealsRawDamage()
        {
            var r = Calc().Resolve(new Hit(40f, DamageType.Physical), CritRules.None, DefenseProfile.None);
            Assert.That(r.Final, Is.EqualTo(40f));
            Assert.That(r.IsCritical, Is.False);
            Assert.That(r.Mitigated, Is.EqualTo(0f));
        }

        [Test]
        public void Physical_AppliesArmorThenResistance()
        {
            var target = new DefenseProfile(100f, new Resistances().Set(DamageType.Physical, 0.5f));
            var r = Calc().Resolve(new Hit(100f, DamageType.Physical), CritRules.None, target);
            Assert.That(r.Final, Is.EqualTo(25f).Within(1e-4f));
        }

        [Test]
        public void Elemental_IgnoresArmor_AndVulnerabilityAmplifies()
        {
            var target = new DefenseProfile(1000f, new Resistances().Set(DamageType.Fire, -0.5f));
            var r = Calc().Resolve(new Hit(10f, DamageType.Fire), CritRules.None, target);
            Assert.That(r.Final, Is.EqualTo(15f).Within(1e-4f));
        }

        [Test]
        public void GuaranteedCrit_MultipliesRawDamage()
        {
            var r = Calc().Resolve(new Hit(10f, DamageType.Shock), new CritRules(1f, 2f), DefenseProfile.None);
            Assert.That(r.IsCritical, Is.True);
            Assert.That(r.Raw, Is.EqualTo(20f));
        }

        [Test]
        public void HitThatCannotCrit_NeverCrits()
        {
            var r = Calc().Resolve(new Hit(10f, DamageType.Shock, canCrit: false), new CritRules(1f, 2f), DefenseProfile.None);
            Assert.That(r.IsCritical, Is.False);
        }

        [Test]
        public void Crits_AreReproducibleBySeed()
        {
            var crit = new CritRules(0.3f);
            var a = Calc(42); var b = Calc(42);
            int crits = 0;
            for (int i = 0; i < 200; i++)
            {
                var ra = a.Resolve(new Hit(5f, DamageType.Physical), crit, DefenseProfile.None);
                var rb = b.Resolve(new Hit(5f, DamageType.Physical), crit, DefenseProfile.None);
                Assert.That(ra.IsCritical, Is.EqualTo(rb.IsCritical));
                if (ra.IsCritical) crits++;
            }
            Assert.That(crits, Is.InRange(30, 90));
        }

        [Test]
        public void ResolveRejectsNulls()
        {
            var calc = Calc();
            Assert.Throws<ArgumentNullException>(() => calc.Resolve(null, CritRules.None, DefenseProfile.None));
            Assert.Throws<ArgumentNullException>(() => new DamageCalculator(null));
        }
    }
}
