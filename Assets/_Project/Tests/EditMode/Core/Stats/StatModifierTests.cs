using System;
using NUnit.Framework;
using Specimen.Core.Stats;

namespace Specimen.Tests.Core.Stats
{
    [TestFixture]
    public class StatModifierTests
    {
        [Test]
        public void Factories_SetKindValueAndSource()
        {
            var src = new object();
            var m = StatModifier.Percent(0.25f, src);
            Assert.That(m.Kind, Is.EqualTo(ModifierKind.PercentAdd));
            Assert.That(m.Value, Is.EqualTo(0.25f));
            Assert.That(m.Source, Is.SameAs(src));
            Assert.That(StatModifier.Flat(1f).Kind, Is.EqualTo(ModifierKind.Flat));
            Assert.That(StatModifier.Multiply(2f).Kind, Is.EqualTo(ModifierKind.Multiplier));
        }

        [TestCase(float.NaN)]
        [TestCase(float.PositiveInfinity)]
        public void Throws_OnNonFiniteValue(float v)
            => Assert.Throws<ArgumentOutOfRangeException>(() => StatModifier.Flat(v));

        [Test]
        public void Throws_OnNegativeMultiplier()
            => Assert.Throws<ArgumentOutOfRangeException>(() => StatModifier.Multiply(-1f));
    }
}
