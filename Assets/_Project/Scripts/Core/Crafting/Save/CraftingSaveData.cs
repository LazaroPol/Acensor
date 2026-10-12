using System.Collections.Generic;

namespace Specimen.Core.Crafting.Save
{
    /// <summary>Instantánea del progreso de oficios e inventario del personaje (GDD §17.7).</summary>
    public sealed class CraftingSaveData
    {
        public List<SkillEntry> Skills { get; } = new List<SkillEntry>();
        public List<string> KnownRecipes { get; } = new List<string>();
        public List<string> ReadVolumes { get; } = new List<string>();
        public List<StationEntry> Stations { get; } = new List<StationEntry>();
        public List<PartEntry> Parts { get; } = new List<PartEntry>();
        public List<SlotEntry> Slots { get; } = new List<SlotEntry>();
        public List<JobEntry> Jobs { get; } = new List<JobEntry>();
        public List<ItemEntry> Items { get; } = new List<ItemEntry>();
        public List<InventoryEntry> Inventory { get; } = new List<InventoryEntry>();
    }
}
