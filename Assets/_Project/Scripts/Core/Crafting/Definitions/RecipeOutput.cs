using Specimen.Core.Common;
using Specimen.Core.Items;

namespace Specimen.Core.Crafting.Definitions
{
    /// <summary>Salida de receta: ítem + cantidad. La primera salida es la principal (recibe la calidad).</summary>
    public sealed class RecipeOutput
    {
        public ItemDefinition Item { get; }
        public int Quantity { get; }

        public RecipeOutput(ItemDefinition item, int quantity = 1)
        {
            Item = Guard.NotNull(item, nameof(item));
            Quantity = Guard.Positive(quantity, nameof(quantity));
        }

        public override string ToString() => $"{Item.Name} x{Quantity}";
    }
}
