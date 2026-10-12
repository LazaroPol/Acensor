using System;
using System.Collections.Generic;
using System.Linq;
using Specimen.Core.Common;
using Specimen.Core.Crafting.Definitions;
using Specimen.Core.Items;
using Specimen.Core.Professions;

namespace Specimen.Core.Crafting.Stations
{
    /// <summary>
    /// Qué recetas ofrece una estación (GDD §17.3). Visibles: de su tipo, con nivel de estación
    /// suficiente, conocidas y con sus piezas colocadas (y sanas). De esas, las que fallan por nivel
    /// de oficio o materiales salen en gris con su <see cref="RecipeBlockReason"/>.
    /// </summary>
    public sealed class RecipeAvailability
    {
        private readonly RecipeCatalog _catalog;

        public RecipeAvailability(RecipeCatalog catalog)
        {
            _catalog = Guard.NotNull(catalog, nameof(catalog));
        }

        public IReadOnlyList<RecipeOption> For(Workstation station, IRecipeKnowledge knowledge,
            Func<ProfessionId, int> professionLevel, IInventory materials)
        {
            Guard.NotNull(station, nameof(station));
            Guard.NotNull(knowledge, nameof(knowledge));
            Guard.NotNull(professionLevel, nameof(professionLevel));
            Guard.NotNull(materials, nameof(materials));
            return _catalog.ForStation(station.Type)
                .Where(r => IsVisible(r, station, knowledge))
                .Select(r => new RecipeOption(r, BlockOf(r, professionLevel, materials)))
                .ToList().AsReadOnly();
        }

        public static bool IsVisible(RecipeDefinition recipe, Workstation station, IRecipeKnowledge knowledge) =>
            recipe.Station == station.Type
            && recipe.MinStationLevel <= station.Level
            && recipe.IsKnownBy(knowledge)
            && station.Parts.HasAll(recipe.RequiredParts);

        public static RecipeBlockReason BlockOf(RecipeDefinition recipe, Func<ProfessionId, int> professionLevel,
            IInventory materials)
        {
            if (professionLevel(recipe.Profession) < recipe.MinProfessionLevel) return RecipeBlockReason.ProfessionLevel;
            return TagConsumptionPlanner.Plan(recipe.Inputs, materials).IsComplete
                ? RecipeBlockReason.None
                : RecipeBlockReason.MissingMaterials;
        }
    }
}
