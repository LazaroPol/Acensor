using System.Linq;
using Specimen.Core.Common;
using Specimen.Core.Crafting.Mods;
using Specimen.Core.Professions;

namespace Specimen.Core.Crafting.Authoring
{
    /// <summary>Convierte <see cref="ModData"/> ⇄ <see cref="ModDefinition"/>.</summary>
    public static class ModAssetMapper
    {
        public static ModDefinition ToDefinition(ModData data)
        {
            Guard.NotNull(data, nameof(data));
            var modifiers = (data.modifiers ?? new ModifierData[0]).Select(m => new GearModifier(m.stat, m.kind, m.value));
            var requirements = new ModRequirements(data.minQuality,
                data.requiresProfession ? data.profession : (ProfessionId?)null, data.minLevel);
            return new ModDefinition(data.id, data.name ?? string.Empty, data.slotType, data.compatibleTags,
                modifiers.ToList(), requirements, data.removable, data.weight);
        }

        public static ModData FromDefinition(ModDefinition mod)
        {
            Guard.NotNull(mod, nameof(mod));
            var req = mod.Requirements;
            return new ModData
            {
                id = mod.Id, name = mod.Name, slotType = mod.SlotType, compatibleTags = mod.CompatibleTags,
                modifiers = mod.Modifiers.Select(m => new ModifierData { stat = m.Stat, kind = m.Kind, value = m.Value }).ToArray(),
                minQuality = req.MinQuality, requiresProfession = req.Profession.HasValue,
                profession = req.Profession ?? default, minLevel = req.MinLevel,
                removable = mod.Removable, weight = mod.Item.Weight
            };
        }
    }
}
