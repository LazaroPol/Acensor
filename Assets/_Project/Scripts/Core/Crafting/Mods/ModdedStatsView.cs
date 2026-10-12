namespace Specimen.Core.Crafting.Mods
{
    /// <summary>Stats base y efectivos de un objeto con mods, para la UI (base → efectivo).</summary>
    public sealed class ModdedStatsView
    {
        public ModdedStatsView(GearStatSheet baseStats, GearStatSheet effective, int maxDurability)
        {
            Base = baseStats;
            Effective = effective;
            MaxDurability = maxDurability;
        }

        public GearStatSheet Base { get; }
        public GearStatSheet Effective { get; }

        /// <summary>Durabilidad máxima efectiva; nunca menor que 1 si el objeto se desgasta.</summary>
        public int MaxDurability { get; }

        /// <summary>Diferencia efectivo − base (positivo = mejora, salvo en Peso).</summary>
        public float Delta(GearStat stat) => Effective[stat] - Base[stat];
    }
}
