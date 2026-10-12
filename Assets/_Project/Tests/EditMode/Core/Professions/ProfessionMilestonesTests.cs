using System;
using NUnit.Framework;
using Specimen.Core.Professions;

namespace Specimen.Tests.Core.Professions
{
    [TestFixture]
    public class ProfessionMilestonesTests
    {
        [TestCase(1, 1, 0f, 3)]
        [TestCase(4, 1, 0f, 3)]
        [TestCase(5, 2, 0.10f, 3)]
        [TestCase(12, 3, 0.20f, 4)]
        [TestCase(15, 4, 0.25f, 5)]
        [TestCase(20, 4, 0.30f, 6)]
        public void DefaultTableFollowsTheGdd(int level, int tier, float time, int maxQuality)
        {
            var step = ProfessionMilestones.Default.For(level);
            Assert.That(step.MaterialTier, Is.EqualTo(tier));
            Assert.That(step.TimeBonus, Is.EqualTo(time).Within(1e-6f));
            Assert.That(step.MaxQuality, Is.EqualTo(maxQuality));
        }

        [Test]
        public void RejectsTablesThatDoNotStartAtOneOrDoNotIncrease()
        {
            var one = new ProfessionMilestone(1, 1, 0f, 0);
            var five = new ProfessionMilestone(5, 2, 0.1f, 0);
            Assert.Throws<ArgumentException>(() => new ProfessionMilestones(new ProfessionMilestone[0]));
            Assert.Throws<ArgumentException>(() => new ProfessionMilestones(new[] { five }));
            Assert.Throws<ArgumentException>(() => new ProfessionMilestones(new[] { one, five, five }));
            Assert.Throws<ArgumentOutOfRangeException>(() => new ProfessionMilestone(1, 1, 1f, 0));
        }
    }
}
