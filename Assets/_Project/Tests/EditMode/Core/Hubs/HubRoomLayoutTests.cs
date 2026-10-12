using System.Linq;
using NUnit.Framework;
using Specimen.Core.Hubs;
using Specimen.Core.Visuals;

namespace Specimen.Tests.Core.Hubs
{
    public class HubRoomLayoutTests
    {
        private static readonly HubRoomLayout Room = new HubRoomLayout(12, 8, 4);

        [Test]
        public void Build_HasOneStationPerKitAndOneElevator()
        {
            var placements = Room.Build();
            Assert.That(placements.Count(p => p.Id == VisualId.Loot), Is.EqualTo(4));
            Assert.That(placements.Count(p => p.Id == VisualId.Ascensor), Is.EqualTo(1));
        }

        [Test]
        public void Stations_AreInsideTheRoomAndSpreadOut()
        {
            var xs = Enumerable.Range(0, 4).Select(i => Room.StationPoint(i).X).ToList();
            Assert.That(xs, Is.Ordered);
            Assert.That(xs.First(), Is.GreaterThan(1f));
            Assert.That(xs.Last(), Is.LessThan(11f));
        }

        [Test]
        public void StationPoint_OutOfRange_Throws()
        {
            Assert.That(() => Room.StationPoint(4), Throws.InstanceOf<System.ArgumentOutOfRangeException>());
        }

        [Test]
        public void Nearest_PicksClosestWithinRangeOnly()
        {
            var points = new[] { (1f, 1f), (5f, 5f), (9f, 9f) };
            Assert.That(ProximityPicker.Nearest(points, 5.5f, 5f, 2f), Is.EqualTo(1));
            Assert.That(ProximityPicker.Nearest(points, 20f, 20f, 2f), Is.EqualTo(-1));
        }
    }
}
