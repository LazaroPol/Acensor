using Specimen.Core.Crafting;
using Specimen.Core.Items;
using Specimen.Core.Professions;
using static Specimen.Tests.Core.Crafting.CraftingFixtures;

namespace Specimen.Tests.Core.Crafting
{
    internal static class SynergyFixtures
    {
        public static readonly ProfessionSynergy MixedArmor =
            new ProfessionSynergy(ProfessionId.Herrero, ProfessionId.Sastre, "Armadura mixta");
        public static readonly ItemDefinition Armor =
            new ItemDefinition("armadura-mixta", "Armadura mixta", ItemCategory.Armor, Rarity.Rare, 40);

        public static readonly ProfessionId[] SmithAndTailor = { ProfessionId.Herrero, ProfessionId.Sastre };

        public static SynergyRecipe ArmorRecipe() => new SynergyRecipe("armadura-mixta", MixedArmor,
            new[] { new RecipeIngredient(Ore, 2), new RecipeIngredient(Coal, 2) }, Armor);

        public static SynergyRecipeBook SynergyBook() => new SynergyRecipeBook(new[] { ArmorRecipe() });
    }
}
