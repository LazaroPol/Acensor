using System.Linq;
using NUnit.Framework;
using Specimen.Core.Floors;

namespace Specimen.Tests.Core.Floors
{
    public class RingCorridorGeneratorTests
    {
        private static readonly RingCorridorGenerator Generator = new RingCorridorGenerator(RingLayoutConfig.Default);

        private static string Signature(FloorLayout l) =>
            string.Join(",", l.Rooms) + "|" + string.Join(",", l.Corridors);

        [Test]
        public void SameSeed_ProducesIdenticalLayout()
        {
            Assert.That(Signature(Generator.Generate(42)), Is.EqualTo(Signature(Generator.Generate(42))));
        }

        [Test]
        public void DifferentSeeds_VaryTheLayout()
        {
            var distinct = Enumerable.Range(1, 10).Select(s => Signature(Generator.Generate((ulong)s))).Distinct();
            Assert.That(distinct.Count(), Is.GreaterThan(5));
        }

        [Test]
        public void EachRing_HasConfiguredRoomCount()
        {
            var layout = Generator.Generate(7);
            Assert.That(layout.RoomsInRing(0).Single().Kind, Is.EqualTo(RoomKind.Entrance));
            Assert.That(layout.RoomsInRing(1).Count(), Is.EqualTo(3));
            Assert.That(layout.RoomsInRing(2).Count(), Is.EqualTo(4));
            Assert.That(layout.RoomsInRing(3).Count(), Is.EqualTo(5));
            Assert.That(layout.DeepestRing, Is.EqualTo(3));
        }

        [Test]
        public void Corridors_OnlyJoinSameOrAdjacentRings([Range(1, 30)] int seed)
        {
            var layout = Generator.Generate((ulong)seed);
            foreach (var c in layout.Corridors)
                Assert.That(System.Math.Abs(layout.Rooms[c.A].Ring - layout.Rooms[c.B].Ring), Is.LessThanOrEqualTo(1));
        }

        [Test]
        public void AllRooms_AreReachableFromEntrance([Range(1, 30)] int seed)
        {
            Assert.That(FloorConnectivity.IsFullyConnected(Generator.Generate((ulong)seed)), Is.True);
        }

        [Test]
        public void ExactlyOneEmergencyExit_InDeepestRing([Range(1, 30)] int seed)
        {
            var layout = Generator.Generate((ulong)seed);
            var exits = layout.Rooms.Where(r => r.Kind == RoomKind.EmergencyExit).ToList();
            Assert.That(exits, Has.Count.EqualTo(1));
            Assert.That(exits[0].Ring, Is.EqualTo(layout.DeepestRing));
        }

        [Test]
        public void SingleRing_ConnectsEveryRoomToEntrance()
        {
            var layout = new RingCorridorGenerator(new RingLayoutConfig(new[] { 2 }, 1f, 1f)).Generate(3);
            Assert.That(layout.NeighborsOf(0), Is.EquivalentTo(new[] { 1, 2 }));
            Assert.That(layout.Corridors, Has.Count.EqualTo(3));
            Assert.That(layout.RoomsInRing(1).Count(r => r.Kind == RoomKind.EmergencyExit), Is.EqualTo(1));
        }

        [Test]
        public void Layout_RemembersItsSeed()
        {
            Assert.That(Generator.Generate(99).Seed, Is.EqualTo(99UL));
        }
    }
}
