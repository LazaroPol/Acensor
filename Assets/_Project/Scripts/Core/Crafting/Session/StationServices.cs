using System.Collections.Generic;
using System.Linq;
using Specimen.Core.Crafting.Queue;
using Specimen.Core.Crafting.Stations;

namespace Specimen.Core.Crafting.Session
{
    /// <summary>Datos que una sesión de estación consulta: piezas colocables, combustibles y mejoras.</summary>
    public sealed class StationServices
    {
        public StationServices(IEnumerable<StationPartEntry> parts, FuelTable fuel = null, IStationUpgrades upgrades = null)
        {
            Parts = (parts ?? Enumerable.Empty<StationPartEntry>()).ToList().AsReadOnly();
            Fuel = fuel ?? FuelTable.Default;
            Upgrades = upgrades ?? DefaultStationUpgrades.Instance;
        }

        public static StationServices Default { get; } = new StationServices(DefaultStationParts.All);

        public IReadOnlyList<StationPartEntry> Parts { get; }
        public FuelTable Fuel { get; }
        public IStationUpgrades Upgrades { get; }

        public StationPartEntry PartOf(string id) => Parts.FirstOrDefault(p => p.Id == id);
    }
}
