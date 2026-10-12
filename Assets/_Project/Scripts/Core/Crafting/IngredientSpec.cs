using System;
using Specimen.Core.Common;
using Specimen.Core.Items;

namespace Specimen.Core.Crafting
{
    /// <summary>
    /// Ingrediente de receta por objeto concreto (<see cref="ForItem"/>) o por etiqueta con tier
    /// mínimo (<see cref="ForTag"/>), p. ej. «3 de cualquier madera de tier ≥ 2».
    /// </summary>
    public sealed class IngredientSpec
    {
        /// <summary>Ítem exigido; null si el ingrediente es por etiqueta.</summary>
        public ItemDefinition Item { get; }
        public MaterialTags Tag { get; }
        public int Quantity { get; }
        public int MinTier { get; }

        public bool IsByTag => Item == null;

        private IngredientSpec(ItemDefinition item, MaterialTags tag, int quantity, int minTier)
        {
            Item = item;
            Tag = tag;
            Quantity = Guard.Positive(quantity, nameof(quantity));
            MinTier = minTier;
        }

        public static IngredientSpec ForItem(ItemDefinition item, int quantity = 1)
        {
            Guard.NotNull(item, nameof(item));
            if (item.IsDegradable)
                throw new ArgumentException("Un ingrediente no puede tener durabilidad.", nameof(item));
            return new IngredientSpec(item, MaterialTags.None, quantity, ItemDefinition.MinTier);
        }

        public static IngredientSpec ForTag(MaterialTags tag, int quantity = 1, int minTier = 0)
        {
            if (tag == MaterialTags.None)
                throw new ArgumentException("La etiqueta no puede ser None.", nameof(tag));
            if (minTier < ItemDefinition.MinTier || minTier > ItemDefinition.MaxTier)
                throw new ArgumentOutOfRangeException(nameof(minTier), minTier, "Tier mínimo fuera de rango.");
            return new IngredientSpec(null, tag, quantity, minTier);
        }

        /// <summary>Atajo de compatibilidad: un <see cref="RecipeIngredient"/> es un ingrediente por objeto.</summary>
        public static IngredientSpec From(RecipeIngredient ingredient)
        {
            Guard.NotNull(ingredient, nameof(ingredient));
            return ForItem(ingredient.Item, ingredient.Quantity);
        }

        /// <summary>True si una unidad de <paramref name="definition"/> sirve para este ingrediente.</summary>
        public bool Accepts(ItemDefinition definition)
        {
            if (definition == null || definition.IsDegradable) return false;
            if (!IsByTag) return definition.Id == Item.Id;
            return definition.HasTag(Tag) && definition.Tier >= MinTier;
        }

        public override string ToString() =>
            IsByTag ? $"{Tag} (tier ≥ {MinTier}) x{Quantity}" : $"{Item.Name} x{Quantity}";
    }
}
