using System.Collections.Generic;
using System.Linq;
using Specimen.Core.Common;
using Specimen.Core.Items;

namespace Specimen.Core.Crafting.Mods
{
    /// <summary>
    /// Instancia de equipo con sus mods instalados. Los huecos salen de su calidad;
    /// un objeto sin calidad no admite mods. Solo <see cref="ModInstaller"/> cambia los mods.
    /// </summary>
    public sealed class ModdedItem
    {
        private readonly List<ModDefinition> _mods = new List<ModDefinition>();

        public ModdedItem(ItemInstance item, QualityRules rules)
        {
            Item = Guard.NotNull(item, nameof(item));
            Guard.NotNull(rules, nameof(rules));
            Grade = item.Quality == Quality.None ? null : rules.Grade(item.Quality);
        }

        public ItemInstance Item { get; }

        /// <summary>Grado de calidad; null si el objeto no tiene calidad.</summary>
        public QualityGrade Grade { get; }

        public int SlotCount => Grade?.ModSlots ?? 0;
        public int FreeSlots => SlotCount - _mods.Count;
        public IReadOnlyList<ModDefinition> Mods => _mods;

        public int CountOf(ModSlotType type) => _mods.Count(m => m.SlotType == type);

        public ModDefinition Find(string modId) => _mods.FirstOrDefault(m => m.Id == modId);

        internal void Attach(ModDefinition mod) => _mods.Add(mod);

        internal void Detach(ModDefinition mod) => _mods.Remove(mod);

        public override string ToString() => $"{Item.Definition} ({_mods.Count}/{SlotCount} mods)";
    }
}
