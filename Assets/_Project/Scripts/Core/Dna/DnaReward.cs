using Specimen.Core.Iteration.Ending;

namespace Specimen.Core.Dna
{
    /// <summary>
    /// Puntos de ADN que deja una iteración (GDD §3.5 simplificado para la demo): la victoria paga una base,
    /// un plus por piso y otro por cada pieza de loot subida al Osprey; morir o abandonar solo paga los pisos.
    /// </summary>
    public static class DnaReward
    {
        public const int VictoryBase = 10;
        public const int PerFloorOnVictory = 3;
        public const int PerLootItem = 2;
        public const int PerFloorOnFailure = 2;

        public static int For(IterationOutcome outcome, int floorsReached, int lootItems)
        {
            int floors = System.Math.Max(0, floorsReached);
            int loot = System.Math.Max(0, lootItems);
            return outcome == IterationOutcome.Victory
                ? VictoryBase + PerFloorOnVictory * floors + PerLootItem * loot
                : PerFloorOnFailure * floors;
        }
    }
}
