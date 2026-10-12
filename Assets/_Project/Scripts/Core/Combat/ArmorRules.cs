using System;

namespace Specimen.Core.Combat
{
    /// <summary>
    /// Mitigación por armadura al estilo de WoW clásico: reducción = A / (A + 400 + 85 · nivel del atacante),
    /// con un máximo del 75 %. Solo afecta al daño físico. A más nivel del atacante, menos protege la misma armadura.
    /// </summary>
    public static class ArmorRules
    {
        public const float Cap = 0.75f;

        public static float KFor(int attackerLevel) => 400f + 85f * Math.Max(1, attackerLevel);

        public static float Reduction(float armor, int attackerLevel)
        {
            float a = Math.Max(0f, armor);
            return Math.Min(Cap, a / (a + KFor(attackerLevel)));
        }

        /// <summary>Calculadora de mitigación para un atacante de ese nivel (para <see cref="DamageCalculator"/>).</summary>
        public static ArmorMitigation MitigationFor(int attackerLevel) => new ArmorMitigation(KFor(attackerLevel));
    }
}
