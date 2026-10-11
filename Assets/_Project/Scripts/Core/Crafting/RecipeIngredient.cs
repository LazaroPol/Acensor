using System;
using Specimen.Core.Common;
using Specimen.Core.Items;

namespace Specimen.Core.Crafting
{
    /// <summary>
    /// Ingrediente de una receta: un material y cuántas unidades consume. Solo se admiten objetos
    /// sin desgaste, para que consumir y devolver unidades sea siempre equivalente.
    /// </summary>
    public sealed class RecipeIngredient
    {
        public ItemDefinition Item { get; }
        public int Quantity { get; }

        public string ItemId => Item.Id;

        public RecipeIngredient(ItemDefinition item, int quantity = 1)
        {
            Item = Guard.NotNull(item, nameof(item));
            Quantity = Guard.Positive(quantity, nameof(quantity));
            if (item.IsDegradable)
                throw new ArgumentException("Un ingrediente no puede tener durabilidad.", nameof(item));
        }

        public override string ToString() => $"{Item.Name} x{Quantity}";
    }
}
