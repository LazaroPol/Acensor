using Specimen.Core.Crafting;
using Specimen.Core.Crafting.Definitions;
using Specimen.Core.Crafting.Stations;
using Specimen.Core.Professions;
using static Specimen.Tests.Core.Crafting.CraftingFixtures;

namespace Specimen.Tests.Core.Crafting.Stations
{
    internal static class StationFixtures
    {
        public static readonly StationPartDefinition Crisol =
            new StationPartDefinition("crisol", StationType.Fragua, maxDurability: 2, qualityBonus: 1);
        public static readonly StationPartDefinition Olla =
            new StationPartDefinition("olla_hierro", StationType.HogarCocina, qualityBonus: 2);

        public static StationUpgrade ForgeTo2(int ore = 5) => new StationUpgrade(StationType.Fragua, 2,
            new[] { IngredientSpec.ForItem(Ore, ore) }, queueSize: 4, qualityBonus: 2);

        public static StationUpgrade ForgeTo3() => new StationUpgrade(StationType.Fragua, 3,
            new[] { IngredientSpec.ForItem(Coal, 2) }, queueSize: 6, qualityBonus: 4);

        public static RecipeDefinition Recipe(string id, int stationLevel = 1, string part = null,
            int professionLevel = 1, int ore = 3, StationType station = StationType.Fragua) =>
            new RecipeDefinition(id, ProfessionId.Herrero, station,
                new[] { IngredientSpec.ForItem(Ore, ore) }, new[] { new RecipeOutput(Sword) },
                minStationLevel: stationLevel, requiredParts: part == null ? null : new[] { part },
                minProfessionLevel: professionLevel);
    }
}
