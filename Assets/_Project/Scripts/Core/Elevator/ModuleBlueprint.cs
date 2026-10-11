using System;
using System.Collections.Generic;
using System.Linq;
using Specimen.Core.Common;
using Specimen.Core.Crafting;
using Specimen.Core.Items;

namespace Specimen.Core.Elevator
{
    /// <summary>
    /// Plano inmutable de un módulo: qué materiales cuesta construirlo y, opcionalmente,
    /// cuánto cuesta cada mejora de nivel (GDD §6: algunos módulos suben de nivel).
    /// </summary>
    public sealed class ModuleBlueprint
    {
        public ModuleType Type { get; }
        public IReadOnlyList<RecipeIngredient> Cost { get; }

        /// <summary>Coste de pasar al nivel i+2 (índice 0 = mejora de nivel 1 a 2).</summary>
        public IReadOnlyList<IReadOnlyList<RecipeIngredient>> UpgradeCosts { get; }

        public int MaxLevel => 1 + UpgradeCosts.Count;

        public ModuleBlueprint(ModuleType type, IEnumerable<RecipeIngredient> cost,
            IEnumerable<IEnumerable<RecipeIngredient>> upgradeCosts = null)
        {
            Type = type;
            Cost = Validated(cost, nameof(cost));
            UpgradeCosts = (upgradeCosts ?? Enumerable.Empty<IEnumerable<RecipeIngredient>>())
                .Select(c => Validated(c, nameof(upgradeCosts))).ToArray();
        }

        /// <summary>True si el inventario tiene todos los materiales del coste.</summary>
        public bool IsAffordable(IInventory inventory) => CanPay(Cost, inventory);

        /// <summary>Coste de mejorar desde <paramref name="currentLevel"/>; false si ya es el máximo.</summary>
        public bool TryGetUpgradeCost(int currentLevel, out IReadOnlyList<RecipeIngredient> cost)
        {
            var i = currentLevel - 1;
            cost = i >= 0 && i < UpgradeCosts.Count ? UpgradeCosts[i] : null;
            return cost != null;
        }

        internal static bool CanPay(IReadOnlyList<RecipeIngredient> cost, IInventory inventory)
        {
            Guard.NotNull(inventory, nameof(inventory));
            return cost.All(c => inventory.CountOf(c.ItemId) >= c.Quantity);
        }

        internal static void Pay(IReadOnlyList<RecipeIngredient> cost, IInventory inventory)
        {
            foreach (var c in cost) inventory.Remove(c.ItemId, c.Quantity);
        }

        private static IReadOnlyList<RecipeIngredient> Validated(IEnumerable<RecipeIngredient> cost, string name)
        {
            var list = Guard.NotNull(cost, name).ToArray();
            if (list.Length == 0) throw new ArgumentException("Un coste necesita materiales.", name);
            if (list.Any(c => c == null)) throw new ArgumentException("Material nulo.", name);
            if (list.Select(c => c.ItemId).Distinct().Count() != list.Length)
                throw new ArgumentException("Material repetido en el coste.", name);
            return list;
        }
    }
}
