using System.Collections.Generic;

namespace Specimen.Core.Crafting.Mods
{
    /// <summary>Mods de §17.4/§17.5: los del Palo de madera aquí; el resto del catálogo en DefaultMods.Catalog.</summary>
    public static partial class DefaultMods
    {
        public const string EsquirlasMetal = "mod.esquirlas_metal";
        public const string PiedraAtada = "mod.piedra_atada";
        public const string Clavo = "mod.clavo";
        public const string TirasCuero = "mod.tiras_cuero";
        public const string ResinaIgnea = "mod.resina_ignea";

        private const GearTags Head = GearTags.Arma | GearTags.Madera;

        public static readonly ModDefinition Esquirlas = new ModDefinition(EsquirlasMetal, "Esquirlas de metal",
            ModSlotType.Cabeza, Head, new[]
            {
                GearModifier.Flat(GearStat.DañoCortante, 4f), GearModifier.Percent(GearStat.Durabilidad, -0.2f)
            });

        public static readonly ModDefinition Piedra = new ModDefinition(PiedraAtada, "Piedra atada",
            ModSlotType.Cabeza, Head, new[]
            {
                GearModifier.Flat(GearStat.DañoContundente, 5f), GearModifier.Flat(GearStat.Peso, 1.5f),
                GearModifier.Percent(GearStat.Velocidad, -0.05f)
            });

        public static readonly ModDefinition ClavoMod = new ModDefinition(Clavo, "Clavo",
            ModSlotType.Cabeza, Head, new[] { GearModifier.Flat(GearStat.DañoPerforante, 3f) });

        public static readonly ModDefinition Tiras = new ModDefinition(TirasCuero, "Tiras de cuero",
            ModSlotType.Empuñadura, GearTags.Arma | GearTags.Herramienta, new[]
            {
                GearModifier.Percent(GearStat.Velocidad, 0.08f), GearModifier.Percent(GearStat.Durabilidad, 0.1f)
            });

        public static readonly ModDefinition Resina = new ModDefinition(ResinaIgnea, "Resina ígnea",
            ModSlotType.Accesorio, GearTags.Arma, new[]
            {
                GearModifier.Percent(GearStat.DañoCortante, 0.1f), GearModifier.Percent(GearStat.DañoContundente, 0.1f),
                GearModifier.Percent(GearStat.DañoPerforante, 0.1f)
            }, removable: false);

        /// <summary>Los 15 mods. Perezoso: los campos viven en varios ficheros parciales (orden de inicio no garantizado).</summary>
        public static IReadOnlyList<ModDefinition> All => _all ?? (_all = new[]
        {
            Esquirlas, Piedra, ClavoMod, Tiras, Resina, FiloSilex, PuntaHueso, Remaches, Contrapeso, MangoFresno,
            PlacasCosidas, ForroAcolchado, AfiladoFino, GemaTallada, CorreajeLigero
        });

        private static ModDefinition[] _all;
    }
}
