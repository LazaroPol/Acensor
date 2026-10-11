using System;
using NUnit.Framework;
using Specimen.Core.Crafting;
using Specimen.Core.Elevator;
using Specimen.Core.Items;
using static Specimen.Tests.Core.Elevator.ElevatorFixtures;

namespace Specimen.Tests.Core.Elevator
{
    [TestFixture]
    public class ModuleUpgradeTests
    {
        private static ElevatorPlatform WithBonfire(out Inventory inv, int wood, int stone)
        {
            var p = Platform();
            inv = Stocked(wood + 3, stone + 2);
            Assert.That(p.TryBuild(0, ModuleType.Bonfire, inv), Is.EqualTo(BuildResult.Built));
            return p;
        }

        [Test]
        public void BlueprintExposesMaxLevelAndUpgradeCosts()
        {
            Assert.That(BonfirePlan().MaxLevel, Is.EqualTo(3));
            Assert.That(ForgePlan().MaxLevel, Is.EqualTo(1));
            Assert.That(BonfirePlan().TryGetUpgradeCost(2, out var cost), Is.True);
            Assert.That(cost[0].Quantity, Is.EqualTo(3));
            Assert.That(BonfirePlan().TryGetUpgradeCost(3, out _), Is.False);
            Assert.That(BonfirePlan().TryGetUpgradeCost(0, out _), Is.False);
        }

        [Test]
        public void BlueprintRejectsInvalidUpgradeCosts()
        {
            var basic = new[] { new RecipeIngredient(Wood, 1) };
            Assert.Throws<ArgumentException>(() => new ModuleBlueprint(ModuleType.Forge, basic,
                new[] { new RecipeIngredient[0] }));
            Assert.Throws<ArgumentNullException>(() => new ModuleBlueprint(ModuleType.Forge, basic,
                new RecipeIngredient[][] { null }));
        }

        [Test]
        public void BuiltModuleStartsAtLevelOneAndEmptyAtZero()
        {
            var p = WithBonfire(out _, 0, 0);
            Assert.That(p.Slots[0].Level, Is.EqualTo(1));
            Assert.That(p.Slots[1].Level, Is.EqualTo(0));
        }

        [Test]
        public void UpgradePaysNextLevelCostAndNotifies()
        {
            var p = WithBonfire(out var inv, 2, 3);
            ModuleSlot notified = null;
            p.ModuleUpgraded += s => notified = s;
            Assert.That(p.TryUpgrade(0, inv), Is.EqualTo(UpgradeResult.Upgraded));
            Assert.That(p.TryUpgrade(0, inv), Is.EqualTo(UpgradeResult.Upgraded));
            Assert.That(p.Slots[0].Level, Is.EqualTo(3));
            Assert.That(inv.CountOf("madera"), Is.EqualTo(0));
            Assert.That(inv.CountOf("piedra"), Is.EqualTo(0));
            Assert.That(notified, Is.SameAs(p.Slots[0]));
        }

        [TestCase(-1, UpgradeResult.InvalidSlot)]
        [TestCase(3, UpgradeResult.InvalidSlot)]
        [TestCase(1, UpgradeResult.EmptySlot)]
        public void UpgradeFailsOnBadSlotWithoutTouchingInventory(int slot, UpgradeResult expected)
        {
            var p = WithBonfire(out var inv, 5, 5);
            Assert.That(p.TryUpgrade(slot, inv), Is.EqualTo(expected));
            Assert.That(inv.CountOf("madera"), Is.EqualTo(5));
        }

        [Test]
        public void UpgradeFailsAtMaxLevelOrWithoutMaterials()
        {
            var p = WithBonfire(out var inv, 1, 0);
            Assert.That(p.TryUpgrade(0, inv), Is.EqualTo(UpgradeResult.MissingMaterials));
            Assert.That(inv.CountOf("madera"), Is.EqualTo(1));
            var inv2 = Stocked(0, 4);
            Assert.That(p.TryBuild(1, ModuleType.Forge, inv2), Is.EqualTo(BuildResult.Built));
            Assert.That(p.TryUpgrade(1, Stocked(9, 9)), Is.EqualTo(UpgradeResult.MaxLevel));
        }

        [Test]
        public void ResetClearsLevels()
        {
            var p = WithBonfire(out var inv, 2, 0);
            p.TryUpgrade(0, inv);
            p.Reset();
            Assert.That(p.Slots[0].Level, Is.EqualTo(0));
            Assert.Throws<ArgumentNullException>(() => p.TryUpgrade(0, null));
        }
    }
}
