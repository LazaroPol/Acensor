using System;
using Specimen.Core.Crafting.Mods;
using Specimen.Core.Items;
using Specimen.Core.Professions;

namespace Specimen.Core.Crafting.Authoring
{
    /// <summary>Datos serializables de un <see cref="ModDefinition"/> (Unity no guarda nulables: <see cref="requiresProfession"/>).</summary>
    [Serializable]
    public sealed class ModData
    {
        public string id;
        public string name;
        public ModSlotType slotType;
        public GearTags compatibleTags;
        public ModifierData[] modifiers = new ModifierData[0];
        public Quality minQuality = Quality.None;
        public bool requiresProfession;
        public ProfessionId profession;
        public int minLevel;
        public bool removable = true;
        public float weight;
    }

    [Serializable]
    public sealed class ModifierData
    {
        public GearStat stat;
        public GearModifierKind kind;
        public float value;
    }
}
