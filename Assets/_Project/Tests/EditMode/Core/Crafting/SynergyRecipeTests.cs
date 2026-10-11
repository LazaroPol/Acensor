using System;
using NUnit.Framework;
using Specimen.Core.Crafting;
using Specimen.Core.Professions;
using static Specimen.Tests.Core.Crafting.CraftingFixtures;
using static Specimen.Tests.Core.Crafting.SynergyFixtures;

namespace Specimen.Tests.Core.Crafting
{
    [TestFixture]
    public class SynergyRecipeTests
    {
        [Test]
        public void RejectsNullSynergy()
        {
            Assert.Throws<ArgumentNullException>(() =>
                new SynergyRecipe("x", null, new[] { new RecipeIngredient(Ore) }, Armor));
        }

        [Test]
        public void ValidatesFormulaLikeRegularRecipes()
        {
            Assert.Throws<ArgumentException>(() =>
                new SynergyRecipe("x", MixedArmor, new RecipeIngredient[0], Armor));
        }

        [Test]
        public void UnlocksOnlyWhenGroupHasBothProfessions()
        {
            var recipe = ArmorRecipe();
            Assert.That(recipe.IsUnlockedBy(SmithAndTailor), Is.True);
            Assert.That(recipe.IsUnlockedBy(new[] { ProfessionId.Herrero, ProfessionId.Herrero }), Is.False);
            Assert.That(recipe.IsUnlockedBy(new ProfessionId[0]), Is.False);
        }

        [Test]
        public void OnlyInvolvedProfessionsCanCraft()
        {
            var recipe = ArmorRecipe();
            Assert.That(recipe.CanBeCraftedBy(ProfessionId.Sastre), Is.True);
            Assert.That(recipe.CanBeCraftedBy(ProfessionId.Cocinero), Is.False);
        }

        [Test]
        public void BookRejectsDuplicatesAndFiltersByGroup()
        {
            Assert.Throws<ArgumentException>(() => new SynergyRecipeBook(new[] { ArmorRecipe(), ArmorRecipe() }));
            var book = SynergyBook();
            Assert.That(book.Count, Is.EqualTo(1));
            Assert.That(book.UnlockedFor(SmithAndTailor), Has.Count.EqualTo(1));
            Assert.That(book.UnlockedFor(new[] { ProfessionId.Sastre }), Is.Empty);
            Assert.That(book.TryGet(null, out _), Is.False);
        }
    }
}
