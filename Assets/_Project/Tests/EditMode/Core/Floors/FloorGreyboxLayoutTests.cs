using System.Linq;
using NUnit.Framework;
using Specimen.Core.Floors;
using Specimen.Core.Visuals;

namespace Specimen.Tests.Core.Floors
{
    public class FloorGreyboxLayoutTests
    {
        private static FloorGreyboxLayout Layout(ulong seed = 7, FloorLayout floor = null) =>
            new FloorGreyboxLayout(floor ?? new RingCorridorGenerator(RingLayoutConfig.Default).Generate(seed));

        [Test]
        public void Build_HasOneElevatorAndOneEmergencyDoor()
        {
            var placements = Layout().Build();
            Assert.That(placements.Count(p => p.Id == VisualId.Ascensor), Is.EqualTo(1));
            Assert.That(placements.Count(p => p.Id == VisualId.Puerta), Is.EqualTo(1));
        }

        [Test]
        public void Build_EveryRoomCenterHasFloor()
        {
            var floor = new RingCorridorGenerator(RingLayoutConfig.Default).Generate(3);
            var layout = new FloorGreyboxLayout(floor);
            var cells = layout.Build().Where(p => p.Id == VisualId.Suelo).Select(p => (p.X, p.Z)).ToList();
            foreach (var room in floor.Rooms)
                Assert.That(cells, Does.Contain(layout.CenterOf(room.Id)));
        }

        [Test]
        public void Build_NoDuplicateFloorCells()
        {
            var cells = Layout().Build().Where(p => p.Id == VisualId.Suelo).Select(p => (p.X, p.Z)).ToList();
            Assert.That(cells.Distinct().Count(), Is.EqualTo(cells.Count));
        }

        [Test]
        public void ShaftPoint_IsTheEmergencyExitDoor()
        {
            var layout = Layout();
            var door = layout.Build().First(p => p.Id == VisualId.Puerta);
            Assert.That(layout.ShaftPoint, Is.EqualTo((door.X, door.Z)));
        }

        [Test]
        public void SpawnPoint_IsEntranceCenter()
        {
            var layout = Layout();
            Assert.That(layout.SpawnPoint, Is.EqualTo(layout.CenterOf(0)));
        }
    }
}
