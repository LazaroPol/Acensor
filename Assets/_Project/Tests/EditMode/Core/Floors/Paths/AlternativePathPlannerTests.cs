using System;
using System.Linq;
using NUnit.Framework;
using Specimen.Core.Floors;
using Specimen.Core.Floors.Paths;
using Specimen.Core.Professions;

namespace Specimen.Tests.Core.Floors.Paths
{
    public class AlternativePathPlannerTests
    {
        private static readonly RingCorridorGenerator Generator = new RingCorridorGenerator(RingLayoutConfig.Default);
        private static readonly AlternativePathPlanner Planner = new AlternativePathPlanner(3);
        private static readonly ProfessionId[] Nobody = new ProfessionId[0];

        private static FloorLayout Square() => new FloorLayout(1,
            Enumerable.Range(0, 4).Select(i => new FloorRoom(i, i == 0 ? 0 : 1, i == 0 ? RoomKind.Entrance : RoomKind.Chamber)).ToArray(),
            new[] { new Corridor(0, 1), new Corridor(1, 2), new Corridor(2, 3), new Corridor(3, 0) });

        [Test]
        public void NoProfessionIsMandatory_WholeFloorReachableWithoutOpeners([Range(1, 30)] int seed)
        {
            var layout = Generator.Generate((ulong)seed);
            var paths = Planner.Plan(layout);
            Assert.That(paths.ReachableBy(layout, Nobody).Count, Is.EqualTo(layout.Rooms.Count));
            Assert.That(paths.SealedCorridors.Count, Is.LessThanOrEqualTo(3));
        }

        [Test]
        public void SamePlan_ForSameSeed()
        {
            string Sig(ulong s) => string.Join(",", Planner.Plan(Generator.Generate(s)).SealedCorridors);
            Assert.That(Sig(7), Is.EqualTo(Sig(7)));
            Assert.That(Enumerable.Range(1, 10).Select(s => Sig((ulong)s)).Distinct().Count(), Is.GreaterThan(3));
        }

        [Test]
        public void Square_SealsOnlyOneCorridor_ToStayConnected()
        {
            var paths = Planner.Plan(Square());
            Assert.That(paths.SealedCorridors, Has.Count.EqualTo(1));
        }

        [Test]
        public void SealedCorridor_IsShortcut_OnlyForOpeners()
        {
            var layout = Square();
            var paths = new FloorPaths(new[] { new Corridor(0, 1) });
            Assert.That(paths.IsSealed(new Corridor(1, 0)), Is.True);
            Assert.That(paths.ReachableBy(layout, new[] { ProfessionId.Manitas }).Count, Is.EqualTo(4));
            var cut = new FloorLayout(1, layout.Rooms, new[] { new Corridor(0, 1) });
            Assert.That(paths.ReachableBy(cut, new[] { ProfessionId.Herrero }), Is.EquivalentTo(new[] { 0 }));
            Assert.That(paths.ReachableBy(cut, new[] { ProfessionId.Quimico }), Is.EquivalentTo(new[] { 0, 1 }));
        }

        [Test]
        public void ZeroDoors_PlansNone_AndNegativeThrows()
        {
            Assert.That(new AlternativePathPlanner(0).Plan(Square()).SealedCorridors, Is.Empty);
            Assert.Throws<ArgumentOutOfRangeException>(() => new AlternativePathPlanner(-1));
            Assert.That(FloorPaths.None.SealedCorridors, Is.Empty);
        }
    }
}
