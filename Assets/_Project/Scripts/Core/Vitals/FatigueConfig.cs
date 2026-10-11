using System;
using Specimen.Core.Common;

namespace Specimen.Core.Vitals
{
    /// <summary>
    /// Parámetros de la fatiga: puntos por piso, extra si el jugador quedó atrapado, penalización de
    /// stamina máxima por punto (fracción) y tope de puntos. Penalización × tope nunca supera el 100%.
    /// </summary>
    public sealed class FatigueConfig
    {
        public static readonly FatigueConfig Default = new FatigueConfig(1, 2, 0.1f, 5);

        public int PointsPerFloor { get; }
        public int TrappedExtraPoints { get; }
        public float StaminaPenaltyPerPoint { get; }
        public int MaxPoints { get; }

        public FatigueConfig(int pointsPerFloor, int trappedExtraPoints, float staminaPenaltyPerPoint, int maxPoints)
        {
            PointsPerFloor = Guard.NonNegative(pointsPerFloor, nameof(pointsPerFloor));
            TrappedExtraPoints = Guard.NonNegative(trappedExtraPoints, nameof(trappedExtraPoints));
            StaminaPenaltyPerPoint = Guard.NonNegative(staminaPenaltyPerPoint, nameof(staminaPenaltyPerPoint));
            MaxPoints = Guard.Positive(maxPoints, nameof(maxPoints));
            if (staminaPenaltyPerPoint * maxPoints > 1f)
                throw new ArgumentOutOfRangeException(nameof(staminaPenaltyPerPoint), staminaPenaltyPerPoint,
                    "La penalización total (por punto × tope) no puede superar el 100%.");
        }

        /// <summary>Factor multiplicador de la stamina máxima para un nivel de fatiga dado.</summary>
        public float StaminaFactorAt(int points) => 1f - StaminaPenaltyPerPoint * points;
    }
}
