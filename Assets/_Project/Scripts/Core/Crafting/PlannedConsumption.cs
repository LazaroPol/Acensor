using Specimen.Core.Common;
using Specimen.Core.Items;

namespace Specimen.Core.Crafting
{
    /// <summary>Una línea del plan: cuántas unidades de un ítem se van a gastar.</summary>
    public sealed class PlannedConsumption
    {
        public ItemDefinition Item { get; }
        public int Quantity { get; }

        public PlannedConsumption(ItemDefinition item, int quantity)
        {
            Item = Guard.NotNull(item, nameof(item));
            Quantity = Guard.Positive(quantity, nameof(quantity));
        }

        public override string ToString() => $"{Item.Name} x{Quantity}";
    }
}
