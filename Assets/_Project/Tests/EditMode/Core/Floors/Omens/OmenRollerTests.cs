using System;
using System.Linq;
using NUnit.Framework;
using Specimen.Core.Floors.Omens;
using Specimen.Core.Floors.Types;
using Specimen.Core.Randomness;

namespace Specimen.Tests.Core.Floors.Omens
{
    public class OmenRollerTests
    {
        private static readonly FloorPlanner Planner = new FloorPlanner(FloorTypeCatalog.Default);
        private static readonly OmenRoller Roller = new OmenRoller(FloorModifierCatalog.Default, 3);

        private static string Sig(Omen o) => string.Join(",", o.Modifiers.Select(m => m.Kind));

        [Test]
        public void SamePlan_IsReproducible()
        {
            for (ulong s = 0; s < 20; s++)
            {
                var plan = Planner.Plan(FloorTypeKind.Waves, s);
                Assert.That(Sig(Roller.Roll(plan)), Is.EqualTo(Sig(Roller.Roll(plan))));
            }
        }

        [Test]
        public void Roll_RespectsMaxDistinctAndType()
        {
            for (ulong s = 0; s < 50; s++)
            {
                var omen = Roller.Roll(Planner.Plan(FloorTypeKind.Loot, s));
                Assert.That(omen.FloorType.Kind, Is.EqualTo(FloorTypeKind.Loot));
                Assert.That(omen.Modifiers.Count, Is.InRange(0, 3));
                Assert.That(omen.Modifiers.Select(m => m.Kind).Distinct().Count(), Is.EqualTo(omen.Modifiers.Count));
            }
        }

        [Test]
        public void PeacefulFloors_NeverGetCombatModifiers()
        {
            for (ulong s = 0; s < 100; s++)
                Assert.That(Roller.Roll(Planner.Plan(FloorTypeKind.Sanctuary, s)).Has(FloorModifierKind.IgneousEnemies), Is.False);
        }

        [Test]
        public void Seeds_ProduceVariety()
        {
            var sigs = Enumerable.Range(0, 40).Select(s => Sig(Roller.Roll(Planner.Plan(FloorTypeKind.Boss, (ulong)s))));
            Assert.That(sigs.Distinct().Count(), Is.GreaterThan(5));
        }

        [Test]
        public void ZeroMax_GivesSilentOmen()
        {
            var omen = new OmenRoller(FloorModifierCatalog.Default, 0).Roll(new LootFloor(), new Pcg32Random(3));
            Assert.That(omen.Modifiers, Is.Empty);
        }

        [Test]
        public void InvalidInput_Throws()
        {
            Assert.Throws<ArgumentNullException>(() => new OmenRoller(null));
            Assert.Throws<ArgumentOutOfRangeException>(() => new OmenRoller(FloorModifierCatalog.Default, -1));
            Assert.Throws<ArgumentNullException>(() => Roller.Roll(null));
            Assert.Throws<ArgumentNullException>(() => Roller.Roll(new LootFloor(), null));
            Assert.Throws<ArgumentNullException>(() => Roller.Roll(null, new Pcg32Random(1)));
        }
    }
}
