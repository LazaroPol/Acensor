using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Specimen.Core.Common;
using Specimen.Core.Crafting.Mods;
using Specimen.Core.Crafting.Session;
using Specimen.Core.Items;
using Specimen.Core.Professions;

namespace Specimen.Core.Crafting.Play
{
    /// <summary>
    /// Ventana de mods de un objeto (GDD §17.4, §17.7): recuerda los mods de cada instancia, los pone y
    /// quita moviéndolos entre la bolsa y el objeto, y da sus stats base → efectivos.
    /// </summary>
    public sealed class GearModBench
    {
        private readonly ConditionalWeakTable<ItemInstance, ModdedItem> _items = new ConditionalWeakTable<ItemInstance, ModdedItem>();
        private readonly Dictionary<string, ModDefinition> _mods;
        private readonly QualityRules _rules;
        private readonly CraftingProfile _profile;
        private readonly ModInstallValidator _validator;
        private readonly ModInstaller _installer;

        public GearModBench(IEnumerable<ModDefinition> mods, QualityRules rules, CraftingProfile profile,
            IGearTagProvider tags = null)
        {
            _mods = Guard.NotNull(mods, nameof(mods)).ToDictionary(m => m.Id);
            _rules = Guard.NotNull(rules, nameof(rules));
            _profile = Guard.NotNull(profile, nameof(profile));
            _validator = new ModInstallValidator(tags ?? new DefaultGearTags());
            _installer = new ModInstaller(_validator);
        }

        public ModDefinition Mod(string modId) => modId != null && _mods.TryGetValue(modId, out var m) ? m : null;

        public ModdedItem For(ItemInstance item) =>
            _items.GetValue(Guard.NotNull(item, nameof(item)), i => new ModdedItem(i, _rules));

        /// <summary>Mods instalados en el objeto (ninguno si nunca se tocó).</summary>
        public IReadOnlyList<ModDefinition> ModsOf(ItemInstance item) =>
            _items.TryGetValue(item, out var modded) ? modded.Mods : new List<ModDefinition>();

        /// <summary>Mods de la bolsa que se podrían poner ahora en el objeto.</summary>
        public IReadOnlyList<ModDefinition> Installable(ItemInstance item)
        {
            var target = For(item);
            var levels = Levels();
            return _profile.Bag.Stacks.Select(s => Mod(s.Id)).Where(m => m != null).Distinct()
                .Where(m => _validator.Check(target, m, levels) == ModInstallResult.Installed).ToList();
        }

        public ModInstallResult Install(ItemInstance item, string modId)
        {
            var mod = Mod(modId);
            return mod == null ? ModInstallResult.ModNotOwned : _installer.Install(For(item), mod, _profile.Bag, Levels());
        }

        public ModUninstallResult Uninstall(ItemInstance item, string modId) =>
            _installer.Uninstall(For(item), modId, _profile.Bag);

        public ModdedStatsView Stats(ItemInstance item) => ModdedStats.Compute(For(item), GearBaseStats.Of(item.Definition));

        /// <summary>Pone los mods guardados sin pasar por la bolsa (carga de partida); ignora ids desconocidos.</summary>
        public void Restore(ItemInstance item, IEnumerable<string> modIds)
        {
            var target = For(item);
            foreach (var id in modIds ?? Enumerable.Empty<string>())
            {
                var mod = Mod(id);
                if (mod != null && target.FreeSlots > 0) target.Attach(mod);
            }
        }

        private ProfessionLevels Levels()
        {
            var levels = new ProfessionLevels(_profile.Skills.FirstOrDefault()?.Curve);
            foreach (var skill in _profile.Skills) levels.For(skill.Profession).Restore(skill.Level, skill.Xp);
            return levels;
        }
    }
}
