using System.Collections.Generic;
using Specimen.Core.Common;
using Specimen.Core.Items;

namespace Specimen.Core.Crafting.Mods
{
    /// <summary>
    /// Etiquetas derivadas de la categoría (Arma, Armadura, Herramienta) y del material (Madera, Metal),
    /// más las extra que se declaren por id (ArmaContundente, Lanza, Flecha…).
    /// </summary>
    public sealed class DefaultGearTags : IGearTagProvider
    {
        private readonly IReadOnlyDictionary<string, GearTags> _extra;

        public DefaultGearTags(IReadOnlyDictionary<string, GearTags> extraById = null)
        {
            _extra = extraById ?? new Dictionary<string, GearTags>();
        }

        public GearTags TagsOf(ItemDefinition item)
        {
            Guard.NotNull(item, nameof(item));
            var tags = FromCategory(item.Category);
            if (item.HasTag(MaterialTags.Madera)) tags |= GearTags.Madera;
            if (item.HasTag(MaterialTags.Metal)) tags |= GearTags.Metal;
            return _extra.TryGetValue(item.Id, out var extra) ? tags | extra : tags;
        }

        private static GearTags FromCategory(ItemCategory category)
        {
            switch (category)
            {
                case ItemCategory.Weapon: return GearTags.Arma;
                case ItemCategory.Armor: return GearTags.Armadura;
                case ItemCategory.Tool: return GearTags.Herramienta;
                default: return GearTags.None;
            }
        }
    }
}
