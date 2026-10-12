using System;
using Specimen.Core.Common;
using Specimen.Core.Items;

namespace Specimen.Core.Crafting.Stations
{
    /// <summary>
    /// Ficha de contenido de una pieza de estación (GDD §17.5): su definición, el nombre visible, qué
    /// recetas habilita (texto para la UI) y el objeto físico que ocupa la bolsa antes de colocarla.
    /// </summary>
    public sealed class StationPartEntry
    {
        public StationPartEntry(StationPartDefinition definition, string name, string enables, float weight)
        {
            Definition = Guard.NotNull(definition, nameof(definition));
            if (string.IsNullOrWhiteSpace(name)) throw new ArgumentException("Nombre vacío.", nameof(name));
            if (string.IsNullOrWhiteSpace(enables)) throw new ArgumentException("Falta qué habilita.", nameof(enables));
            Name = name;
            Enables = enables;
            Item = new ItemDefinition(definition.Id, name, ItemCategory.Tool, Rarity.Uncommon, weight: weight);
        }

        public StationPartDefinition Definition { get; }
        public string Id => Definition.Id;
        public string Name { get; }
        public string Enables { get; }
        public ItemDefinition Item { get; }

        public bool HasBonus => Definition.QualityBonus > 0 || Definition.TimeBonus > 0f || Definition.FuelSaving > 0f;

        public override string ToString() => $"{Name} ({Definition.Station})";
    }
}
