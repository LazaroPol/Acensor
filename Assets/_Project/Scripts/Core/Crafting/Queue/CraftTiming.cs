using System;

namespace Specimen.Core.Crafting.Queue
{
    /// <summary>
    /// Tiempo real de un trabajo (GDD §17.3): <c>CraftSeconds × (1 − bonusOficio) × (1 − bonusPiezas)</c>,
    /// mínimo <see cref="MinSeconds"/>.
    /// </summary>
    public static class CraftTiming
    {
        public const float MinSeconds = 1f;

        public static float Duration(float craftSeconds, float professionBonus, float partsBonus)
        {
            if (craftSeconds < 0f) throw new ArgumentOutOfRangeException(nameof(craftSeconds));
            float seconds = craftSeconds * (1f - Clamp(professionBonus)) * (1f - Clamp(partsBonus));
            return Math.Max(MinSeconds, seconds);
        }

        /// <summary>Segundos que dura una unidad de combustible con un ahorro (0,25 = −25 % consumo).</summary>
        public static float BurnTime(float burnSeconds, float fuelSaving) =>
            burnSeconds / (1f - Clamp(fuelSaving));

        private static float Clamp(float bonus) => Math.Max(0f, Math.Min(0.9f, bonus));
    }
}
