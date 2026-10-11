using System;
using System.Linq;
using NUnit.Framework;
using Specimen.Core.Prototype;
using Specimen.Core.Visuals;

namespace Specimen.Tests.Core.Prototype
{
    [TestFixture]
    public class GreyboxRoomLayoutTests
    {
        private readonly GreyboxRoomLayout _room = new GreyboxRoomLayout(6, 4);

        private int Count(VisualId id) => _room.Build().Count(p => p.Id == id);

        [Test]
        public void FloorCoversEveryCellOfTheGrid()
        {
            var floor = _room.Build().Where(p => p.Id == VisualId.Suelo).ToList();
            Assert.That(floor, Has.Count.EqualTo(24));
            Assert.That(floor.Select(p => (p.X, p.Z)).Distinct().Count(), Is.EqualTo(24));
            Assert.That(floor.All(p => p.X >= 0 && p.X < 6 && p.Z >= 0 && p.Z < 4));
        }

        [Test]
        public void PerimeterIsClosedByWallsAndOneDoor()
        {
            Assert.That(Count(VisualId.Muro), Is.EqualTo(2 * 6 + 2 * 4 - 1));
            Assert.That(Count(VisualId.Puerta), Is.EqualTo(1));
        }

        [Test]
        public void DoorSitsInTheMiddleOfTheSouthWall()
        {
            var door = _room.Build().Single(p => p.Id == VisualId.Puerta);
            Assert.That((door.X, door.Z, door.YawDegrees), Is.EqualTo((3f, 0f, 0f)));
        }

        [Test]
        public void SideWallsAreTurnedToRunAlongZ()
        {
            var side = _room.Build().Where(p => p.Id == VisualId.Muro && (p.X == 0f || p.X == 6f) && p.YawDegrees != 0f);
            Assert.That(side.Count(), Is.EqualTo(8));
            Assert.That(side.All(p => p.YawDegrees == GreyboxRoomLayout.WestYaw));
        }

        [Test]
        public void PlayerSpawnsOnceAtTheCenterOfTheRoom()
        {
            var player = _room.Build().Single(p => p.Id == VisualId.Jugador);
            Assert.That((player.X, player.Y, player.Z), Is.EqualTo((3f, 0f, 2f)));
        }

        [Test]
        public void EverythingRestsOnTheGround()
            => Assert.That(_room.Build().All(p => p.Y == 0f));

        [Test]
        public void BuildIsDeterministic()
            => Assert.That(_room.Build().Select(p => p.ToString()),
                Is.EqualTo(_room.Build().Select(p => p.ToString())));

        [TestCase(0, 4)]
        [TestCase(4, 0)]
        [TestCase(-1, 3)]
        public void RejectsEmptyRooms(int width, int depth)
            => Assert.That(() => new GreyboxRoomLayout(width, depth),
                Throws.InstanceOf<ArgumentOutOfRangeException>());
    }
}
