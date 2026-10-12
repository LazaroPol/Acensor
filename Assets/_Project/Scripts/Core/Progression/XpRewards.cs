using System;
using Specimen.Core.Bestiary;
using Specimen.Core.Common;

namespace Specimen.Core.Progression
{
    /// <summary>
    /// Experiencia por derrotar criaturas (según su vida base) y por recolectar o fabricar. Matar criaturas
    /// muy por debajo de tu nivel da menos, como en los MMO clásicos.
    /// </summary>
    public static class XpRewards
    {
        public const int Gather = 8;
        public const int Craft = 15;

        public static int ForKill(BestiaryEntry entry, int playerLevel, int creatureLevel = 1)
        {
            Guard.NotNull(entry, nameof(entry));
            float baseXp = 20f + entry.Archetype.BaseStats.MaxHealth * 0.6f;
            int gap = playerLevel - creatureLevel;
            float factor = gap <= 2 ? 1f : Math.Max(0.1f, 1f - (gap - 2) * 0.15f);
            return Math.Max(1, (int)Math.Round(baseXp * factor));
        }
    }
}
