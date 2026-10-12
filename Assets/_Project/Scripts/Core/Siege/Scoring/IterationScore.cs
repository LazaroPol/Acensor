using Specimen.Core.Common;

namespace Specimen.Core.Siege.Scoring
{
    /// <summary>
    /// Puntuación de la iteración (GDD §3.5): bajas, calidad del loot extraído y pisos completados, más un
    /// multiplicador de supervivencia colectiva que crece con los jugadores vivos al final de cada piso.
    /// </summary>
    public sealed class IterationScore
    {
        private readonly ScoreConfig _config;

        public IterationScore(ScoreConfig config) => _config = Guard.NotNull(config, nameof(config));

        public int Kills { get; private set; }
        public int FloorsCompleted { get; private set; }
        public float LootQuality { get; private set; }
        public int SurvivorFloors { get; private set; }

        public float BasePoints => Kills * _config.KillValue + LootQuality + FloorsCompleted * _config.FloorBonus;
        public float Multiplier => 1f + _config.SurvivalStep * SurvivorFloors;

        public void AddKills(int count) => Kills += Guard.NonNegative(count, nameof(count));

        public void AddLoot(float quality) => LootQuality += Guard.NonNegative(quality, nameof(quality));

        /// <summary>Cierra un piso: suma su bonus y los jugadores vivos al acumulado del multiplicador.</summary>
        public void CompleteFloor(int playersAlive)
        {
            SurvivorFloors += Guard.NonNegative(playersAlive, nameof(playersAlive));
            FloorsCompleted++;
        }

        /// <summary>Puntuación final; si el equipo murió, los puntos se conservan pero sin multiplicador.</summary>
        public ScoreResult Settle(bool survived) => new ScoreResult(BasePoints, survived ? Multiplier : 1f);
    }
}
