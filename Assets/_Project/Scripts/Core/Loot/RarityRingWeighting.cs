using Specimen.Core.Common;
using Specimen.Core.Items;

namespace Specimen.Core.Loot
{
    /// <summary>
    /// Ponderación por defecto: en el anillo 1 se respetan los pesos base; cada anillo más profundo
    /// suma <c>bonusPerStep × nivel de rareza</c> al multiplicador. Lo común no cambia y lo raro
    /// gana cada vez más peso relativo cuanto más lejos del ascensor.
    /// </summary>
    public sealed class RarityRingWeighting : IRingWeighting
    {
        public const float DefaultBonusPerStep = 0.5f;

        public float BonusPerStep { get; }

        public RarityRingWeighting(float bonusPerStep = DefaultBonusPerStep)
        {
            BonusPerStep = Guard.NonNegative(bonusPerStep, nameof(bonusPerStep));
        }

        public float Multiplier(Rarity rarity, int ring)
        {
            int depth = DepthRings.Validate(ring, nameof(ring)) - DepthRings.Min;
            return 1f + BonusPerStep * (int)rarity * depth;
        }
    }
}
