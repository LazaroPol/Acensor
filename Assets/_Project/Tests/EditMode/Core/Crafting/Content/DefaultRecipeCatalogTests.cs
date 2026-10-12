using System;
using System.Linq;
using NUnit.Framework;
using Specimen.Core.Crafting;
using Specimen.Core.Crafting.Content;
using Specimen.Core.Crafting.Definitions;
using Specimen.Core.Crafting.Stations;
using Specimen.Core.Items;
using Specimen.Core.Professions;

namespace Specimen.Tests.Core.Crafting.Content
{
    /// <summary>Recetario por defecto de §17.6 (OF-12): escalera por oficio, desbloqueos e integración.</summary>
    [TestFixture]
    public class DefaultRecipeCatalogTests
    {
        private static readonly RecipeCatalog Catalog = DefaultRecipeCatalog.Create();

        private static ProfessionId[] Professions => (ProfessionId[])Enum.GetValues(typeof(ProfessionId));

        [Test]
        public void HasAtLeast70RecipesAndEveryProfessionClimbsFourTiers()
        {
            Assert.That(Catalog.Count, Is.GreaterThanOrEqualTo(70));
            foreach (var p in Professions)
            {
                var tiers = Catalog.ForProfession(p).Select(r => RecipeTiers.Of(r.MinProfessionLevel)).ToList();
                for (var t = RecipeTiers.Min; t <= RecipeTiers.Max; t++)
                    Assert.That(tiers, Does.Contain(t), $"{p} T{t}");
            }
        }

        [Test]
        public void RecipesFollowTheLadderOfLevelsAndStations()
        {
            foreach (var r in Catalog.All)
            {
                var tier = RecipeTiers.Of(r.MinProfessionLevel);
                Assert.That(r.MinProfessionLevel, Is.EqualTo(RecipeTiers.ProfessionLevel(tier)), r.Id);
                Assert.That(r.Station, Is.EqualTo(StationTypes.MainOf(r.Profession)), r.Id);
                Assert.That(r.MinStationLevel, Is.EqualTo(RecipeTiers.StationLevel(tier, r.RequiredParts.Count > 0)), r.Id);
                Assert.That(r.Id, Does.StartWith(r.Profession.ToString().ToLowerInvariant() + "."), r.Id);
                Assert.That(r.Xp, Is.EqualTo(RecipeTiers.Xp(tier)), r.Id);
            }
        }

        [Test]
        public void StarterRecipesAreKnownAndNeedNoPart()
        {
            foreach (var r in Catalog.All.Where(r => RecipeTiers.Of(r.MinProfessionLevel) == 1))
            {
                Assert.That(r.Unlock, Is.SameAs(UnlockCondition.AlwaysKnown), r.Id);
                Assert.That(r.RequiredParts, Is.Empty, r.Id);
            }
        }

        [Test]
        public void EveryNonStarterRecipeHasAKnowledgeOrPartThatUnlocksIt()
        {
            foreach (var r in Catalog.All.Where(r => RecipeTiers.Of(r.MinProfessionLevel) > 1))
                Assert.That(r.RequiredParts.Count > 0 || !(r.Unlock is AlwaysKnownCondition), Is.True, r.Id);
        }

        [Test]
        public void RequiredPartsExistAndBelongToTheRecipeStation()
        {
            foreach (var r in Catalog.All)
            foreach (var part in r.RequiredParts)
            {
                Assert.That(DefaultStationParts.TryGet(part, out var entry), Is.True, $"{r.Id}: {part}");
                Assert.That(entry.Definition.Station, Is.EqualTo(r.Station), $"{r.Id}: {part}");
            }
        }

        [Test]
        public void LegacyRecipesAreIntegratedWithTheirIdsAndOutputs()
        {
            foreach (var legacy in DefaultRecipes.Create())
            {
                Assert.That(Catalog.TryGet(legacy.Id, out var r), Is.True, legacy.Id);
                Assert.That(r.Profession, Is.EqualTo(legacy.Profession), legacy.Id);
                Assert.That(r.MainOutput.Item.Id, Is.EqualTo(legacy.Output.Id), legacy.Id);
            }
        }

        [Test]
        public void TagInputsKeepTheirMinimumTier()
        {
            var tagged = Catalog.Get("sastre.capa_planeo").Inputs.Single(i => i.IsByTag);
            Assert.That(tagged.Tag, Is.EqualTo(MaterialTags.Madera));
            Assert.That(tagged.Quantity, Is.EqualTo(2));
            Assert.That(tagged.MinTier, Is.EqualTo(2));
        }

        [TestCase(1, 1)]
        [TestCase(4, 1)]
        [TestCase(5, 2)]
        [TestCase(12, 3)]
        [TestCase(20, 4)]
        public void TierFollowsTheProfessionLevel(int level, int tier) =>
            Assert.That(RecipeTiers.Of(level), Is.EqualTo(tier));

        [Test]
        public void TierOutOfRangeIsRejected() =>
            Assert.Throws<ArgumentOutOfRangeException>(() => RecipeTiers.ProfessionLevel(5));
    }
}
