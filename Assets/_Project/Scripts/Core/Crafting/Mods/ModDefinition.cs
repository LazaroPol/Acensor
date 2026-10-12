using System;
using System.Collections.Generic;
using System.Linq;
using Specimen.Core.Common;
using Specimen.Core.Items;

namespace Specimen.Core.Crafting.Mods
{
    /// <summary>
    /// Pieza añadible a un objeto (GDD §17.4). Su <see cref="Item"/> es lo que ocupa la bolsa
    /// antes de instalarlo y después de retirarlo; comparte <see cref="Id"/> con él.
    /// </summary>
    public sealed class ModDefinition
    {
        public const int DefaultStack = 10;

        public ModDefinition(string id, string name, ModSlotType slotType, GearTags compatibleTags,
            IEnumerable<GearModifier> modifiers, ModRequirements requirements = null, bool removable = true,
            float weight = 0f)
        {
            if (string.IsNullOrWhiteSpace(id)) throw new ArgumentException("Id vacío.", nameof(id));
            if (compatibleTags == GearTags.None)
                throw new ArgumentException("Un mod necesita alguna etiqueta compatible.", nameof(compatibleTags));
            Id = id;
            Name = Guard.NotNull(name, nameof(name));
            SlotType = slotType;
            CompatibleTags = compatibleTags;
            Modifiers = Guard.NotNull(modifiers, nameof(modifiers)).ToArray();
            Requirements = requirements ?? ModRequirements.None;
            Removable = removable;
            Item = new ItemDefinition(id, name, ItemCategory.Material, Rarity.Common,
                maxStack: DefaultStack, weight: weight);
        }

        public string Id { get; }
        public string Name { get; }
        public ModSlotType SlotType { get; }
        public GearTags CompatibleTags { get; }
        public IReadOnlyList<GearModifier> Modifiers { get; }
        public ModRequirements Requirements { get; }

        /// <summary>Si al desinstalarlo vuelve a la bolsa; si no, se destruye (p. ej. resinas).</summary>
        public bool Removable { get; }
        public ItemDefinition Item { get; }

        /// <summary>True si alguna etiqueta compatible coincide con las del objeto base.</summary>
        public bool FitsTags(GearTags baseTags) => (CompatibleTags & baseTags) != GearTags.None;

        public override string ToString() => $"{Name} [{SlotType}]";
    }
}
