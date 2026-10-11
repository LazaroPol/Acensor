using System;
using System.Collections.Generic;
using System.Linq;
using Specimen.Core.Common;
using Specimen.Core.Crafting;
using Specimen.Core.Items;

namespace Specimen.Core.Elevator
{
    /// <summary>Plano inmutable de un módulo: qué materiales cuesta construirlo.</summary>
    public sealed class ModuleBlueprint
    {
        public ModuleType Type { get; }
        public IReadOnlyList<RecipeIngredient> Cost { get; }

        public ModuleBlueprint(ModuleType type, IEnumerable<RecipeIngredient> cost)
        {
            Type = type;
            Cost = Guard.NotNull(cost, nameof(cost)).ToArray();
            if (Cost.Count == 0) throw new ArgumentException("Un plano necesita materiales.", nameof(cost));
            if (Cost.Any(c => c == null)) throw new ArgumentException("Material nulo.", nameof(cost));
            if (Cost.Select(c => c.ItemId).Distinct().Count() != Cost.Count)
                throw new ArgumentException("Material repetido en el plano.", nameof(cost));
        }

        /// <summary>True si el inventario tiene todos los materiales del coste.</summary>
        public bool IsAffordable(IInventory inventory)
        {
            Guard.NotNull(inventory, nameof(inventory));
            return Cost.All(c => inventory.CountOf(c.ItemId) >= c.Quantity);
        }
    }
}
