using System;
using Specimen.Core.Common;

namespace Specimen.Core.Floors.Population
{
    /// <summary>Cuánto se puebla una sala de un anillo: tiradas de loot, enemigos y nivel de amenaza.</summary>
    public sealed class RingQuota
    {
        public RingQuota(int minLoot, int maxLoot, int minEnemies, int maxEnemies, int threat)
        {
            MinLoot = Guard.NonNegative(minLoot, nameof(minLoot));
            MaxLoot = CheckMax(maxLoot, minLoot, nameof(maxLoot));
            MinEnemies = Guard.NonNegative(minEnemies, nameof(minEnemies));
            MaxEnemies = CheckMax(maxEnemies, minEnemies, nameof(maxEnemies));
            Threat = Guard.Positive(threat, nameof(threat));
        }

        public int MinLoot { get; }
        public int MaxLoot { get; }
        public int MinEnemies { get; }
        public int MaxEnemies { get; }
        /// <summary>Nivel de amenaza de los enemigos del anillo (1 = el más flojo).</summary>
        public int Threat { get; }

        private static int CheckMax(int max, int min, string paramName) =>
            max >= min ? max : throw new ArgumentOutOfRangeException(paramName, max, "El máximo no puede ser menor que el mínimo.");
    }
}
