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
            foreach (var i in ingredients) inventory.Remove(i.ItemId, i.Quantity);
            if (TryAddOutput(output, quantity, inventory)) return CraftResult.Crafted;
            foreach (var i in ingredients) inventory.Add(ItemInstance.CreateNew(i.Item), i.Quantity);
            return CraftResult.NoSpace;
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
