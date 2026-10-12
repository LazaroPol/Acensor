using System;
using NUnit.Framework;
using Specimen.Core.Crafting;
using Specimen.Core.Items;
using static Specimen.Tests.Core.Crafting.TagFixtures;

namespace Specimen.Tests.Core.Crafting
{
    [TestFixture]
    public class IngredientSpecTests
    {
        [Test]
        public void ForItemValidates()
        {
            Assert.Throws<ArgumentNullException>(() => IngredientSpec.ForItem(null));
            Assert.Throws<ArgumentOutOfRangeException>(() => IngredientSpec.ForItem(Iron, 0));
            Assert.Throws<ArgumentException>(() => IngredientSpec.ForItem(CraftingFixtures.Sword));
        }

        [Test]
        public void ForTagValidates()
        {
            Assert.Throws<ArgumentException>(() => IngredientSpec.ForTag(MaterialTags.None, 1));
            Assert.Throws<ArgumentOutOfRangeException>(() => IngredientSpec.ForTag(MaterialTags.Madera, 1, 5));
            Assert.Throws<ArgumentOutOfRangeException>(() => IngredientSpec.ForTag(MaterialTags.Madera, -1));
        }

        [Test]
        public void ForTagAcceptsOnlyTaggedItemsAtOrAboveMinTier()
        {
            var spec = IngredientSpec.ForTag(MaterialTags.Madera, 3, minTier: 2);
            Assert.That(spec.IsByTag, Is.True);
            Assert.That(spec.Accepts(Oak), Is.True);
            Assert.That(spec.Accepts(Pine), Is.False);
            Assert.That(spec.Accepts(Iron), Is.False);
        }

        [Test]
        public void ForItemAcceptsOnlyThatItem()
        {
            var spec = IngredientSpec.ForItem(Iron, 2);
            Assert.That(spec.IsByTag, Is.False);
            Assert.That(spec.Accepts(Iron), Is.True);
            Assert.That(spec.Accepts(Oak), Is.False);
        }

        [Test]
        public void LegacyIngredientConvertsToItemSpec()
        {
            var spec = IngredientSpec.From(new RecipeIngredient(Iron, 4));
            Assert.That(spec.Item, Is.SameAs(Iron));
            Assert.That(spec.Quantity, Is.EqualTo(4));
        }

        [Test]
        public void ChoiceValidates()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => new IngredientChoice(-1, "pino"));
            Assert.Throws<ArgumentException>(() => new IngredientChoice(0, " "));
        }
    }
}
