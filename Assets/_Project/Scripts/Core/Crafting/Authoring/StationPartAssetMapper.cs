using Specimen.Core.Common;
using Specimen.Core.Crafting.Stations;

namespace Specimen.Core.Crafting.Authoring
{
    /// <summary>Convierte <see cref="StationPartData"/> ⇄ <see cref="StationPartEntry"/> (definición + ítem).</summary>
    public static class StationPartAssetMapper
    {
        public static StationPartEntry ToEntry(StationPartData data)
        {
            Guard.NotNull(data, nameof(data));
            var definition = new StationPartDefinition(data.id, data.station, data.maxDurability,
                data.qualityBonus, data.timeBonus, data.fuelSaving);
            return new StationPartEntry(definition, data.name, data.enables, data.weight);
        }

        public static StationPartData FromEntry(StationPartEntry entry)
        {
            Guard.NotNull(entry, nameof(entry));
            var d = entry.Definition;
            return new StationPartData
            {
                id = d.Id, name = entry.Name, station = d.Station, maxDurability = d.MaxDurability,
                qualityBonus = d.QualityBonus, timeBonus = d.TimeBonus, fuelSaving = d.FuelSaving,
                enables = entry.Enables, weight = entry.Item.Weight
            };
        }
    }
}
