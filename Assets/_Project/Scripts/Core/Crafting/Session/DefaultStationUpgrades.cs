using Specimen.Core.Crafting.Definitions;
using Specimen.Core.Crafting.Stations;
using Specimen.Core.Items;

namespace Specimen.Core.Crafting.Session
{
    /// <summary>
    /// Mejoras por defecto de cualquier estación (tabla orientativa de la Fragua, GDD §17.3): nivel 2 con
    /// piedra y metal, cola de 4 y +2 de calidad; nivel 3 con piedra y metal de tier alto, cola de 6 y +4.
    /// </summary>
    public sealed class DefaultStationUpgrades : IStationUpgrades
    {
        public static DefaultStationUpgrades Instance { get; } = new DefaultStationUpgrades();

        public StationUpgrade For(StationType station, int targetLevel)
        {
            switch (targetLevel)
            {
                case 2:
                    return new StationUpgrade(station, 2, new[]
                    {
                        IngredientSpec.ForTag(MaterialTags.Piedra, 20),
                        IngredientSpec.ForTag(MaterialTags.Metal, 10, minTier: 2)
                    }, queueSize: 4, qualityBonus: 2);
                case 3:
                    return new StationUpgrade(station, 3, new[]
                    {
                        IngredientSpec.ForTag(MaterialTags.Piedra, 15, minTier: 3),
                        IngredientSpec.ForTag(MaterialTags.Metal, 10, minTier: 3)
                    }, queueSize: 6, qualityBonus: 4);
                default:
                    return null;
            }
        }
    }
}
