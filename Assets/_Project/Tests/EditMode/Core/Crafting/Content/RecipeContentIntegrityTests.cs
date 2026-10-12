using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using Specimen.Core.Crafting.Content;
using Specimen.Core.Crafting.Definitions;
using Specimen.Core.Crafting.Knowledge;
using Specimen.Core.Items.Catalog;

namespace Specimen.Tests.Core.Crafting.Content
{
    /// <summary>Integridad del recetario y los conocimientos de §17.6 (OF-12): nada huérfano ni inalcanzable.</summary>
    [TestFixture]
    public class RecipeContentIntegrityTests
    {
        private static readonly RecipeCatalog Catalog = DefaultRecipeCatalog.Create();

        private static int Tier(RecipeDefinition r) => RecipeTiers.Of(r.MinProfessionLevel);

        [Test]
        public void EveryInputAndOutputExistsInTheCatalog()
        {
            foreach (var r in Catalog.All)
            {
                foreach (var o in r.Outputs) Assert.That(ContentItems.TryGet(o.Item.Id, out _), Is.True, r.Id);
                foreach (var i in r.Inputs)
                    Assert.That(i.IsByTag ? ItemCatalog.All.Any(i.Accepts) : ContentItems.TryGet(i.Item.Id, out _),
                        Is.True, r.Id);
            }
        }

        [Test]
        public void CraftedInputsAreAvailableAtOrBelowTheConsumerTier()
        {
            var producedAt = new Dictionary<string, int>();
            foreach (var r in Catalog.All)
            foreach (var o in r.Outputs)
                producedAt[o.Item.Id] = producedAt.TryGetValue(o.Item.Id, out var t) ? System.Math.Min(t, Tier(r)) : Tier(r);
            foreach (var r in Catalog.All)
            foreach (var i in r.Inputs.Where(i => !i.IsByTag && producedAt.ContainsKey(i.Item.Id)))
                Assert.That(producedAt[i.Item.Id], Is.LessThanOrEqualTo(Tier(r)), $"{r.Id}: {i.Item.Id}");
        }

        [Test]
        public void WithoutKnowledgeOnlyRecipesWithoutKnowledgeUnlockAreKnown()
        {
            var known = new KnownRecipes();
            foreach (var r in Catalog.All) Assert.That(r.IsKnownBy(known), Is.EqualTo(r.Unlock is AlwaysKnownCondition), r.Id);
        }

        [Test]
        public void ReadingAProfessionKnowledgeUnlocksAllItsRecipes()
        {
            foreach (var group in Catalog.All.GroupBy(r => r.Profession))
            {
                var known = new KnownRecipes();
                foreach (var k in DefaultKnowledge.All.Where(k => k.Profession == group.Key)) known.Register(k);
                foreach (var r in group) Assert.That(r.IsKnownBy(known), Is.True, r.Id);
            }
        }

        [Test]
        public void KnowledgeTeachesExistingRecipesOfItsProfessionAndLevel()
        {
            foreach (var k in DefaultKnowledge.All)
            foreach (var id in k.Teaches)
            {
                Assert.That(Catalog.TryGet(id, out var r), Is.True, $"{k.Id}: {id}");
                Assert.That(r.Profession, Is.EqualTo(k.Profession), k.Id);
                Assert.That(k.MinLevel, Is.LessThanOrEqualTo(r.MinProfessionLevel), k.Id);
                Assert.That(r.Unlock is KnowledgeRequiredCondition c && c.KnowledgeId == k.Id, Is.True, k.Id);
            }
        }

        [Test]
        public void BatteryHasAtLeast25KnowledgeOfEveryTypeAndCompleteBooks()
        {
            var all = DefaultKnowledge.All;
            Assert.That(all.Count(k => !k.IsVolume), Is.GreaterThanOrEqualTo(25));
            Assert.That(all.Select(k => k.Id).Distinct().Count(), Is.EqualTo(all.Count));
            foreach (var type in new[] { KnowledgeType.Scroll, KnowledgeType.Blueprint, KnowledgeType.Treatise })
                Assert.That(all.Any(k => k.Type == type), Is.True, type.ToString());
            foreach (var book in all.Where(k => k.IsVolume).GroupBy(k => k.CollectionId))
                Assert.That(book.Select(k => k.Volume), Is.EquivalentTo(new[] { 1, 2, 3, 4, 5 }), book.Key);
            Assert.That(DefaultKnowledge.TryGet("pergamino.remaches", out _), Is.True);
            Assert.That(DefaultKnowledge.TryGet("pergamino.inexistente", out _), Is.False);
        }

        [Test]
        public void EveryUnusedKnowledgeIsAVolume()
        {
            var used = new HashSet<string>(Catalog.All.Select(r => r.Unlock).OfType<KnowledgeRequiredCondition>()
                .Select(c => c.KnowledgeId));
            foreach (var k in DefaultKnowledge.All.Where(k => !k.IsVolume))
                Assert.That(used, Does.Contain(k.Id), k.Id);
        }
    }
}
