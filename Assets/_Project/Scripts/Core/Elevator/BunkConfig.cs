using System;
using System.Collections.Generic;
using System.Linq;
using Specimen.Core.Common;
using Specimen.Core.Crafting;

namespace Specimen.Core.Elevator
{
    /// <summary>
    /// Parámetros de la Litera (GDD §3.3, §6): materiales que cuesta revivir a un Espectro y cuántas
    /// unidades de cada material se rebajan por nivel por encima del primero (nunca por debajo de 1).
    /// </summary>
    public sealed class BunkConfig
    {
        public IReadOnlyList<RecipeIngredient> ReviveCost { get; }
        public int ReductionPerLevel { get; }

        public BunkConfig(IEnumerable<RecipeIngredient> reviveCost, int reductionPerLevel = 0)
        {
            var cost = Guard.NotNull(reviveCost, nameof(reviveCost)).ToArray();
            if (cost.Length == 0) throw new ArgumentException("Revivir necesita materiales.", nameof(reviveCost));
            if (cost.Any(c => c == null)) throw new ArgumentException("Material nulo.", nameof(reviveCost));
            if (cost.Select(c => c.ItemId).Distinct().Count() != cost.Length)
                throw new ArgumentException("Material repetido en el coste.", nameof(reviveCost));
            ReviveCost = cost;
            ReductionPerLevel = Guard.NonNegative(reductionPerLevel, nameof(reductionPerLevel));
        }

        /// <summary>Coste de revivir con la Litera a <paramref name="level"/> (nivel ≥ 1).</summary>
        public IReadOnlyList<RecipeIngredient> CostAt(int level)
        {
            Guard.Positive(level, nameof(level));
            int cut = ReductionPerLevel * (level - 1);
            return ReviveCost.Select(c => new RecipeIngredient(c.Item, Math.Max(1, c.Quantity - cut))).ToArray();
        }
    }
}
