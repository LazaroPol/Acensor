using NUnit.Framework;
using Specimen.Core.Visuals;

namespace Specimen.Tests.Core.Visuals
{
    [TestFixture]
    public class VisualAnchorTests
    {
        [TestCase(VisualId.Suelo)]
        [TestCase(VisualId.Muro)]
        [TestCase(VisualId.Puerta)]
        [TestCase(VisualId.Ascensor)]
        public void ModularEnvironmentIsAnchoredAtTheCorner(VisualId id)
            => Assert.That(VisualAnchors.For(id), Is.EqualTo(VisualAnchor.Esquina));

        [TestCase(VisualId.Jugador)]
        [TestCase(VisualId.Enemigo)]
        [TestCase(VisualId.Loot)]
        public void CharactersAndObjectsAreAnchoredAtTheBase(VisualId id)
            => Assert.That(VisualAnchors.For(id), Is.EqualTo(VisualAnchor.Base));

        [Test]
        public void CornerAnchorCentersThePrimitiveInsideItsBox()
        {
            var offset = GreyboxOffset.CenterOf(Style(1f, 3f, 0.2f), VisualAnchor.Esquina);
            Assert.That(offset.X, Is.EqualTo(0.5f));
            Assert.That(offset.Y, Is.EqualTo(1.5f));
            Assert.That(offset.Z, Is.EqualTo(0.1f));
        }

        [Test]
        public void BaseAnchorOnlyLiftsThePrimitiveToStandOnTheFloor()
        {
            var offset = GreyboxOffset.CenterOf(Style(1f, 1.8f, 1f), VisualAnchor.Base);
            Assert.That(offset.X, Is.EqualTo(0f));
            Assert.That(offset.Y, Is.EqualTo(0.9f));
            Assert.That(offset.Z, Is.EqualTo(0f));
        }

        [Test]
        public void OffsetRequiresAStyle()
            => Assert.That(() => GreyboxOffset.CenterOf(null, VisualAnchor.Base),
                Throws.ArgumentNullException);

        private static GreyboxStyle Style(float x, float y, float z)
            => new GreyboxStyle(GreyboxPrimitive.Cubo, new GreyboxColor(0f, 0f, 0f),
                new VisualScale(x, y, z));
    }
}
