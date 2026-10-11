using System;
using NUnit.Framework;
using Specimen.Core.Elevator;
using static Specimen.Tests.Core.Elevator.ElevatorFixtures;

namespace Specimen.Tests.Core.Elevator
{
    [TestFixture]
    public class ElevatorPlatformTests
    {
        [Test]
        public void ValidatesConstructor()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => new ElevatorPlatform(0, Catalog()));
            Assert.Throws<ArgumentNullException>(() => new ElevatorPlatform(2, null));
        }

        [Test]
        public void StartsWithEmptyIndexedSlots()
        {
            var p = Platform(3);
            Assert.That(p.Slots, Has.Count.EqualTo(3));
            Assert.That(p.Slots, Has.All.Matches<ModuleSlot>(s => s.IsEmpty));
            Assert.That(p.Slots[2].Index, Is.EqualTo(2));
        }

        [Test]
        public void BuildConsumesMaterialsAndOccupiesSlot()
        {
            var p = Platform();
            var inv = Stocked(5, 3);
            ModuleSlot notified = null;
            p.ModuleBuilt += s => notified = s;
            Assert.That(p.TryBuild(1, ModuleType.Bonfire, inv), Is.EqualTo(BuildResult.Built));
            Assert.That(inv.CountOf("madera"), Is.EqualTo(2));
            Assert.That(inv.CountOf("piedra"), Is.EqualTo(1));
            Assert.That(p.Slots[1].Module, Is.EqualTo(ModuleType.Bonfire));
            Assert.That(p.Has(ModuleType.Bonfire), Is.True);
            Assert.That(notified, Is.SameAs(p.Slots[1]));
        }

        [TestCase(-1, ModuleType.Forge, BuildResult.InvalidSlot)]
        [TestCase(3, ModuleType.Forge, BuildResult.InvalidSlot)]
        [TestCase(0, ModuleType.Forge, BuildResult.SlotOccupied)]
        [TestCase(1, ModuleType.Bonfire, BuildResult.AlreadyBuilt)]
        [TestCase(1, ModuleType.Bunk, BuildResult.UnknownBlueprint)]
        public void FailuresLeaveInventoryUntouched(int slot, ModuleType type, BuildResult expected)
        {
            var p = Platform();
            p.TryBuild(0, ModuleType.Bonfire, Stocked(3, 2));
            var inv = Stocked(9, 9);
            bool notified = false;
            p.ModuleBuilt += _ => notified = true;
            Assert.That(p.TryBuild(slot, type, inv), Is.EqualTo(expected));
            Assert.That(inv.CountOf("madera") + inv.CountOf("piedra"), Is.EqualTo(18));
            Assert.That(notified, Is.False);
        }

        [Test]
        public void FailsWithoutEnoughMaterials()
        {
            var p = Platform();
            var inv = Stocked(0, 3);
            Assert.That(p.TryBuild(0, ModuleType.Forge, inv), Is.EqualTo(BuildResult.MissingMaterials));
            Assert.That(inv.CountOf("piedra"), Is.EqualTo(3));
            Assert.That(p.Slots[0].IsEmpty, Is.True);
        }

        [Test]
        public void ModulesPersistUntilReset()
        {
            var p = Platform();
            p.TryBuild(0, ModuleType.Forge, Stocked(0, 4));
            p.TryBuild(2, ModuleType.Bonfire, Stocked(3, 2));
            Assert.That(p.Has(ModuleType.Forge) && p.Has(ModuleType.Bonfire), Is.True);
            p.Reset();
            Assert.That(p.Slots, Has.All.Matches<ModuleSlot>(s => s.IsEmpty));
            Assert.That(p.TryBuild(0, ModuleType.Forge, Stocked(0, 4)), Is.EqualTo(BuildResult.Built));
        }

        [Test]
        public void RejectsNullInventory()
        {
            Assert.Throws<ArgumentNullException>(() => Platform().TryBuild(0, ModuleType.Forge, null));
        }
    }
}
