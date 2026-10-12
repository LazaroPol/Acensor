using Specimen.Core.Crafting;
using Specimen.Core.Crafting.Definitions;
using Specimen.Core.Items;
using Specimen.Core.Items.Catalog;
using Specimen.Core.Professions;
using static Specimen.Tests.Core.Crafting.CraftingFixtures;

namespace Specimen.Tests.Core.Crafting.Queue
{
    internal static class QueueFixtures
    {
        public static readonly ItemDefinition Iron = new ItemDefinition("hierro", "Hierro",
            ItemCategory.Material, Rarity.Common, 0, 10, tier: 1, tags: MaterialTags.Metal);
        public static readonly ItemDefinition Steel = new ItemDefinition("acero", "Acero",
            ItemCategory.Material, Rarity.Common, 0, 10, tier: 3, tags: MaterialTags.Metal);
        public static readonly ItemDefinition Wood = new ItemDefinition(ItemIds.Madera, "Madera",
            ItemCategory.Material, Rarity.Common, 0, 10, tags: MaterialTags.Combustible);

        public static RecipeDefinition Forge(float seconds = 10f, int ore = 3, int xp = 20,
            StationType station = StationType.Fragua, string part = null) =>
            new RecipeDefinition("forja", ProfessionId.Herrero, station,
                new[] { IngredientSpec.ForItem(Ore, ore) }, new[] { new RecipeOutput(Sword) },
                requiredParts: part == null ? null : new[] { part }, craftSeconds: seconds, xp: xp);

        public static RecipeDefinition ByTag(float seconds = 10f) =>
            new RecipeDefinition("forja_metal", ProfessionId.Herrero, StationType.Fragua,
                new[] { IngredientSpec.ForTag(MaterialTags.Metal, 2, minTier: 1) },
                new[] { new RecipeOutput(Sword) }, craftSeconds: seconds, xp: 20);

        public static void Put(IInventory inventory, ItemDefinition item, int quantity) =>
            inventory.Add(ItemInstance.CreateNew(item), quantity);
    }

    /// <summary>Tirada fija que recuerda con qué datos se le llamó.</summary>
    internal sealed class FixedRoller : IQualityRoller
    {
        public Quality Result = Quality.Good;
        public int Calls;
        public QualityRollInput Last;

        public Quality Roll(QualityRollInput input)
        {
            Calls++;
            Last = input;
            return Result;
        }
    }
}
