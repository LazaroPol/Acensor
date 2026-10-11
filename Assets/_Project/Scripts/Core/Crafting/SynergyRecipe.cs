using System.Collections.Generic;
using System.Linq;
using Specimen.Core.Common;
using Specimen.Core.Items;
using Specimen.Core.Professions;

namespace Specimen.Core.Crafting
{
    /// <summary>
    /// Receta de sinergia (GDD §7): solo se desbloquea si el grupo tiene las dos profesiones
    /// de la sinergia, y solo puede fabricarla un jugador con una de ellas.
    /// </summary>
    public sealed class SynergyRecipe
    {
        private readonly Recipe _formula;

        public SynergyRecipe(string id, ProfessionSynergy synergy, IEnumerable<RecipeIngredient> ingredients,
            ItemDefinition output, int outputQuantity = 1)
        {
            Synergy = Guard.NotNull(synergy, nameof(synergy));
            _formula = new Recipe(id, synergy.First, ingredients, output, outputQuantity);
        }

        public string Id => _formula.Id;
        public ProfessionSynergy Synergy { get; }
        public IReadOnlyList<RecipeIngredient> Ingredients => _formula.Ingredients;
        public ItemDefinition Output => _formula.Output;
        public int OutputQuantity => _formula.OutputQuantity;

        /// <summary>Indica si el grupo reúne las dos profesiones de la sinergia.</summary>
        public bool IsUnlockedBy(IEnumerable<ProfessionId> group)
        {
            var present = Guard.NotNull(group, nameof(group)).ToList();
            return present.Contains(Synergy.First) && present.Contains(Synergy.Second);
        }

        public bool CanBeCraftedBy(ProfessionId crafter) => Synergy.Involves(crafter);

        public bool HasIngredients(IInventory inventory) => _formula.HasIngredients(inventory);

        public override string ToString() =>
            $"{Id} ({Synergy.First}+{Synergy.Second}) → {Output.Name} x{OutputQuantity}";
    }
}
