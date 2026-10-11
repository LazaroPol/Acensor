using System;
using NUnit.Framework;
using Specimen.Core.Physics;

namespace Specimen.Tests.Core.Physics
{
    [TestFixture]
    public class CollisionMatrixTests
    {
        private readonly CollisionMatrix _default = DefaultCollisionMatrix.Create();

        [Test]
        public void EmptyMatrixCollidesWithNothing()
        {
            var matrix = new CollisionMatrix();
            foreach (CollisionLayer a in Enum.GetValues(typeof(CollisionLayer)))
                foreach (CollisionLayer b in Enum.GetValues(typeof(CollisionLayer)))
                    Assert.That(matrix.Collides(a, b), Is.False);
        }

        [Test]
        public void EnableAndDisableAreSymmetric()
        {
            var matrix = new CollisionMatrix().Enable(CollisionLayer.Clon, CollisionLayer.Trigger);
            Assert.That(matrix.Collides(CollisionLayer.Trigger, CollisionLayer.Clon), Is.True);
            matrix.Disable(CollisionLayer.Trigger, CollisionLayer.Clon);
            Assert.That(matrix.Collides(CollisionLayer.Clon, CollisionLayer.Trigger), Is.False);
        }

        [TestCase(CollisionLayer.Jugador)]
        [TestCase(CollisionLayer.Enemigo)]
        [TestCase(CollisionLayer.Clon)]
        public void WallsBlockMovingBodies(CollisionLayer body) =>
            Assert.That(_default.Collides(CollisionLayer.Muro, body), Is.True);

        [Test]
        public void TriggersOnlyDetectPlayers()
        {
            foreach (CollisionLayer other in Enum.GetValues(typeof(CollisionLayer)))
                Assert.That(_default.Collides(CollisionLayer.Trigger, other),
                    Is.EqualTo(other == CollisionLayer.Jugador), other.ToString());
        }

        [Test]
        public void PlayersDoNotBlockEachOtherAndWallsIgnoreWalls()
        {
            Assert.That(_default.Collides(CollisionLayer.Jugador, CollisionLayer.Jugador), Is.False);
            Assert.That(_default.Collides(CollisionLayer.Muro, CollisionLayer.Muro), Is.False);
        }

        [Test]
        public void UnknownLayerIsRejected() =>
            Assert.Throws<ArgumentOutOfRangeException>(() => _default.Collides((CollisionLayer)99, CollisionLayer.Muro));
    }
}
