using System;
using NUnit.Framework;
using Specimen.Core.Crafting;
using Specimen.Core.Professions;
using static Specimen.Tests.Core.Crafting.CraftingFixtures;

namespace Specimen.Tests.Core.Crafting
{
    [TestFixture]
    public class RecipeTests
    {
        [Test]
        public void IngredientRejectsDegradableItemsAndBadQuantity()
        {
            Assert.Throws<ArgumentException>(() => new RecipeIngredient(Sword));
            Assert.Throws<ArgumentOutOfRangeException>(() => new RecipeIngredient(Ore, 0));
        }

        [Test]
        public void RecipeRequiresIngredientsWithoutDuplicates()
        {
            Assert.Throws<ArgumentException>(() =>
                new Recipe("x", ProfessionId.Herrero, new RecipeIngredient[0], Sword));
            Assert.Throws<ArgumentException>(() => new Recipe("x", ProfessionId.Herrero,
                new[] { new RecipeIngredient(Ore), new RecipeIngredient(Ore, 2) }, Sword));
            Assert.Throws<ArgumentException>(() => new Recipe(" ", ProfessionId.Herrero,
                new[] { new RecipeIngredient(Ore) }, Sword));
        }

        [Test]
        public void HasIngredientsChecksEveryQuantity()
        {
            var recipe = SwordRecipe();
            Assert.That(recipe.HasIngredients(Stocked(4, 3, 1)), Is.True);
            Assert.That(recipe.HasIngredients(Stocked(4, 2, 1)), Is.False);
            Assert.That(recipe.HasIngredients(Stocked(4, 3, 0)), Is.False);
        }

        [Test]
        public void BookFiltersByProfessionAndFindsById()
        {
            var book = Book();
            Assert.That(book.Count, Is.EqualTo(2));
            Assert.That(book.For(ProfessionId.Herrero), Has.Count.EqualTo(1));
            Assert.That(book.For(ProfessionId.Cocinero), Is.Empty);
            Assert.That(book.TryGet("vendas", out var r) && r.Profession == ProfessionId.Sastre, Is.True);
            Assert.That(book.TryGet("nada", out _), Is.False);
            Assert.That(book.TryGet(null, out _), Is.False);
        }

        [Test]
        public void BookRejectsDuplicateIds()
        {
            Assert.Throws<ArgumentException>(() => new RecipeBook(new[] { SwordRecipe(), SwordRecipe() }));
        }
    }
}
