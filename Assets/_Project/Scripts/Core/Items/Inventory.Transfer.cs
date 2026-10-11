using System;
using Specimen.Core.Common;

namespace Specimen.Core.Items
{
    public sealed partial class Inventory
    {
        public int Add(ItemInstance item, int quantity = 1)
        {
            Guard.NotNull(item, nameof(item));
            Guard.Positive(quantity, nameof(quantity));
            if (item.Definition.IsDegradable) EnsureUniqueInstance(item, quantity);
            int remaining = quantity;
            foreach (var stack in _stacks)
            {
                if (remaining == 0) break;
                if (stack.CanMerge(item)) remaining -= stack.Add(remaining);
            }
            while (remaining > 0 && FreeSlots > 0)
            {
                int amount = remaining < item.Definition.MaxStack ? remaining : item.Definition.MaxStack;
                _stacks.Add(new ItemStack(item, amount));
                remaining -= amount;
            }
            return Notify(quantity - remaining);
        }

        public int Remove(string itemId, int quantity = 1)
        {
            Guard.Positive(quantity, nameof(quantity));
            int remaining = quantity;
            for (int i = _stacks.Count - 1; i >= 0 && remaining > 0; i--)
            {
                var stack = _stacks[i];
                if (stack.Id != itemId) continue;
                remaining -= stack.Remove(remaining);
                if (stack.Quantity == 0) _stacks.RemoveAt(i);
            }
            return Notify(quantity - remaining);
        }

        /// <summary>Cada objeto degradable guarda su desgaste: no puede duplicarse.</summary>
        private void EnsureUniqueInstance(ItemInstance item, int quantity)
        {
            if (quantity != 1)
                throw new ArgumentOutOfRangeException(nameof(quantity), "Un objeto con desgaste es único.");
            if (_stacks.Exists(s => ReferenceEquals(s.Item, item)))
                throw new InvalidOperationException("Ese objeto ya está en el inventario.");
        }

        private int Notify(int moved)
        {
            if (moved > 0) Changed?.Invoke();
            return moved;
        }
    }
}
