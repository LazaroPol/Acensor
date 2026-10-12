using System;
using NUnit.Framework;
using Specimen.Core.Siege.Scoring;

namespace Specimen.Tests.Core.Siege.Scoring
{
    public class IterationScoreTests
    {
        private static IterationScore Score() => new IterationScore(new ScoreConfig(10f, 50f, 0.1f));

        [Test]
        public void Config_RejectsNegativeValues()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => new ScoreConfig(-1f, 0f, 0f));
            Assert.Throws<ArgumentOutOfRangeException>(() => new ScoreConfig(0f, -1f, 0f));
            Assert.Throws<ArgumentOutOfRangeException>(() => new ScoreConfig(0f, 0f, -1f));
            Assert.Throws<ArgumentNullException>(() => new IterationScore(null));
        }

        [Test]
        public void NewScore_IsZero_WithNeutralMultiplier()
        {
            var s = Score();
            Assert.That(s.BasePoints, Is.EqualTo(0f));
            Assert.That(s.Multiplier, Is.EqualTo(1f));
        }

        [Test]
        public void BasePoints_AddKillsLootAndFloorBonus()
        {
            var s = Score();
            s.AddKills(3);
            s.AddLoot(12.5f);
            s.CompleteFloor(4);
            Assert.That(s.BasePoints, Is.EqualTo(3 * 10f + 12.5f + 50f).Within(1e-4));
            Assert.That(s.FloorsCompleted, Is.EqualTo(1));
        }

        [Test]
        public void Multiplier_AccumulatesSurvivorsAcrossFloors()
        {
            var s = Score();
            s.CompleteFloor(4);
            s.CompleteFloor(3);
            s.CompleteFloor(3);
            Assert.That(s.SurvivorFloors, Is.EqualTo(10));
            Assert.That(s.Multiplier, Is.EqualTo(2f).Within(1e-4));
        }

        [Test]
        public void Settle_AppliesMultiplier_OnlyIfSurvived()
        {
            var s = Score();
            s.AddKills(10);
            s.CompleteFloor(2);
            var won = s.Settle(true);
            var dead = s.Settle(false);
            Assert.That(won.Total, Is.EqualTo(150f * 1.2f).Within(1e-3));
            Assert.That(dead.Multiplier, Is.EqualTo(1f));
            Assert.That(dead.Total, Is.EqualTo(150f).Within(1e-3));
        }

        [Test]
        public void Mutators_RejectNegativeValues()
        {
            var s = Score();
            Assert.Throws<ArgumentOutOfRangeException>(() => s.AddKills(-1));
            Assert.Throws<ArgumentOutOfRangeException>(() => s.AddLoot(-1f));
            Assert.Throws<ArgumentOutOfRangeException>(() => s.CompleteFloor(-1));
        }
    }
}
