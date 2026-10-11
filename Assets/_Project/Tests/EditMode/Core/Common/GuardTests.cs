using System;
using NUnit.Framework;
using Specimen.Core.Common;

namespace Specimen.Tests.Core.Common
{
    [TestFixture]
    public class GuardTests
    {
        [Test]
        public void NotNull_ReturnsValue_WhenNotNull()
        {
            var obj = new object();
            Assert.That(Guard.NotNull(obj, "obj"), Is.SameAs(obj));
        }

        [Test]
        public void NotNull_Throws_WhenNull()
        {
            var ex = Assert.Throws<ArgumentNullException>(() => Guard.NotNull<object>(null, "obj"));
            Assert.That(ex.ParamName, Is.EqualTo("obj"));
        }

        [TestCase(0f)]
        [TestCase(3.5f)]
        public void NonNegative_AcceptsZeroAndPositive(float v) => Assert.That(Guard.NonNegative(v, "v"), Is.EqualTo(v));

        [Test]
        public void NonNegative_Throws_OnNegative()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => Guard.NonNegative(-0.1f, "v"));
            Assert.Throws<ArgumentOutOfRangeException>(() => Guard.NonNegative(-1, "v"));
        }

        [TestCase(0)]
        [TestCase(-5)]
        public void Positive_Throws_OnZeroOrNegative(int v) => Assert.Throws<ArgumentOutOfRangeException>(() => Guard.Positive(v, "v"));

        [Test]
        public void Positive_AcceptsPositive()
        {
            Assert.That(Guard.Positive(1, "v"), Is.EqualTo(1));
            Assert.That(Guard.Positive(0.01f, "v"), Is.EqualTo(0.01f));
        }
    }
}
