using Specimen.Core.Common;
using Specimen.Core.Items;
using Specimen.Core.Professions;

namespace Specimen.Core.Crafting.Mods
{
    /// <summary>
    /// Instala y retira mods moviéndolos entre la bolsa y el objeto. Atómico: si algo falla,
    /// ni la bolsa ni el objeto cambian (como <see cref="CraftTransaction"/>).
    /// </summary>
    public sealed class ModInstaller
    {
        private readonly ModInstallValidator _validator;

        public ModInstaller(ModInstallValidator validator)
        {
            _validator = Guard.NotNull(validator, nameof(validator));
        }

        public ModInstallResult Install(ModdedItem target, ModDefinition mod, IInventory bag,
            ProfessionLevels levels = null)
        {
            Guard.NotNull(bag, nameof(bag));
            var check = _validator.Check(target, mod, levels);
            if (check != ModInstallResult.Installed) return check;
            if (bag.CountOf(mod.Id) < 1) return ModInstallResult.ModNotOwned;
            bag.Remove(mod.Id);
            target.Attach(mod);
            return ModInstallResult.Installed;
        }

        public ModUninstallResult Uninstall(ModdedItem target, string modId, IInventory bag)
        {
            Guard.NotNull(target, nameof(target));
            Guard.NotNull(bag, nameof(bag));
            var mod = target.Find(modId);
            if (mod == null) return ModUninstallResult.NotInstalled;
            if (!mod.Removable)
            {
                target.Detach(mod);
                return ModUninstallResult.Destroyed;
            }
            var piece = ItemInstance.CreateNew(mod.Item);
            if (bag.SpaceFor(piece) < 1) return ModUninstallResult.BagFull;
            bag.Add(piece);
            target.Detach(mod);
            return ModUninstallResult.Returned;
        }
    }
}
