using System;
using System.Collections.Generic;
using System.Linq;
using Specimen.Core.Common;
using Specimen.Core.Items;

namespace Specimen.Core.Crafting.Queue
{
    /// <summary>
    /// Vista de dos inventarios como uno (GDD §17.3: la estación tira de su entrada y, si falta, de la
    /// bolsa del jugador). Quita primero del principal; añade primero al principal.
    /// </summary>
    public sealed class InventoryChain : IInventory
    {
        private readonly IInventory _primary;
        private readonly IInventory _secondary;

        public InventoryChain(IInventory primary, IInventory secondary = null)
        {
            _primary = Guard.NotNull(primary, nameof(primary));
            _secondary = secondary;
        }

        public event Action Changed
        {
            add { foreach (var inv in Parts) inv.Changed += value; }
            remove { foreach (var inv in Parts) inv.Changed -= value; }
        }

        private IEnumerable<IInventory> Parts =>
            _secondary == null ? new[] { _primary } : new[] { _primary, _secondary };

        public int Capacity => Parts.Sum(i => i.Capacity);
        public int UsedSlots => Parts.Sum(i => i.UsedSlots);
        public int FreeSlots => Parts.Sum(i => i.FreeSlots);
        public IReadOnlyList<ItemStack> Stacks => Parts.SelectMany(i => i.Stacks).ToList();

        public int CountOf(string itemId) => Parts.Sum(i => i.CountOf(itemId));

        public int SpaceFor(ItemInstance item) => Parts.Sum(i => i.SpaceFor(item));

        public int Add(ItemInstance item, int quantity = 1)
        {
            int added = 0;
            foreach (var inv in Parts)
                if (added < quantity) added += inv.Add(item, quantity - added);
            return added;
        }

        public int Remove(string itemId, int quantity = 1)
        {
            int removed = 0;
            foreach (var inv in Parts)
                if (removed < quantity) removed += inv.Remove(itemId, quantity - removed);
            return removed;
        }

        public bool RemoveInstance(ItemInstance item) => Parts.Any(i => i.RemoveInstance(item));
    }
}
