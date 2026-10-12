using NUnit.Framework;
using Specimen.Core.Professions;

namespace Specimen.Tests.Core.Professions
{
    [TestFixture]
    public class ProfessionSkillTests
    {
        private static ProfessionSkill Skill() =>
            new ProfessionSkill(ProfessionId.Herrero, new ProfessionCurve(new[] { 10, 20, 30 }));

        [Test]
        public void GainingXpCanRaiseSeveralLevels()
        {
            var skill = Skill();
            Assert.That(skill.Gain(5), Is.EqualTo(0));
            Assert.That(skill.Gain(30), Is.EqualTo(2));
            Assert.That(skill.Level, Is.EqualTo(3));
            Assert.That(skill.Xp, Is.EqualTo(5));
        }

        [Test]
        public void NeverPassesTheMaximum()
        {
            var skill = Skill();
            Assert.That(skill.Gain(1000), Is.EqualTo(3));
            Assert.That(skill.Level, Is.EqualTo(4));
            Assert.That(skill.IsMaxed, Is.True);
            Assert.That(skill.Xp, Is.EqualTo(0));
            Assert.That(skill.Gain(50), Is.EqualTo(0));
        }

        [Test]
        public void LevelsKeepOneSkillPerProfession()
        {
            var levels = new ProfessionLevels();
            levels.For(ProfessionId.Sastre).Gain(40);
            Assert.That(levels.LevelOf(ProfessionId.Sastre), Is.EqualTo(2));
            Assert.That(levels.LevelOf(ProfessionId.Herrero), Is.EqualTo(1));
            Assert.That(levels.For(ProfessionId.Sastre).Profession, Is.EqualTo(ProfessionId.Sastre));
            Assert.That(levels.All, Has.Exactly(7).Items);
        }
    }
}
