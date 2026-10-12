using System;
using System.Collections.Generic;
using NUnit.Framework;
using Specimen.Core.Bestiary;
using Specimen.Core.Combat.Bosses;
using Specimen.Core.Progression;
using Specimen.Core.Stats;

namespace Specimen.Tests.Core.Progression
{
    [TestFixture]
    public class ProgressTests
    {
        [Test]
        public void CurveGrowsAndStopsAtMax()
        {
            for (int l = 1; l < LevelCurve.MaxLevel - 1; l++)
                Assert.That(LevelCurve.XpToNext(l + 1), Is.GreaterThan(LevelCurve.XpToNext(l)));
            Assert.That(LevelCurve.XpToNext(LevelCurve.MaxLevel), Is.EqualTo(0));
            Assert.Throws<ArgumentOutOfRangeException>(() => LevelCurve.XpToNext(0));
        }

        [Test]
        public void AddingXpLevelsUpAndCarriesTheRest()
        {
            var progress = new CharacterProgress();
            var levels = new List<int>();
            progress.LeveledUp += levels.Add;
            int need = LevelCurve.XpToNext(1) + LevelCurve.XpToNext(2);
            Assert.That(progress.AddXp(need + 5), Is.EqualTo(2));
            Assert.That(progress.Level, Is.EqualTo(3));
            Assert.That(progress.Xp, Is.EqualTo(5));
            Assert.That(levels, Is.EqualTo(new[] { 2, 3 }));
            Assert.That(progress.TalentPoints, Is.EqualTo(2));
        }

        [Test]
        public void MaxLevelIgnoresXp()
        {
            var progress = new CharacterProgress(LevelCurve.MaxLevel);
            Assert.That(progress.AddXp(10000), Is.EqualTo(0));
            Assert.That(progress.Fraction, Is.EqualTo(1f));
        }

        [Test]
        public void BossGivesMoreXpAndLowCreaturesLess()
        {
            var imp = DefaultBestiary.Find("diablillo");
            var boss = DefaultBestiary.Find(ActOneBoss.Id);
            Assert.That(XpRewards.ForKill(boss, 1), Is.GreaterThan(XpRewards.ForKill(imp, 1)));
            Assert.That(XpRewards.ForKill(imp, 10), Is.LessThan(XpRewards.ForKill(imp, 1)));
            Assert.That(XpRewards.ForKill(imp, 20), Is.GreaterThanOrEqualTo(1));
        }

        [Test]
        public void LevelStatsScaleAndCanBeReapplied()
        {
            var sheet = new StatSheet();
            sheet.Define(StatType.MaxHealth, 100f);
            sheet.Define(StatType.Damage, 10f);
            LevelStats.Apply(sheet, 6);
            Assert.That(sheet.ValueOf(StatType.MaxHealth), Is.EqualTo(140f).Within(0.01f));
            LevelStats.Apply(sheet, 1);
            Assert.That(sheet.ValueOf(StatType.Damage), Is.EqualTo(10f).Within(0.01f));
        }
    }
}
