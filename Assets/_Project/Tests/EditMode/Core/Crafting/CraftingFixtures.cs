using Specimen.Core.Crafting;
using Specimen.Core.Items;
using Specimen.Core.Professions;

namespace Specimen.Tests.Core.Crafting
{
    internal static class CraftingFixtures
    {
        public static readonly ItemDefinition Ore =
            new ItemDefinition("mena", "Mena", ItemCategory.Material, Rarity.Common, 0, 10);
        public static readonly ItemDefinition Coal =
            new ItemDefinition("carbon", "Carbón", ItemCategory.Material, Rarity.Common, 0, 10);
        public static readonly ItemDefinition Sword =
            new ItemDefinition("espada", "Espada", ItemCategory.Weapon, Rarity.Common, 30);
        public static readonly ItemDefinition Bandage =
            new ItemDefinition("venda", "Venda", ItemCategory.Consumable, Rarity.Common, 0, 5);

        public static Recipe SwordRecipe() => new Recipe("forjar-espada", ProfessionId.Herrero,
            new[] { new RecipeIngredient(Ore, 3), new RecipeIngredient(Coal, 1) }, Sword);

        public static Recipe BandageRecipe() => new Recipe("vendas", ProfessionId.Sastre,
            new[] { new RecipeIngredient(Coal, 2) }, Bandage, 2);

        public static RecipeBook Book() => new RecipeBook(new[] { SwordRecipe(), BandageRecipe() });

        public static Inventory Stocked(int capacity, int ore, int coal)
        {
            var inv = new Inventory(capacity);
            if (ore > 0) inv.Add(ItemInstance.CreateNew(Ore), ore);
            if (coal > 0) inv.Add(ItemInstance.CreateNew(Coal), coal);
            return inv;
        }
    }
}
