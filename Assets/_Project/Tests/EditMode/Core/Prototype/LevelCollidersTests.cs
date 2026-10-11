using System.Linq;
using NUnit.Framework;
using Specimen.Core.Physics;
using Specimen.Core.Prototype;
using Specimen.Core.Visuals;

namespace Specimen.Tests.Core.Prototype
{
    [TestFixture]
    public class LevelCollidersTests
    {
        private readonly LevelColliders _colliders = new LevelColliders(0.2f, 3f);

        [Test]
        public void OneWallColliderPerWallAndNoneForDoorOrFloor()
        {
            var boxes = _colliders.Build(new GreyboxRoomLayout(6, 4).Build());
            Assert.That(boxes, Has.Count.EqualTo(2 * 6 + 2 * 4 - 1));
            Assert.That(boxes.All(b => b.Layer == CollisionLayer.Muro));
        }

        [Test]
        public void SouthWallSegmentExtendsAlongX()
        {
            var box = _colliders.Build(new[] { new LevelPlacement(VisualId.Muro, 2f, 0f, 0f) }).Single();
            Assert.That((box.CenterX, box.CenterY, box.CenterZ), Is.EqualTo((2.5f, 1.5f, 0f)));
            Assert.That((box.SizeX, box.SizeY, box.SizeZ), Is.EqualTo((1f, 3f, 0.2f)));
        }

        [Test]
        public void WestWallSegmentExtendsAlongZ()
        {
            var wall = new LevelPlacement(VisualId.Muro, 0f, 0f, 3f, GreyboxRoomLayout.WestYaw);
            var box = _colliders.Build(new[] { wall }).Single();
            Assert.That((box.CenterX, box.CenterZ), Is.EqualTo((0f, 3.5f)));
            Assert.That((box.SizeX, box.SizeZ), Is.EqualTo((0.2f, 1f)));
        }

        [Test]
        public void PlayerSpawnIsNotInsideAnyWall()
        {
            var room = new GreyboxRoomLayout(6, 4);
            var boxes = _colliders.Build(room.Build());
            Assert.That(boxes.Any(b => b.ContainsPlanar(3f, 2f)), Is.False);
        }

        [Test]
        public void DoorGapLeavesThePerimeterOpen()
        {
            var room = new GreyboxRoomLayout(6, 4);
            var boxes = _colliders.Build(room.Build());
            Assert.That(boxes.Any(b => b.ContainsPlanar(room.DoorCell + 0.5f, 0f)), Is.False);
        }
    }
}
