using Specimen.Core.Crafting;
using Specimen.Core.Elevator;
using Specimen.Core.Items;

namespace Specimen.Tests.Core.Elevator
{
    internal static class ElevatorFixtures
    {
        public static readonly ItemDefinition Wood =
            new ItemDefinition("madera", "Madera", ItemCategory.Material, Rarity.Common, 0, 10);
        public static readonly ItemDefinition Stone =
            new ItemDefinition("piedra", "Piedra", ItemCategory.Material, Rarity.Common, 0, 10);

        public static ModuleBlueprint BonfirePlan() => new ModuleBlueprint(ModuleType.Bonfire,
            new[] { new RecipeIngredient(Wood, 3), new RecipeIngredient(Stone, 2) });

        public static ModuleBlueprint ForgePlan() => new ModuleBlueprint(ModuleType.Forge,
            new[] { new RecipeIngredient(Stone, 4) });

        public static BlueprintCatalog Catalog() => new BlueprintCatalog(new[] { BonfirePlan(), ForgePlan() });

        public static ElevatorPlatform Platform(int slots = 3) => new ElevatorPlatform(slots, Catalog());

        public static Inventory Stocked(int wood, int stone)
        {
            var inv = new Inventory(4);
            if (wood > 0) inv.Add(ItemInstance.CreateNew(Wood), wood);
            if (stone > 0) inv.Add(ItemInstance.CreateNew(Stone), stone);
            return inv;
        }
    }
}
