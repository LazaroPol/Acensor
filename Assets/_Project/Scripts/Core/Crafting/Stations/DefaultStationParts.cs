using System.Collections.Generic;
using System.Linq;
using Specimen.Core.Crafting.Definitions;
using S = Specimen.Core.Crafting.Definitions.StationType;

namespace Specimen.Core.Crafting.Stations
{
    /// <summary>Las 19 piezas de estación de §17.5. Ids <c>pieza.*</c> estables: los citan las recetas.</summary>
    public static partial class DefaultStationParts
    {
        public const string Prefix = "pieza.";

        public static IReadOnlyList<StationPartEntry> All { get; } = Build();

        public static IEnumerable<StationPartEntry> For(StationType station)
            => All.Where(p => p.Definition.Station == station);

        public static bool TryGet(string id, out StationPartEntry entry)
        {
            entry = All.FirstOrDefault(p => p.Id == id);
            return entry != null;
        }

        private static IReadOnlyList<StationPartEntry> Build()
        {
            var list = new List<StationPartEntry>();
            AddForgeKitchenAlchemy(list);
            AddWorkshops(list);
            return list;
        }

        private static void Add(List<StationPartEntry> list, string slug, S station, string name, string enables,
            float weight, int quality = 0, float time = 0f, float fuel = 0f)
            => list.Add(new StationPartEntry(new StationPartDefinition(Prefix + slug, station,
                qualityBonus: quality, timeBonus: time, fuelSaving: fuel), name, enables, weight));
    }
}
