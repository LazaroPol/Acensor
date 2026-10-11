using System;
using Specimen.Core.Common;

namespace Specimen.Core.Items
{
    /// <summary>
    /// Contenido de una ranura: un objeto y cuántas unidades hay. Los objetos apilables son idénticos
    /// (sin desgaste), así que basta un ejemplar representativo; los degradables ocupan siempre 1.
    /// </summary>
    public sealed class ItemStack
    {
        public ItemInstance Item { get; }
        public int Quantity { get; private set; }

        public ItemDefinition Definition => Item.Definition;
        public string Id => Item.Id;
        public int MaxStack => Definition.MaxStack;
        public int Space => MaxStack - Quantity;
        public bool IsFull => Space == 0;

        public ItemStack(ItemInstance item, int quantity)
        {
            Item = Guard.NotNull(item, nameof(item));
            Guard.Positive(quantity, nameof(quantity));
            if (quantity > item.Definition.MaxStack)
                throw new ArgumentOutOfRangeException(nameof(quantity), "Supera el máximo de la pila.");
            Quantity = quantity;
        }

        /// <summary>Indica si otra unidad del objeto puede sumarse a esta pila.</summary>
        public bool CanMerge(ItemInstance other) =>
            other != null && Definition.IsStackable && other.Id == Id && !IsFull;

        /// <summary>Añade hasta llenar la pila; devuelve las unidades realmente añadidas.</summary>
        internal int Add(int amount)
        {
            int added = Math.Min(Guard.NonNegative(amount, nameof(amount)), Space);
            Quantity += added;
            return added;
        }

        /// <summary>Quita unidades; devuelve las realmente quitadas.</summary>
        internal int Remove(int amount)
        {
            int removed = Math.Min(Guard.NonNegative(amount, nameof(amount)), Quantity);
            Quantity -= removed;
            return removed;
        }

        public override string ToString() => $"{Item.Definition.Name} x{Quantity}";
    }
}
