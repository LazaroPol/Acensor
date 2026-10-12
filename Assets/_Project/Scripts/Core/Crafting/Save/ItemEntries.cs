using System.Collections.Generic;
using System.Linq;
using Specimen.Core.Items;

namespace Specimen.Core.Crafting.Save
{
    /// <summary>Instancia con calidad, durabilidad y mods: <c>item=ab12:espada_hierro:4:120:mod.a,mod.b</c>.</summary>
    public sealed class ItemEntry
    {
        public string InstanceId { get; }
        public string ItemId { get; }
        public Quality Quality { get; }
        public int Durability { get; }
        public IReadOnlyList<string> Mods { get; }

        public ItemEntry(string instanceId, string itemId, Quality quality, int durability, IEnumerable<string> mods = null)
        {
            InstanceId = instanceId;
            ItemId = itemId;
            Quality = quality;
            Durability = durability;
            Mods = (mods ?? Enumerable.Empty<string>()).ToArray();
        }
    }

    /// <summary>
    /// Ranura de inventario: instancia (<c>inv=bolsa:3:ab12</c>) o pila (<c>inv=bolsa:4:madera_pino:12</c>).
    /// </summary>
    public sealed class InventoryEntry
    {
        public string Inventory { get; }
        public int Slot { get; }
        /// <summary>Id de instancia si <see cref="IsInstance"/>; si no, id del ítem apilado.</summary>
        public string Ref { get; }
        public int Count { get; }
        public bool IsInstance { get; }

        private InventoryEntry(string inventory, int slot, string reference, int count, bool isInstance)
        {
            Inventory = inventory;
            Slot = slot;
            Ref = reference;
            Count = count;
            IsInstance = isInstance;
        }

        public static InventoryEntry Instance(string inventory, int slot, string instanceId)
            => new InventoryEntry(inventory, slot, instanceId, 1, true);

        public static InventoryEntry Stack(string inventory, int slot, string itemId, int count)
            => new InventoryEntry(inventory, slot, itemId, count, false);
    }
}
