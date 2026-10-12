using System;
using NUnit.Framework;
using Specimen.Core.Animation;
using Specimen.Core.Classes;

namespace Specimen.Tests.Core.Animation
{
    [TestFixture]
    public class KitAnimationSetsTests
    {
        [Test]
        public void EveryKitCoversEverySlotWithAQuaterniusFallback()
        {
            foreach (ClassKitId kit in Enum.GetValues(typeof(ClassKitId)))
            {
                var set = KitAnimationSets.For(kit);
                foreach (AnimSlot slot in Enum.GetValues(typeof(AnimSlot)))
                    Assert.That(set.CandidatesFor(slot), Is.Not.Empty, $"{kit}.{slot}");
                Assert.That(set.CandidatesFor(AnimSlot.AtaqueMele), Does.Contain("Sword_Regular_A"), kit.ToString());
            }
        }

        [TestCase(ClassKitId.Bastion, "HumanM@Attack2H01", "HumanM@CombatIdle2H01")]
        [TestCase(ClassKitId.Vigia, "HumanF@AttackPolearm01", "HumanF@CombatIdlePolearm01")]
        [TestCase(ClassKitId.Duelista, "HumanM@Attack1H01_R", "HumanM@CombatIdle1H01")]
        public void MeleeKitsUseKevinIglesiasByGrip(ClassKitId kit, string attack, string idle)
        {
            var set = KitAnimationSets.For(kit);
            Assert.That(set.CandidatesFor(AnimSlot.AtaqueMele)[0], Is.EqualTo(attack));
            Assert.That(set.CandidatesFor(AnimSlot.Reposo)[0], Is.EqualTo(idle));
        }

        [TestCase(ClassKitId.Oraculo, "HumanF@MagicAttackDirect1H01_R")]
        [TestCase(ClassKitId.DruidaCeniza, "HumanM@MagicAttackOmni01")]
        [TestCase(ClassKitId.Arcanista, "HumanF@MagicAttackDirect2H01")]
        public void CastersUseTheirOwnSpell(ClassKitId kit, string spell)
            => Assert.That(KitAnimationSets.For(kit).CandidatesFor(AnimSlot.Hechizo)[0], Is.EqualTo(spell));

        [Test]
        public void DeathUsesKevinFirstThenQuaternius()
        {
            var death = KitAnimationSets.For(ClassKitId.Vigia).CandidatesFor(AnimSlot.Muerte);
            Assert.That(death[0], Is.EqualTo("HumanF@Death01"));
            Assert.That(death, Does.Contain("Death01"));
        }

        [Test]
        public void GripsFollowTheWeapon()
        {
            Assert.That(KitGrips.For(ClassKitId.Bastion), Is.EqualTo(WeaponGrip.DosManos));
            Assert.That(KitGrips.For(ClassKitId.Vigia), Is.EqualTo(WeaponGrip.Asta));
            Assert.That(KitGrips.For(ClassKitId.Duelista), Is.EqualTo(WeaponGrip.UnaMano));
            Assert.That(KitGrips.For(ClassKitId.Arcanista), Is.EqualTo(WeaponGrip.Magia));
        }
    }
}
