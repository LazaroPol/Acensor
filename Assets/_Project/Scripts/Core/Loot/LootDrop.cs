using Specimen.Core.Common;
using Specimen.Core.Items;

namespace Specimen.Core.Loot
{
    /// <summary>Resultado de una tirada: un objeto y cuántas unidades han caído.</summary>
    public readonly struct LootDrop
    {
        public ItemDefinition Item { get; }
        public int Quantity { get; }

        public LootDrop(ItemDefinition item, int quantity)
        {
            Item = Guard.NotNull(item, nameof(item));
            Quantity = Guard.Positive(quantity, nameof(quantity));
        }

        /// <summary>Crea la pila lista para meter en un inventario (objetos nuevos, durabilidad máxima).</summary>
        public ItemStack ToStack() => new ItemStack(ItemInstance.CreateNew(Item), Quantity);

        public override string ToString() => $"{Item.Name} x{Quantity}";
    }
}
