using System;

namespace Specimen.Core.Items
{
    /// <summary>
    /// Reglas por rareza: cuanto más rara, más aguanta el equipo antes de romperse.
    /// </summary>
    public static class RarityRules
    {
        public static float DurabilityMultiplier(this Rarity rarity)
        {
            switch (rarity)
            {
                case Rarity.Common: return 1f;
                case Rarity.Uncommon: return 1.25f;
                case Rarity.Rare: return 1.5f;
                case Rarity.Epic: return 2f;
                case Rarity.Legendary: return 3f;
                default: throw new ArgumentOutOfRangeException(nameof(rarity), rarity, null);
            }
        }

        /// <summary>Durabilidad máxima resultante (redondeada, nunca por debajo de la base).</summary>
        public static int ScaleDurability(this Rarity rarity, int baseDurability)
            => (int)Math.Round(baseDurability * rarity.DurabilityMultiplier(), MidpointRounding.AwayFromZero);
    }
}
