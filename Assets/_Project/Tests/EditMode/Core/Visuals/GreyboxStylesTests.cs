using System;
using NUnit.Framework;
using Specimen.Core.Visuals;

namespace Specimen.Tests.Core.Visuals
{
    [TestFixture]
    public class GreyboxStylesTests
    {
        private readonly DefaultGreyboxStyles _defaults = new DefaultGreyboxStyles();

        [Test]
        public void VisualIdValuesAreStable()
        {
            Assert.That((int)VisualId.Jugador, Is.EqualTo(1));
            Assert.That((int)VisualId.Suelo, Is.EqualTo(2));
            Assert.That((int)VisualId.Muro, Is.EqualTo(3));
            Assert.That((int)VisualId.Puerta, Is.EqualTo(4));
            Assert.That((int)VisualId.Ascensor, Is.EqualTo(5));
            Assert.That((int)VisualId.Loot, Is.EqualTo(6));
            Assert.That((int)VisualId.Enemigo, Is.EqualTo(7));
        }

        [Test]
        public void EveryIdHasAStyle()
        {
            foreach (VisualId id in Enum.GetValues(typeof(VisualId)))
                Assert.That(_defaults.For(id), Is.Not.Null, id.ToString());
        }

        [Test]
        public void CharactersAreHumanSizedCapsules()
        {
            var player = _defaults.For(VisualId.Jugador);
            Assert.That(player.Primitive, Is.EqualTo(GreyboxPrimitive.Capsula));
            Assert.That(player.Scale.Y, Is.EqualTo(1.8f));
            Assert.That(_defaults.For(VisualId.Enemigo).Primitive, Is.EqualTo(GreyboxPrimitive.Capsula));
        }

        [Test]
        public void PlayerAndEnemyAreDistinguishable()
            => Assert.That(_defaults.For(VisualId.Jugador).Color,
                Is.Not.EqualTo(_defaults.For(VisualId.Enemigo).Color));

        [Test]
        public void UnknownIdFailsFast()
            => Assert.Throws<ArgumentOutOfRangeException>(() => _defaults.For((VisualId)999));

        [TestCase(-0.1f, 0f, 0f)]
        [TestCase(0f, 1.1f, 0f)]
        [TestCase(0f, 0f, 2f)]
        public void ColorOutsideUnitRangeFails(float r, float g, float b)
            => Assert.Throws<ArgumentOutOfRangeException>(() => new GreyboxColor(r, g, b));

        [TestCase(0f, 1f, 1f)]
        [TestCase(1f, -1f, 1f)]
        [TestCase(1f, 1f, 0f)]
        public void NonPositiveScaleFails(float x, float y, float z)
            => Assert.Throws<ArgumentOutOfRangeException>(() => new VisualScale(x, y, z));

        [Test]
        public void ValueTypesCompareByValue()
        {
            Assert.That(new GreyboxColor(0.1f, 0.2f, 0.3f), Is.EqualTo(new GreyboxColor(0.1f, 0.2f, 0.3f)));
            Assert.That(new VisualScale(1f, 2f, 3f), Is.EqualTo(new VisualScale(1f, 2f, 3f)));
        }

        [Test]
        public void ResolutionFactoriesRejectNull()
        {
            Assert.Throws<ArgumentNullException>(() => VisualResolution<string>.FromAsset(VisualId.Muro, null));
            Assert.Throws<ArgumentNullException>(() => VisualResolution<string>.FromGreybox(VisualId.Muro, null));
        }
    }
}
