using Specimen.Core.Common;

namespace Specimen.Core.Siege.Scoring
{
    /// <summary>Parámetros de puntuación (GDD §3.5): valor por baja, bonus por piso y paso del multiplicador.</summary>
    public sealed class ScoreConfig
    {
        public ScoreConfig(float killValue, float floorBonus, float survivalStep)
        {
            KillValue = Guard.NonNegative(killValue, nameof(killValue));
            FloorBonus = Guard.NonNegative(floorBonus, nameof(floorBonus));
            SurvivalStep = Guard.NonNegative(survivalStep, nameof(survivalStep));
        }

        public float KillValue { get; }
        public float FloorBonus { get; }
        /// <summary>Incremento del multiplicador por cada jugador vivo al final de un piso.</summary>
        public float SurvivalStep { get; }

        /// <summary>Valores provisionales: 10 por baja, 50 por piso, +0,1 por superviviente.</summary>
        public static ScoreConfig Default => new ScoreConfig(10f, 50f, 0.1f);
    }
}
