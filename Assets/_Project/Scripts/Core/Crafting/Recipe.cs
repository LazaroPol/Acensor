using System;
using System.Collections.Generic;
using System.Linq;
using Specimen.Core.Common;
using Specimen.Core.Items;
using Specimen.Core.Professions;

namespace Specimen.Core.Crafting
{
    /// <summary>Receta inmutable de una profesión: ingredientes → resultado (GDD §7).</summary>
    public sealed class Recipe
    {
        public string Id { get; }
        public ProfessionId Profession { get; }
        public IReadOnlyList<RecipeIngredient> Ingredients { get; }
        public ItemDefinition Output { get; }
        public int OutputQuantity { get; }

        /// <summary>Calidad máxima que puede salir de esta receta (Legendary = sin tope propio).</summary>
        public Quality QualityCap { get; }

        public Recipe(string id, ProfessionId profession, IEnumerable<RecipeIngredient> ingredients,
            ItemDefinition output, int outputQuantity = 1, Quality qualityCap = Quality.Legendary)
        {
            if (string.IsNullOrWhiteSpace(id)) throw new ArgumentException("Id vacío.", nameof(id));
            Id = id;
            Profession = profession;
            Ingredients = Guard.NotNull(ingredients, nameof(ingredients)).ToList().AsReadOnly();
            Output = Guard.NotNull(output, nameof(output));
            OutputQuantity = Guard.Positive(outputQuantity, nameof(outputQuantity));
            if (qualityCap < Quality.Crude || qualityCap > Quality.Legendary)
                throw new ArgumentOutOfRangeException(nameof(qualityCap));
            QualityCap = qualityCap;
            if (Ingredients.Count == 0 || Ingredients.Any(i => i == null))
                throw new ArgumentException("Faltan ingredientes válidos.", nameof(ingredients));
            if (Ingredients.Select(i => i.ItemId).Distinct().Count() != Ingredients.Count)
                throw new ArgumentException("Ingrediente repetido.", nameof(ingredients));
        }

        /// <summary>Indica si el inventario tiene todos los ingredientes.</summary>
        public bool HasIngredients(IInventory inventory)
        {
            Guard.NotNull(inventory, nameof(inventory));
            return Ingredients.All(i => inventory.CountOf(i.ItemId) >= i.Quantity);
        }

        public override string ToString() => $"{Id} ({Profession}) → {Output.Name} x{OutputQuantity}";
    }
}
