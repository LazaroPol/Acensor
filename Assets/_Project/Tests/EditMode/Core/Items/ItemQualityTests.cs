using NUnit.Framework;
using Specimen.Core.Crafting;
using Specimen.Core.Items;
using Specimen.Core.Professions;

namespace Specimen.Tests.Core.Items
{
    [TestFixture]
    public class ItemQualityTests
    {
        private static readonly ItemDefinition Sword =
            new ItemDefinition("espada", "Espada", ItemCategory.Weapon, Rarity.Common, 100);
        private static readonly ItemDefinition Ore =
            new ItemDefinition("mena", "Mena", ItemCategory.Material, Rarity.Common, 0, 10);

        [Test]
        public void PlainCreateNewHasNoQuality()
        {
            var item = ItemInstance.CreateNew(Sword);
            Assert.That(item.Quality, Is.EqualTo(Quality.None));
            Assert.That(item.Durability.Max, Is.EqualTo(Sword.MaxDurability));
        }

        [TestCase(Quality.Crude, 0.8f)]
        [TestCase(Quality.Common, 1.0f)]
        [TestCase(Quality.Legendary, 1.8f)]
        public void CreateNewWithGradeScalesDurability(Quality quality, float mult)
        {
            var item = ItemInstance.CreateNew(Sword, QualityRules.Default.Grade(quality));
            Assert.That(item.Quality, Is.EqualTo(quality));
            Assert.That(item.Durability.Max, Is.EqualTo((int)System.Math.Round(Sword.MaxDurability * mult)));
            Assert.That(item.Durability.Current, Is.EqualTo(item.Durability.Max));
        }

        [Test]
        public void RecipeCapDefaultsToLegendary()
        {
            var recipe = new Recipe("r", ProfessionId.Herrero, new[] { new RecipeIngredient(Ore, 1) }, Sword);
            Assert.That(recipe.QualityCap, Is.EqualTo(Quality.Legendary));
            var capped = new Recipe("r", ProfessionId.Herrero, new[] { new RecipeIngredient(Ore, 1) },
                Sword, 1, Quality.Good);
            Assert.That(capped.QualityCap, Is.EqualTo(Quality.Good));
        }
    }
}
