using Specimen.Core.Common;
using Specimen.Core.Crafting.Mods;
using Specimen.Core.Equipment;
using Specimen.Core.Items;

namespace Specimen.Core.Crafting.Play
{
    /// <summary>
    /// Stats base de un objeto para la ventana de mods (GDD §17.4): daño medio y velocidad del arma,
    /// armadura y peso. Sin ficha de equipo, solo el peso.
    /// </summary>
    public static class GearBaseStats
    {
        public static GearStatSheet Of(ItemDefinition item)
        {
            Guard.NotNull(item, nameof(item));
            var sheet = GearStatSheet.Empty.With(GearStat.Peso, item.Weight);
            if (!GearCatalog.TryGet(item.Id, out var piece)) return sheet;
            if (piece.Armor > 0f) sheet = sheet.With(GearStat.Armadura, piece.Armor);
            if (piece.Weapon == null) return sheet;
            return sheet.With(DamageStat(piece.Weapon.Kind), (piece.Weapon.Min + piece.Weapon.Max) / 2f)
                .With(GearStat.Velocidad, piece.Weapon.Speed);
        }

        private static GearStat DamageStat(WeaponKind kind) => kind switch
        {
            WeaponKind.Daga => GearStat.DañoPerforante,
            WeaponKind.Lanza => GearStat.DañoPerforante,
            WeaponKind.Varita => GearStat.DañoPerforante,
            WeaponKind.Baston => GearStat.DañoContundente,
            WeaponKind.Maza => GearStat.DañoContundente,
            _ => GearStat.DañoCortante
        };
    }
}
