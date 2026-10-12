using System.Linq;
using NUnit.Framework;
using Specimen.Core.Classes;
using Specimen.Core.Equipment;
using Specimen.Core.Items.Catalog;
using Specimen.Core.Progression;
using Specimen.Core.Stats;

namespace Specimen.Tests.Core.Equipment
{
    [TestFixture]
    public class EquipmentTests
    {
        [Test]
        public void EveryGearPieceIsAnItemAndWeaponsHaveWowSpeeds()
        {
            foreach (var piece in GearCatalog.All)
            {
                Assert.That(ItemCatalog.TryGet(piece.ItemId, out _), Is.True, piece.ItemId);
                if (piece.Weapon == null) continue;
                Assert.That(piece.Slot, Is.EqualTo(EquipSlot.Arma));
                Assert.That(piece.Weapon.Speed, Is.InRange(1.5f, 3.8f), piece.ItemId);
            }
        }

        [Test]
        public void EveryKitStartsWithAWeaponAndChest()
        {
            var hero = new HeroProgression();
            foreach (var kit in ClassKitCatalog.CreateDefault().All)
            {
                var gear = hero.EquipmentFor(kit.Id);
                Assert.That(gear.In(EquipSlot.Arma), Is.Not.Null, kit.DisplayName);
                Assert.That(gear.In(EquipSlot.Pecho), Is.Not.Null, kit.DisplayName);
            }
        }

        [Test]
        public void EquippingABetterChestRaisesArmorAndReturnsTheOldOne()
        {
            var kit = ClassKitCatalog.CreateDefault().All.First(k => k.Id == ClassKitId.Bastion);
            var hero = new HeroProgression();
            var sheet = kit.CreateStatSheet();
            hero.Apply(kit.Id, sheet);
            float before = sheet.ValueOf(StatType.Armor);
            var old = hero.EquipmentFor(kit.Id).Equip(ItemCatalog.Get(ItemIds.PlacaReforzada));
            hero.Apply(kit.Id, sheet);
            Assert.That(old.Id, Is.EqualTo(GearIds.CotaAnillas));
            Assert.That(sheet.ValueOf(StatType.Armor), Is.EqualTo(before + 150f - 95f).Within(1e-3));
            Assert.That(sheet.ValueOf(StatType.MaxHealth), Is.GreaterThan(kit.CreateStatSheet().ValueOf(StatType.MaxHealth)));
        }

        [Test]
        public void WeaponSwingScalesWithSpeedAndPower()
        {
            var dagger = new WeaponSpec(WeaponKind.Daga, 3, 7, 1.8f);
            var greatsword = new WeaponSpec(WeaponKind.Mandoble, 32, 49, 3.6f);
            Assert.That(greatsword.PowerBonus(14f), Is.EqualTo(dagger.PowerBonus(14f) * 2f).Within(1e-3));
            Assert.That(dagger.Dps, Is.EqualTo(5f / 1.8f).Within(1e-3));
            Assert.That(dagger.MinHit(7f), Is.EqualTo(3f + 7f * 1.8f / WeaponSpec.PowerPerDamage).Within(1e-3));
            Assert.That(new WeaponSpec(WeaponKind.Varita, 1, 2, 2f).Range, Is.EqualTo(WeaponSpec.RangedRange));
        }

        [Test]
        public void DeltaMarksUpgradesAndDowngrades()
        {
            GearCatalog.TryGet(GearIds.CotaAnillas, out var mail);
            GearCatalog.TryGet(ItemIds.PlacaReforzada, out var plate);
            Assert.That(GearDelta.Between(plate, mail).Armor, Is.EqualTo(55f));
            Assert.That(GearDelta.Between(plate, mail).IsUpgrade, Is.True);
            Assert.That(GearDelta.Between(mail, plate).IsUpgrade, Is.False);
            Assert.That(GearDelta.Between(mail, null).Armor, Is.EqualTo(95f));
        }

        [Test]
        public void UnequipEmptiesTheSlot()
        {
            var gear = CharacterEquipment.WithItems(new[] { GearIds.DagaGastada });
            Assert.That(gear.Weapon.Kind, Is.EqualTo(WeaponKind.Daga));
            Assert.That(gear.Unequip(EquipSlot.Arma).Id, Is.EqualTo(GearIds.DagaGastada));
            Assert.That(gear.In(EquipSlot.Arma), Is.Null);
            Assert.That(gear.Weapon.Min, Is.EqualTo(WeaponSpec.Unarmed.Min));
        }
    }
}
