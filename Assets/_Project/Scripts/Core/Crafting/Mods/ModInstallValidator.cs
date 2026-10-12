using Specimen.Core.Common;
using Specimen.Core.Professions;

namespace Specimen.Core.Crafting.Mods
{
    /// <summary>Comprueba, en orden, las reglas de instalación de §17.4 sin cambiar nada.</summary>
    public sealed class ModInstallValidator
    {
        private readonly IGearTagProvider _tags;

        public ModInstallValidator(IGearTagProvider tags)
        {
            _tags = Guard.NotNull(tags, nameof(tags));
        }

        public ModInstallResult Check(ModdedItem target, ModDefinition mod, ProfessionLevels levels)
        {
            Guard.NotNull(target, nameof(target));
            Guard.NotNull(mod, nameof(mod));
            if (!target.Item.IsUsable) return ModInstallResult.ItemBroken;
            if (target.FreeSlots <= 0) return ModInstallResult.NoFreeSlot;
            if (target.CountOf(mod.SlotType) >= ModSlotRules.MaxPerType(mod.SlotType))
                return ModInstallResult.SlotTypeTaken;
            if (!mod.FitsTags(_tags.TagsOf(target.Item.Definition))) return ModInstallResult.Incompatible;
            if (!mod.Requirements.IsMetBy(target.Item.Quality, levels)) return ModInstallResult.RequirementsNotMet;
            return ModInstallResult.Installed;
        }
    }
}
