using System;
using System.Linq;
using NUnit.Framework;
using Specimen.Core.Crafting;
using Specimen.Core.Items;

namespace Specimen.Tests.Core.Crafting
{
    [TestFixture]
    public class QualityRulesTests
    {
        [Test]
        public void DefaultGradesHaveGddSlots()
        {
            var slots = QualityRules.Default.Grades.Select(g => g.ModSlots).ToArray();
            Assert.That(slots, Is.EqualTo(new[] { 1, 1, 2, 2, 3, 4 }));
            Assert.That(QualityRules.Default.Grade(Quality.Legendary).StatMultiplier, Is.EqualTo(1.5f));
            Assert.That(QualityRules.Default.Grade(Quality.Crude).Name, Is.EqualTo("Tosca"));
        }

        [TestCase(-5, Quality.Crude)]
        [TestCase(7, Quality.Crude)]
        [TestCase(8, Quality.Common)]
        [TestCase(14, Quality.Good)]
        [TestCase(28, Quality.Fine)]
        [TestCase(29, Quality.Masterwork)]
        [TestCase(37, Quality.Legendary)]
        public void ScoreMapsToThresholds(int score, Quality expected)
        {
            Assert.That(QualityRules.Default.FromScore(score), Is.EqualTo(expected));
        }

        [Test]
        public void RejectsIncompleteOrUnorderedTables()
        {
            var five = QualityRules.Default.Grades.Take(5);
            Assert.Throws<ArgumentException>(() => new QualityRules(five, QualityWeights.Default));
            var swapped = QualityRules.Default.Grades
                .Select(g => new QualityGrade(g.Quality, g.Name, 50 - (int)g.Quality, 1, 1f, 1f));
            Assert.Throws<ArgumentException>(() => new QualityRules(swapped, QualityWeights.Default));
        }

        [Test]
        public void WeightedTierUsesQuantities()
        {
            var tier = QualityRollInput.WeightedTier(new[] { (1, 3), (4, 1) });
            Assert.That(tier, Is.EqualTo(1.75f).Within(1e-6f));
            Assert.That(QualityRollInput.WeightedTier(new (int, int)[0]), Is.EqualTo(0f));
        }

        [Test]
        public void BaseScoreCombinesAllLevers()
        {
            var input = new QualityRollInput(10, 2.5f, 2, 3, 6);
            // 10·1 + round(7,5)=8 + 2·2 + 3
            Assert.That(QualityWeights.Default.BaseScore(input), Is.EqualTo(25));
        }
    }
}
