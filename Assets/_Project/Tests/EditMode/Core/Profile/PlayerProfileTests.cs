using System.Linq;
using NUnit.Framework;
using Specimen.Core.Classes;
using Specimen.Core.Composition;
using Specimen.Core.Dna;
using Specimen.Core.Profile;

namespace Specimen.Tests.Core.Profile
{
    public class PlayerProfileTests
    {
        private sealed class NoLoader : ISceneLoader { public void Load(SceneId scene) { } }

        [Test]
        public void Reward_PersistsAcrossReload()
        {
            var store = new InMemoryProfileStore();
            PlayerProfile.Load(store).Reward(7);
            Assert.That(PlayerProfile.Load(store).Dna.FreePoints, Is.EqualTo(7));
        }

        [Test]
        public void Unlock_SavesOnlyWhenItSucceeds()
        {
            var store = new InMemoryProfileStore();
            var profile = PlayerProfile.Load(store);
            var cheapest = profile.Tree.Nodes.OrderBy(n => n.Tier).ThenBy(n => n.Cost).First();
            Assert.That(profile.Unlock(cheapest.Id), Is.EqualTo(UnlockResult.NotEnoughPoints));
            Assert.That(store.Text, Is.Null);
            profile.Reward(cheapest.Cost);
            Assert.That(profile.Unlock(cheapest.Id), Is.EqualTo(UnlockResult.Ok));
            Assert.That(PlayerProfile.Load(store).Dna.IsUnlocked(cheapest.Id), Is.True);
        }

        [Test]
        public void ApplyTo_AddsBoughtMutationsToAFreshKitSheet()
        {
            var profile = PlayerProfile.Load(new InMemoryProfileStore());
            var cheapest = profile.Tree.Nodes.OrderBy(n => n.Tier).ThenBy(n => n.Cost).First();
            profile.Reward(cheapest.Cost);
            profile.Unlock(cheapest.Id);
            var sheet = ClassKitCatalog.CreateDefault().All[0].CreateStatSheet();
            Assert.That(profile.ApplyTo(sheet), Is.EqualTo(1));
        }

        [Test]
        public void Composition_SharesTheStoreAcrossNewGames()
        {
            var store = new InMemoryProfileStore();
            GameComposition.Build(new NoLoader(), store).Resolve<PlayerProfile>().Reward(5);
            var again = GameComposition.Build(new NoLoader(), store).Resolve<PlayerProfile>();
            Assert.That(again.Dna.FreePoints, Is.EqualTo(5));
        }

        [Test]
        public void Composition_WithoutStore_StartsEmpty()
        {
            var profile = GameComposition.Build(new NoLoader()).Resolve<PlayerProfile>();
            Assert.That(profile.Dna.FreePoints, Is.EqualTo(0));
        }
    }
}
