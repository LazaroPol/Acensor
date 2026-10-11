using System;
using System.Collections.Generic;
using Specimen.Core.Common;

namespace Specimen.Core.Items
{
    /// <summary>
    /// Inventario por ranuras: cada ranura guarda una pila. Añadir llena primero las pilas existentes
    /// del mismo objeto y después abre ranuras nuevas; quitar vacía las pilas más recientes primero.
    /// </summary>
    public sealed partial class Inventory : IInventory
    {
        private readonly List<ItemStack> _stacks = new List<ItemStack>();

        public int Capacity { get; }
        public int UsedSlots => _stacks.Count;
        public int FreeSlots => Capacity - _stacks.Count;
        public IReadOnlyList<ItemStack> Stacks => _stacks;

        public event Action Changed;

        public Inventory(int capacity)
        {
            Capacity = Guard.Positive(capacity, nameof(capacity));
        }

        public int CountOf(string itemId)
        {
            int total = 0;
            foreach (var stack in _stacks)
                if (stack.Id == itemId) total += stack.Quantity;
            return total;
        }

        public int SpaceFor(ItemInstance item)
        {
            Guard.NotNull(item, nameof(item));
            int space = FreeSlots * item.Definition.MaxStack;
            foreach (var stack in _stacks)
                if (stack.CanMerge(item)) space += stack.Space;
            return space;
        }

        public bool RemoveInstance(ItemInstance item)
        {
            int index = _stacks.FindIndex(s => ReferenceEquals(s.Item, item));
            if (index < 0) return false;
            _stacks.RemoveAt(index);
            Changed?.Invoke();
            return true;
        }
    }
}
