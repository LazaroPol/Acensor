using System;
using NUnit.Framework;
using Specimen.Core.Classes;
using Specimen.Core.Combat;
using Specimen.Core.Combat.Attacks;
using Specimen.Core.Stats;
using Specimen.Core.Vitals;

namespace Specimen.Tests.Core.Combat.Attacks
{
    [TestFixture]
    public class ClassAttackTests
    {
        private static ResourcePool Pool(float max)
        {
            var sheet = new StatSheet();
            sheet.Define(StatType.MaxHealth, 100f);
            sheet.Define(StatType.MaxClassResource, max);
            sheet.Define(StatType.MaxStamina, 100f);
            return new CharacterVitals(sheet).ClassResource;
        }

        [Test]
        public void EveryKitHasAProfile()
        {
            foreach (ClassKitId kit in Enum.GetValues(typeof(ClassKitId)))
                Assert.That(ClassAttackProfiles.For(kit), Is.Not.Null, kit.ToString());
        }

        [Test]
        public void SpellsCostResourceAndMeleeDoesNot()
        {
            Assert.That(ClassAttackProfiles.For(ClassKitId.Arcanista).ResourceCost, Is.GreaterThan(0f));
            Assert.That(ClassAttackProfiles.For(ClassKitId.Arcanista).ProjectileSpeed, Is.GreaterThan(0f));
            Assert.That(ClassAttackProfiles.For(ClassKitId.Bastion).ResourceCost, Is.EqualTo(0f));
        }

        [Test]
        public void CooldownBlocksUntilTicked()
        {
            var attack = new ClassAttack(ClassAttackProfiles.For(ClassKitId.Duelista), Pool(100f), 1f);
            Assert.That(attack.TryStart(), Is.EqualTo(AttackStartResult.Started));
            Assert.That(attack.TryStart(), Is.EqualTo(AttackStartResult.OnCooldown));
            attack.Tick(1f);
            Assert.That(attack.TryStart(), Is.EqualTo(AttackStartResult.Started));
        }

        [Test]
        public void SpellFailsWithoutResourceAndKeepsCooldownReady()
        {
            var profile = new AttackProfile(AttackStyle.Hechizo, DamageType.Fire, 1f, 10f, 0f, 1f, 30f, 10f);
            var attack = new ClassAttack(profile, Pool(40f), 1f);
            Assert.That(attack.TryStart(), Is.EqualTo(AttackStartResult.Started));
            attack.Tick(5f);
            Assert.That(attack.TryStart(), Is.EqualTo(AttackStartResult.NoResource));
            Assert.That(attack.IsReady, Is.True);
        }

        [Test]
        public void AttackSpeedShortensCooldownAndStatScalesDamage()
        {
            var profile = new AttackProfile(AttackStyle.CuerpoACuerpo, DamageType.Physical, 1.5f, 2f, 90f, 1f);
            Assert.That(profile.CooldownFor(2f), Is.EqualTo(0.5f).Within(1e-5f));
            Assert.That(profile.DamageFor(10f), Is.EqualTo(15f).Within(1e-5f));
            Assert.That(profile.HitFor(10f).Type, Is.EqualTo(DamageType.Physical));
        }
    }
}
