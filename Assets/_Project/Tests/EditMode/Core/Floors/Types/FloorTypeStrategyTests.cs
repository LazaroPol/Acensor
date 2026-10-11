using System.Linq;
using NUnit.Framework;
using Specimen.Core.Floors;
using Specimen.Core.Floors.Types;

namespace Specimen.Tests.Core.Floors.Types
{
    public class FloorTypeStrategyTests
    {
        [TestCase(FloorTypeKind.Loot, FloorObjective.ReachExit)]
        [TestCase(FloorTypeKind.Boss, FloorObjective.DefeatBoss)]
        [TestCase(FloorTypeKind.Waves, FloorObjective.SurviveWaves)]
        [TestCase(FloorTypeKind.Puzzle, FloorObjective.SolvePuzzle)]
        [TestCase(FloorTypeKind.Decision, FloorObjective.MakeChoice)]
        [TestCase(FloorTypeKind.Sanctuary, FloorObjective.Rest)]
        public void Objective_MatchesType(FloorTypeKind kind, FloorObjective expected)
        {
            Assert.That(FloorTypeCatalog.Default.Get(kind).Objective, Is.EqualTo(expected));
        }

        [Test]
        public void Loot_UsesDefaultRings()
        {
            Assert.That(new LootFloor().Layout.RoomsPerRing, Is.EqualTo(RingLayoutConfig.Default.RoomsPerRing));
        }

        [Test]
        public void Boss_EndsInSingleArena()
        {
            var layout = new BossFloor().Layout;
            Assert.That(layout.RoomsPerRing.Last(), Is.EqualTo(1));
            Assert.That(layout.LateralChance, Is.Zero);
        }

        [Test]
        public void Hybrid_IsLargestFloor()
        {
            int Size(IFloorType t) => t.Layout.RoomsPerRing.Sum();
            var all = FloorTypeCatalog.Default.All;
            Assert.That(Size(new HybridFloor()), Is.EqualTo(all.Max(Size)));
        }

        [Test]
        public void EveryType_GeneratesConnectedLayoutWithExit()
        {
            foreach (var t in FloorTypeCatalog.Default.All)
            {
                var layout = t.CreateGenerator().Generate(42UL);
                Assert.That(FloorConnectivity.IsFullyConnected(layout), Is.True, t.DisplayName);
                Assert.That(layout.Rooms.Count(r => r.Kind == RoomKind.EmergencyExit), Is.EqualTo(1), t.DisplayName);
                Assert.That(layout.DeepestRing, Is.EqualTo(t.Layout.RingCount), t.DisplayName);
            }
        }
    }
}
