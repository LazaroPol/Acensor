using System.Linq;
using NUnit.Framework;
using Specimen.Core.Bestiary;
using Specimen.Core.Combat;
using Specimen.Core.Combat.Targeting;
using Specimen.Core.Progression;
using Specimen.Core.Randomness;
using Specimen.Core.Sandbox;

namespace Specimen.Tests.Core.Equipment
{
    [TestFixture]
    public class CombatRulesTests
    {
        [Test]
        public void ArmorFollowsTheWowFormulaAndCaps()
        {
            Assert.That(ArmorRules.Reduction(485f, 1), Is.EqualTo(0.5f).Within(1e-4));
            Assert.That(ArmorRules.Reduction(485f, 5), Is.LessThan(ArmorRules.Reduction(485f, 1)));
            Assert.That(ArmorRules.Reduction(1e6f, 1), Is.EqualTo(ArmorRules.Cap));
            Assert.That(ArmorRules.Reduction(0f, 3), Is.EqualTo(0f));
        }

        [Test]
        public void MoreArmorMeansLessPhysicalDamage()
        {
            var hit = new Hit(100f, DamageType.Physical, false);
            var crit = new CritRules(0f);
            var light = new DamageCalculator(new Pcg32Random(1), ArmorRules.MitigationFor(2))
                .Resolve(hit, crit, new DefenseProfile(50f, Resistances.None));
            var heavy = new DamageCalculator(new Pcg32Random(1), ArmorRules.MitigationFor(2))
                .Resolve(hit, crit, new DefenseProfile(250f, Resistances.None));
            Assert.That(heavy.Final, Is.LessThan(light.Final));
            Assert.That(light.Final, Is.EqualTo(100f * (1f - ArmorRules.Reduction(50f, 2))).Within(1e-3));
        }

        [Test]
        public void TabCyclesFromNearestAndWraps()
        {
            var enemies = new[] { (1, 12f), (2, 4f), (3, 8f), (4, 50f) };
            Assert.That(TabTargeting.Next(enemies, null), Is.EqualTo(2));
            Assert.That(TabTargeting.Next(enemies, 2), Is.EqualTo(3));
            Assert.That(TabTargeting.Next(enemies, 1), Is.EqualTo(2));
            Assert.That(TabTargeting.Next(enemies, 99), Is.EqualTo(2));
            Assert.That(TabTargeting.Next(new (int, float)[0], null), Is.Null);
        }

        [Test]
        public void CreaturesGetTougherWithLevelAndTheBossDropsEpicGear()
        {
            var boss = DefaultBestiary.All().Last();
            Assert.That(CreatureLevels.LevelOf(boss), Is.GreaterThan(CreatureLevels.LevelOf(DefaultBestiary.All()[0])));
            Assert.That(CreatureLevels.Health(100f, 3), Is.EqualTo(150f).Within(1e-3));
            Assert.That(CreatureLevels.Armor(0f, 2), Is.EqualTo(80f));
            var drops = SandboxGearLoot.Roll(boss, new Pcg32Random(7));
            Assert.That(drops.Count, Is.EqualTo(1));
            Assert.That(drops[0].Item.Rarity, Is.EqualTo(Specimen.Core.Items.Rarity.Epic));
        }

        [Test]
        public void DifficultyColorsFollowTheLevelGap()
        {
            Assert.That(LevelDifficulty.Of(1, 8), Is.EqualTo(Difficulty.Rojo));
            Assert.That(LevelDifficulty.Of(1, 4), Is.EqualTo(Difficulty.Naranja));
            Assert.That(LevelDifficulty.Of(5, 4), Is.EqualTo(Difficulty.Amarillo));
            Assert.That(LevelDifficulty.Of(8, 4), Is.EqualTo(Difficulty.Verde));
            Assert.That(LevelDifficulty.Of(12, 2), Is.EqualTo(Difficulty.Gris));
        }
    }
}
