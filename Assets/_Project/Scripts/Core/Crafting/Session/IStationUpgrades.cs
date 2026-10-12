using Specimen.Core.Crafting.Definitions;
using Specimen.Core.Crafting.Stations;

namespace Specimen.Core.Crafting.Session
{
    /// <summary>Fuente de datos de las mejoras de estación: la del nivel indicado o null si no hay.</summary>
    public interface IStationUpgrades
    {
        StationUpgrade For(StationType station, int targetLevel);
    }
}
