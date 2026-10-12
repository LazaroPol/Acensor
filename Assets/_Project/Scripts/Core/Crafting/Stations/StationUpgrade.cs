using System;
using System.Collections.Generic;
using System.Linq;
using Specimen.Core.Common;
using Specimen.Core.Crafting.Definitions;

namespace Specimen.Core.Crafting.Stations
{
    /// <summary>
    /// Mejora de estación por datos (GDD §17.3): sube una estación al <see cref="TargetLevel"/> (2-3)
    /// gastando <see cref="Cost"/>; fija el tamaño de cola y los puntos de calidad del nuevo nivel.
    /// </summary>
    public sealed class StationUpgrade
    {
        public StationType Station { get; }
        public int TargetLevel { get; }
        public IReadOnlyList<IngredientSpec> Cost { get; }
        public int QueueSize { get; }
        public int QualityBonus { get; }

        public StationUpgrade(StationType station, int targetLevel, IEnumerable<IngredientSpec> cost,
            int queueSize, int qualityBonus = 0)
        {
            if (!StationTypes.IsDefined(station)) throw new ArgumentOutOfRangeException(nameof(station));
            if (targetLevel <= RecipeDefinition.MinStationLevelValue || targetLevel > RecipeDefinition.MaxStationLevelValue)
                throw new ArgumentOutOfRangeException(nameof(targetLevel), targetLevel, "Nivel objetivo 2-3.");
            Station = station;
            TargetLevel = targetLevel;
            Cost = Guard.NotNull(cost, nameof(cost)).ToList().AsReadOnly();
            if (Cost.Count == 0 || Cost.Any(c => c == null))
                throw new ArgumentException("El coste necesita ingredientes no nulos.", nameof(cost));
            QueueSize = Guard.Positive(queueSize, nameof(queueSize));
            QualityBonus = Guard.NonNegative(qualityBonus, nameof(qualityBonus));
        }

        public override string ToString() => $"{Station} → nivel {TargetLevel}";
    }
}
