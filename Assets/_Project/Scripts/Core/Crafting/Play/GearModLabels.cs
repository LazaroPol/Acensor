using System.Globalization;
using Specimen.Core.Crafting.Mods;

namespace Specimen.Core.Crafting.Play
{
    /// <summary>Textos de la ventana de mods (GDD §17.4, §17.7): nombre de cada stat, línea base → efectivo y resultados.</summary>
    public static class GearModLabels
    {
        public static string Stat(GearStat stat) => stat switch
        {
            GearStat.DañoCortante => "Daño cortante",
            GearStat.DañoContundente => "Daño contundente",
            GearStat.DañoPerforante => "Daño perforante",
            GearStat.Durabilidad => "Durabilidad",
            GearStat.Velocidad => "Velocidad",
            GearStat.Peso => "Peso",
            _ => "Armadura"
        };

        /// <summary>Mejora (+1), empeora (-1) o igual (0): más peso es peor; el resto, más es mejor.</summary>
        public static int Trend(ModdedStatsView view, GearStat stat)
        {
            float delta = view.Delta(stat);
            if (System.Math.Abs(delta) < 0.005f) return 0;
            int sign = delta > 0f ? 1 : -1;
            return stat == GearStat.Peso ? -sign : sign;
        }

        /// <summary>«Daño contundente: 0 → 5 (+5)»; sin cambio, solo el valor.</summary>
        public static string Line(ModdedStatsView view, GearStat stat)
        {
            string name = Stat(stat), from = Number(view.Base[stat]), to = Number(view.Effective[stat]);
            float delta = view.Delta(stat);
            if (System.Math.Abs(delta) < 0.005f) return $"{name}: {to}";
            return $"{name}: {from} → {to} ({(delta > 0f ? "+" : string.Empty)}{Number(delta)})";
        }

        public static string Result(ModInstallResult result) => result switch
        {
            ModInstallResult.Installed => "Mod instalado.",
            ModInstallResult.NoFreeSlot => "No quedan huecos de mod (sube la calidad del objeto).",
            ModInstallResult.SlotTypeTaken => "Ese tipo de hueco ya está ocupado.",
            ModInstallResult.Incompatible => "Ese mod no encaja en este objeto.",
            ModInstallResult.RequirementsNotMet => "No cumples los requisitos del mod.",
            ModInstallResult.ItemBroken => "El objeto está roto: repáralo antes.",
            _ => "No llevas ese mod en la bolsa."
        };

        public static string Result(ModUninstallResult result) => result switch
        {
            ModUninstallResult.Returned => "Mod retirado a la bolsa.",
            ModUninstallResult.Destroyed => "El mod se rompe al retirarlo.",
            ModUninstallResult.BagFull => "La bolsa está llena.",
            _ => "Ese mod no está instalado."
        };

        private static string Number(float value) => value.ToString("0.##", CultureInfo.InvariantCulture);
    }
}
