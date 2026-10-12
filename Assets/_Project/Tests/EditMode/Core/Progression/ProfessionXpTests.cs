using NUnit.Framework;
using Specimen.Core.Professions;
using Specimen.Core.Progression;

namespace Specimen.Tests.Core.Progression
{
    [TestFixture]
    public class ProfessionXpTests
    {
        [TestCase(10, 10, 100)]
        [TestCase(10, 13, 100)]
        [TestCase(10, 7, 50)]
        [TestCase(10, 5, 50)]
        [TestCase(10, 4, 10)]
        public void XpShrinksWithTheRecipeLevelGap(int skillLevel, int recipeLevel, int expected)
        {
            Assert.That(ProfessionXp.Scaled(100, skillLevel, recipeLevel), Is.EqualTo(expected));
        }

        [Test]
        public void AwardAppliesTheScaledXpToTheSkill()
        {
            var skill = new ProfessionSkill(ProfessionId.Cocinero, new ProfessionCurve(new[] { 10, 20 }));
            Assert.That(ProfessionXp.Award(skill, 15, 1), Is.EqualTo(1));
            Assert.That(skill.Xp, Is.EqualTo(5));
        }

        [Test]
        public void HeroProgressionTracksEveryProfessionLevel()
        {
            var hero = new HeroProgression();
            ProfessionXp.Award(hero.ProfessionLevels.For(ProfessionId.Herrero), 40, 1);
            Assert.That(hero.ProfessionLevels.LevelOf(ProfessionId.Herrero), Is.EqualTo(2));
            Assert.That(hero.Professions.Knows(ProfessionId.Herrero), Is.False);
        }
    }
}
