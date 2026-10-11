using System;
using NUnit.Framework;
using Specimen.Core.Iteration.Readiness;

namespace Specimen.Tests.Core.Iteration.Readiness
{
    public class LinearHasteBonusTests
    {
        [TestCase(0f, 0f)]
        [TestCase(90f, 0.125f)]
        [TestCase(180f, 0.25f)]
        [TestCase(500f, 0.25f)]
        [TestCase(-5f, 0f)]
        public void Bonus_IsProportionalToSavedTime_AndClamped(float saved, float expected) =>
            Assert.That(new LinearHasteBonus().BonusFor(saved, 180f), Is.EqualTo(expected).Within(1e-6));

        [Test]
        public void CustomMaxBonus_IsRespected() =>
            Assert.That(new LinearHasteBonus(0.5f).BonusFor(30f, 60f), Is.EqualTo(0.25f).Within(1e-6));

        [Test]
        public void InvalidArguments_AreRejected()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => new LinearHasteBonus(-0.1f));
            Assert.Throws<ArgumentOutOfRangeException>(() => new LinearHasteBonus().BonusFor(1f, 0f));
        }
    }
}
