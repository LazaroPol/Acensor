using System;

namespace Specimen.Core.Crafting.Content
{
    /// <summary>
    /// Escalera de tiers del recetario (GDD §17.6): T1 de partida, T2 nivel 5 y estación 2 o una pieza,
    /// T3 nivel 10 y estación 2, T4 nivel 15 y estación 3. Tiempo y XP crecen con el tier.
    /// </summary>
    public static class RecipeTiers
    {
        public const int Min = 1;
        public const int Max = 4;

        private static readonly int[] Levels = { 1, 5, 10, 15 };

        public static int ProfessionLevel(int tier) => Levels[Require(tier) - 1];

        public static int StationLevel(int tier, bool hasPart) =>
            Require(tier) switch { 1 => 1, 2 => hasPart ? 1 : 2, 3 => 2, _ => 3 };

        public static float CraftSeconds(int tier) => Require(tier) * 4f;

        public static int Xp(int tier) => Require(tier) * 10;

        /// <summary>Tier de una receta a partir de su nivel de oficio mínimo.</summary>
        public static int Of(int professionLevel)
        {
            for (var tier = Max; tier > Min; tier--)
                if (professionLevel >= Levels[tier - 1]) return tier;
            return Min;
        }

        private static int Require(int tier) =>
            tier < Min || tier > Max ? throw new ArgumentOutOfRangeException(nameof(tier), tier, null) : tier;
    }
}
