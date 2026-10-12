using System;
using NUnit.Framework;
using Specimen.Core.Appearance;
using Specimen.Core.Classes;
using Specimen.Core.Combat.Attacks;

namespace Specimen.Tests.Core.Appearance
{
    [TestFixture]
    public class LoadoutTests
    {
        [Test]
        public void EveryKitCarriesAWeapon()
        {
            foreach (ClassKitId kit in Enum.GetValues(typeof(ClassKitId)))
            {
                var loadout = ClassLoadouts.For(kit);
                Assert.That(loadout.Kit, Is.EqualTo(kit));
                Assert.That(loadout.Model, Does.StartWith(loadout.Folder));
            }
        }

        [Test]
        public void MeleeAndSpellKitsAreBothPresent()
        {
            Assert.That(ClassLoadouts.For(ClassKitId.Bastion).Style, Is.EqualTo(AttackStyle.CuerpoACuerpo));
            Assert.That(ClassLoadouts.For(ClassKitId.Arcanista).Style, Is.EqualTo(AttackStyle.Hechizo));
        }

        [Test]
        public void ModelNamesFollowThePack()
        {
            Assert.That(WeaponModels.Model(WeaponId.EspadaLarga, 3), Is.EqualTo("Long_Sword_03"));
            Assert.That(WeaponModels.Folder(WeaponId.Pala), Is.EqualTo("Shovel"));
        }

        [Test]
        public void EveryWeaponHasAFolderAndAPlausibleLength()
        {
            foreach (WeaponId weapon in Enum.GetValues(typeof(WeaponId)))
            {
                Assert.That(WeaponModels.Folder(weapon), Is.Not.Empty);
                Assert.That(WeaponModels.LengthOf(weapon), Is.InRange(0.3f, 2.5f), weapon.ToString());
            }
        }

        [TestCase(0)]
        [TestCase(8)]
        public void RejectsVariantsOutsideThePack(int variant)
            => Assert.Throws<ArgumentOutOfRangeException>(() => new ClassLoadout(ClassKitId.Vigia, WeaponId.Lanza, variant));
    }
}
