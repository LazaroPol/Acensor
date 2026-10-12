using System;
using NUnit.Framework;
using Specimen.Core.Professions;

namespace Specimen.Tests.Core.Professions
{
    [TestFixture]
    public class ProfessionCurveTests
    {
        [Test]
        public void SeedFollowsFortyTimesLevelPowerOnePointFive()
        {
            var curve = ProfessionCurve.Default;
            Assert.That(curve.MaxLevel, Is.EqualTo(20));
            Assert.That(curve.XpToNext(1), Is.EqualTo(40));
            Assert.That(curve.XpToNext(4), Is.EqualTo(320));
            Assert.That(curve.XpToNext(20), Is.EqualTo(0));
        }

        [Test]
        public void MaxLevelComesFromTheTable()
        {
            Assert.That(new ProfessionCurve(new[] { 10, 20, 30 }).MaxLevel, Is.EqualTo(4));
        }

        [Test]
        public void RejectsEmptyOrNonIncreasingTables()
        {
            Assert.Throws<ArgumentException>(() => new ProfessionCurve(new int[0]));
            Assert.Throws<ArgumentException>(() => new ProfessionCurve(new[] { 10, 10 }));
            Assert.Throws<ArgumentException>(() => new ProfessionCurve(new[] { 20, 10 }));
            Assert.Throws<ArgumentException>(() => new ProfessionCurve(new[] { 0, 10 }));
            Assert.Throws<ArgumentNullException>(() => new ProfessionCurve(null));
        }
    }
}
