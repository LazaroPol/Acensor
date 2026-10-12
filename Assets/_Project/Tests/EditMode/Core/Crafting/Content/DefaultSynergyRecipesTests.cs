using System.Linq;
using NUnit.Framework;
using Specimen.Core.Crafting.Content;
using Specimen.Core.Professions;

namespace Specimen.Tests.Core.Crafting.Content
{
    /// <summary>Las sinergias de §7 siguen disponibles con el recetario nuevo (OF-12).</summary>
    [TestFixture]
    public class DefaultSynergyRecipesTests
    {
        [Test]
        public void EverySynergyHasARecipeWithCatalogItems()
        {
            var recipes = DefaultSynergyRecipes.Create();
            foreach (var synergy in DefaultProfessionSynergies.Create())
            {
                var recipe = recipes.SingleOrDefault(r => r.Synergy.Recipe == synergy.Recipe);
                Assert.That(recipe, Is.Not.Null, synergy.Recipe);
                Assert.That(recipe.Id, Does.StartWith("sinergia."), synergy.Recipe);
                Assert.That(ContentItems.TryGet(recipe.Output.Id, out _), Is.True, recipe.Id);
                Assert.That(recipe.Ingredients.All(i => ContentItems.TryGet(i.ItemId, out _)), Is.True, recipe.Id);
            }
        }

        [Test]
        public void SynergyRecipeNeedsBothProfessions()
        {
            var golem = DefaultSynergyRecipes.CreateBook().TryGet("sinergia.golem_piedra", out var r) ? r : null;
            Assert.That(golem, Is.Not.Null);
            Assert.That(golem.IsUnlockedBy(new[] { ProfessionId.Escultor }), Is.False);
            Assert.That(golem.IsUnlockedBy(new[] { ProfessionId.Escultor, ProfessionId.Ingeniero }), Is.True);
        }

        [Test]
        public void ContentItemsResolveStationPartsAndRejectUnknownIds()
        {
            Assert.That(ContentItems.Get("pieza.parrilla").Name, Is.EqualTo("Parrilla"));
            Assert.That(ContentItems.TryGet("nada", out _), Is.False);
            Assert.Throws<System.ArgumentException>(() => ContentItems.Get("nada"));
        }
    }
}
