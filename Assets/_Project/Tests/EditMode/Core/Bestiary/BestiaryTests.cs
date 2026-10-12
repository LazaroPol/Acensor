using System;
using System.Linq;
using NUnit.Framework;
using Specimen.Core.Bestiary;
using Specimen.Core.Combat;
using Specimen.Core.Combat.Bosses;
using Specimen.Core.Combat.Enemies;

namespace Specimen.Tests.Core.Bestiary
{
    [TestFixture]
    public class BestiaryTests
    {
        [Test]
        public void HasSixUniqueCreaturesIncludingTheActOneBoss()
        {
            var all = DefaultBestiary.All();
            Assert.That(all.Count, Is.GreaterThanOrEqualTo(6));
            Assert.That(all.Select(e => e.Id).Distinct().Count(), Is.EqualTo(all.Count));
            Assert.That(DefaultBestiary.Find(ActOneBoss.Id), Is.Not.Null);
        }

        [Test]
        public void BothModelsAreUsed()
        {
            var models = DefaultBestiary.All().Select(e => e.Model).Distinct();
            Assert.That(models, Is.EquivalentTo(Enum.GetValues(typeof(MonsterModel))));
        }

        [Test]
        public void ElementalCreaturesResistTheirOwnElement()
        {
            var imp = DefaultBestiary.Find("diablillo");
            Assert.That(imp.Archetype.AttackType, Is.EqualTo(DamageType.Fire));
            Assert.That(imp.Archetype.Resistances.Of(DamageType.Fire), Is.EqualTo(0.5f));
        }

        [Test]
        public void TextureNamesFollowThePack()
        {
            var brute = DefaultBestiary.Find("puglin-bruto");
            Assert.That(brute.ModelName, Is.EqualTo("Puglin"));
            Assert.That(brute.BaseColorTexture, Is.EqualTo("T_Puglin_BaseColor_2"));
            Assert.That(brute.Scale, Is.GreaterThan(1f));
        }

        [Test]
        public void UnknownIdReturnsNull() => Assert.That(DefaultBestiary.Find("nadie"), Is.Null);

        [TestCase(0)]
        [TestCase(4)]
        public void RejectsVariantsOutsideThePack(int variant)
        {
            var archetype = new EnemyArchetype("x", new EnemyStats(1f, 1f, 0f), DamageType.Physical, Resistances.None);
            Assert.Throws<ArgumentOutOfRangeException>(() => new BestiaryEntry(archetype, "X", MonsterModel.Imp, variant));
        }
    }
}
