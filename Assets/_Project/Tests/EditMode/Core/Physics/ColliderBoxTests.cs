using System;
using NUnit.Framework;
using Specimen.Core.Physics;

namespace Specimen.Tests.Core.Physics
{
    [TestFixture]
    public class ColliderBoxTests
    {
        [Test]
        public void ContainsPlanarUsesFootprintOnly()
        {
            var box = new ColliderBox(CollisionLayer.Muro, 1f, 5f, 0f, 2f, 1f, 0.2f);
            Assert.That(box.ContainsPlanar(0.1f, 0.05f), Is.True);
            Assert.That(box.ContainsPlanar(2.5f, 0f), Is.False);
            Assert.That(box.ContainsPlanar(1f, 0.3f), Is.False);
        }

        [Test]
        public void NonPositiveSizeIsRejected() =>
            Assert.Throws<ArgumentOutOfRangeException>(
                () => new ColliderBox(CollisionLayer.Muro, 0f, 0f, 0f, 0f, 1f, 1f));
    }
}
