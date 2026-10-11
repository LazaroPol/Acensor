using Specimen.Core.Common;

namespace Specimen.Core.Elevator
{
    /// <summary>
    /// Parámetros de la Cocina: fracción de duración extra por cada nivel por encima del primero
    /// (con 0,5 un plato dura ×1 a nivel 1, ×1,5 a nivel 2, ×2 a nivel 3).
    /// </summary>
    public sealed class KitchenConfig
    {
        public static readonly KitchenConfig Default = new KitchenConfig(0.5f);

        public float DurationBonusPerLevel { get; }

        public KitchenConfig(float durationBonusPerLevel)
        {
            DurationBonusPerLevel = Guard.NonNegative(durationBonusPerLevel, nameof(durationBonusPerLevel));
        }

        public float DurationAt(Meal meal, int level)
            => meal.BaseDurationSeconds * (1f + DurationBonusPerLevel * (level - 1));
    }
}
