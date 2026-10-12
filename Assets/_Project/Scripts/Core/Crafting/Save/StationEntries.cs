namespace Specimen.Core.Crafting.Save
{
    /// <summary>Estación y su nivel: <c>station=fragua_aldea:2</c>.</summary>
    public sealed class StationEntry
    {
        public string Station { get; }
        public int Level { get; }

        public StationEntry(string station, int level)
        {
            Station = station;
            Level = level;
        }
    }

    /// <summary>Pieza colocada: <c>part=fragua_aldea:0:pieza.crisol:37</c> (estación:ranura:ítem:durabilidad).</summary>
    public sealed class PartEntry
    {
        public string Station { get; }
        public int Slot { get; }
        public string ItemId { get; }
        public int Durability { get; }

        public PartEntry(string station, int slot, string itemId, int durability)
        {
            Station = station;
            Slot = slot;
            ItemId = itemId;
            Durability = durability;
        }
    }

    /// <summary>Contenido de una ranura: <c>slot=fragua_aldea:fuel:0:carbon_vegetal:4</c>.</summary>
    public sealed class SlotEntry
    {
        public string Station { get; }
        public string Kind { get; }
        public int Index { get; }
        public string ItemId { get; }
        public int Count { get; }

        public SlotEntry(string station, string kind, int index, string itemId, int count)
        {
            Station = station;
            Kind = kind;
            Index = index;
            ItemId = itemId;
            Count = count;
        }
    }
}
