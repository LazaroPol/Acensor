using System;
using NUnit.Framework;
using Specimen.Core.Items;
using Specimen.Core.Repair;

namespace Specimen.Tests.Core.Repair
{
    [TestFixture]
    public class RepairTests
    {
        private static readonly ItemDefinition Scrap =
            new ItemDefinition("chatarra", "Chatarra", ItemCategory.Material, Rarity.Common, 0, 20);
        private static readonly ItemDefinition Sword =
            new ItemDefinition("espada", "Espada", ItemCategory.Weapon, Rarity.Common, 30);
        private static readonly ItemDefinition Relic =
            new ItemDefinition("reliquia", "Reliquia", ItemCategory.Armor, Rarity.Common, 0);

        private readonly RepairService _service = new RepairService(
            new RepairCatalog(new[] { new RepairRule(ItemCategory.Weapon, Scrap, 10) }));

        private static ItemInstance Worn(ItemDefinition def, int current) =>
            new ItemInstance(def, new Durability(def.MaxDurability, current));

        private static Inventory WithScrap(int n)
        {
            var inv = new Inventory(4);
            if (n > 0) inv.Add(ItemInstance.CreateNew(Scrap), n);
            return inv;
        }

        [Test]
        public void RepairsFullyAndConsumesOnlyNeededUnits()
        {
            var sword = Worn(Sword, 5);
            var inv = WithScrap(10);
            var report = _service.TryRepair(sword, inv);
            Assert.That(report.Result, Is.EqualTo(RepairResult.Repaired));
            Assert.That(report.UnitsUsed, Is.EqualTo(3));
            Assert.That(report.DurabilityRestored, Is.EqualTo(25));
            Assert.That(sword.Durability.Current, Is.EqualTo(30));
            Assert.That(inv.CountOf("chatarra"), Is.EqualTo(7));
        }

        [Test]
        public void RepairsPartiallyWhenMaterialRunsOut()
        {
            var sword = Worn(Sword, 0);
            var inv = WithScrap(2);
            var report = _service.TryRepair(sword, inv);
            Assert.That(report.Result, Is.EqualTo(RepairResult.PartiallyRepaired));
            Assert.That(report.Success, Is.True);
            Assert.That(sword.Durability.Current, Is.EqualTo(20));
            Assert.That(inv.CountOf("chatarra"), Is.EqualTo(0));
        }

        [Test]
        public void BrokenItemBecomesUsableAgain()
        {
            var sword = Worn(Sword, 0);
            Assert.That(sword.IsUsable, Is.False);
            _service.TryRepair(sword, WithScrap(1));
            Assert.That(sword.IsUsable, Is.True);
        }

        [Test]
        public void NoMaterialChangesNothing()
        {
            var sword = Worn(Sword, 10);
            var report = _service.TryRepair(sword, WithScrap(0));
            Assert.That(report.Result, Is.EqualTo(RepairResult.MissingMaterials));
            Assert.That(report.Success, Is.False);
            Assert.That(sword.Durability.Current, Is.EqualTo(10));
        }

        [Test]
        public void IntactItemIsNotTouched()
        {
            var inv = WithScrap(3);
            var report = _service.TryRepair(ItemInstance.CreateNew(Sword), inv);
            Assert.That(report.Result, Is.EqualTo(RepairResult.NothingToRepair));
            Assert.That(inv.CountOf("chatarra"), Is.EqualTo(3));
        }

        [Test]
        public void IndestructibleItemIsNotRepairable()
        {
            var report = _service.TryRepair(ItemInstance.CreateNew(Relic), WithScrap(3));
            Assert.That(report.Result, Is.EqualTo(RepairResult.NotRepairable));
        }

        [Test]
        public void CategoryWithoutRuleIsRejected()
        {
            var armor = new ItemDefinition("coraza", "Coraza", ItemCategory.Armor, Rarity.Common, 20);
            var report = _service.TryRepair(Worn(armor, 1), WithScrap(3));
            Assert.That(report.Result, Is.EqualTo(RepairResult.NoRule));
        }

        [TestCase(1, 1)]
        [TestCase(10, 1)]
        [TestCase(11, 2)]
        public void RuleRoundsUnitsUp(int missing, int units)
        {
            Assert.That(new RepairRule(ItemCategory.Weapon, Scrap, 10).UnitsFor(missing), Is.EqualTo(units));
        }

        [Test]
        public void RejectsInvalidArguments()
        {
            Assert.Throws<ArgumentNullException>(() => new RepairService(null));
            Assert.Throws<ArgumentException>(() => new RepairRule(ItemCategory.Weapon, Sword, 5));
            Assert.Throws<ArgumentOutOfRangeException>(() => new RepairRule(ItemCategory.Weapon, Scrap, 0));
            var dup = new[] { new RepairRule(ItemCategory.Weapon, Scrap, 5), new RepairRule(ItemCategory.Weapon, Scrap, 8) };
            Assert.Throws<ArgumentException>(() => new RepairCatalog(dup));
            Assert.Throws<ArgumentNullException>(() => _service.TryRepair(null, WithScrap(1)));
        }
    }
}
