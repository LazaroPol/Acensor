using System.Collections.Generic;
using Specimen.Core.Items;

namespace Specimen.Core.Crafting
{
    /// <summary>
    /// Consume ingredientes y añade el resultado de forma atómica: si el resultado no cabe
    /// (ni con el hueco que liberan los ingredientes) el inventario queda como estaba.
    /// </summary>
    internal static class CraftTransaction
    {
        public static CraftResult Apply(IReadOnlyList<RecipeIngredient> ingredients, ItemDefinition output,
            int quantity, IInventory inventory)
        {
            var entries = new List<PlannedConsumption>(ingredients.Count);
            foreach (var i in ingredients) entries.Add(new PlannedConsumption(i.Item, i.Quantity));
            return Apply(entries, output, quantity, inventory);
        }

        /// <summary>Gasta las unidades planificadas (si siguen ahí) y añade el resultado, o nada.</summary>
        public static CraftResult Apply(IReadOnlyList<PlannedConsumption> entries, ItemDefinition output,
            int quantity, IInventory inventory)
        {
            if (!StillAvailable(entries, inventory)) return CraftResult.MissingIngredients;
            foreach (var e in entries) inventory.Remove(e.Item.Id, e.Quantity);
            if (TryAddOutput(output, quantity, inventory)) return CraftResult.Crafted;
            foreach (var e in entries) inventory.Add(ItemInstance.CreateNew(e.Item), e.Quantity);
            return CraftResult.NoSpace;
        }

        private static bool StillAvailable(IReadOnlyList<PlannedConsumption> entries, IInventory inventory)
        {
            var needed = new Dictionary<string, int>();
            foreach (var e in entries)
                needed[e.Item.Id] = (needed.TryGetValue(e.Item.Id, out int n) ? n : 0) + e.Quantity;
            foreach (var pair in needed)
                if (inventory.CountOf(pair.Key) < pair.Value) return false;
            return true;
        }

        private static bool TryAddOutput(ItemDefinition output, int quantity, IInventory inventory)
        {
            int added = 0;
            for (int n = 0; n < quantity; n++) added += inventory.Add(ItemInstance.CreateNew(output));
            if (added == quantity) return true;
            if (added > 0) inventory.Remove(output.Id, added);
            return false;
        }
    }
}
