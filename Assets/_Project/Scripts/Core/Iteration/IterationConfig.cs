using System;
using System.Collections.Generic;
using System.Linq;
using Specimen.Core.Common;

namespace Specimen.Core.Iteration
{
    /// <summary>
    /// Parámetros inmutables de una iteración (una partida): pisos, duración de cada fase y avisos.
    /// Se construye con <see cref="IterationConfigBuilder"/>; aquí solo se valida la coherencia.
    /// </summary>
    public sealed class IterationConfig
    {
        public const int MaxPlayersLimit = 6;

        public int FloorCount { get; }
        public float CalmSeconds { get; }
        public float IncursionSeconds { get; }
        public int SiegeWaves { get; }
        public int MaxPlayers { get; }

        /// <summary>Segundos que quedan de Incursión tras aprobarse el voto de cierre anticipado.</summary>
        public float EarlyCloseGraceSeconds { get; }

        /// <summary>Segundos restantes de Incursión en los que se avisa del cierre, de mayor a menor.</summary>
        public IReadOnlyList<float> DoorWarnings { get; }

        public IterationConfig(int floorCount, float calmSeconds, float incursionSeconds,
                               int siegeWaves, int maxPlayers, IEnumerable<float> doorWarnings,
                               float earlyCloseGraceSeconds = 15f)
        {
            FloorCount = Guard.Positive(floorCount, nameof(floorCount));
            CalmSeconds = Guard.Positive(calmSeconds, nameof(calmSeconds));
            IncursionSeconds = Guard.Positive(incursionSeconds, nameof(incursionSeconds));
            SiegeWaves = Guard.Positive(siegeWaves, nameof(siegeWaves));
            MaxPlayers = Guard.Positive(maxPlayers, nameof(maxPlayers));
            if (maxPlayers > MaxPlayersLimit)
                throw new ArgumentOutOfRangeException(nameof(maxPlayers), maxPlayers, $"Máximo {MaxPlayersLimit}.");
            EarlyCloseGraceSeconds = Guard.Positive(earlyCloseGraceSeconds, nameof(earlyCloseGraceSeconds));
            if (earlyCloseGraceSeconds >= incursionSeconds)
                throw new ArgumentOutOfRangeException(nameof(earlyCloseGraceSeconds), "Debe ser menor que la Incursión.");
            DoorWarnings = NormalizeWarnings(Guard.NotNull(doorWarnings, nameof(doorWarnings)), incursionSeconds);
        }

        /// <summary>Duración nominal sin contar el asedio: (Calma + Incursión) × pisos.</summary>
        public float FloorsDurationSeconds => (CalmSeconds + IncursionSeconds) * FloorCount;

        private static IReadOnlyList<float> NormalizeWarnings(IEnumerable<float> warnings, float incursion)
        {
            var list = warnings.Distinct().OrderByDescending(w => w).ToList();
            foreach (var w in list)
            {
                if (w <= 0f || w >= incursion)
                    throw new ArgumentOutOfRangeException(nameof(warnings), w, "Cada aviso debe estar en (0, Incursión).");
            }
            return list.AsReadOnly();
        }
    }
}
