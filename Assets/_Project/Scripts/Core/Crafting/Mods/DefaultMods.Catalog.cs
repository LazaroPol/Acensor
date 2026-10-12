using static Specimen.Core.Crafting.Mods.GearModifier;
using G = Specimen.Core.Crafting.Mods.GearStat;
using M = Specimen.Core.Crafting.Mods.ModSlotType;
using T = Specimen.Core.Crafting.Mods.GearTags;

namespace Specimen.Core.Crafting.Mods
{
    /// <summary>Resto de la tabla de mods de §17.5 (ids <c>mod.*</c> estables: se guardan en los objetos).</summary>
    public static partial class DefaultMods
    {
        public static readonly ModDefinition FiloSilex = new ModDefinition("mod.filo_silex", "Filo de sílex",
            M.Filo, T.Arma, new[] { Flat(G.DañoCortante, 2f) });

        public static readonly ModDefinition PuntaHueso = new ModDefinition("mod.punta_hueso", "Punta de hueso",
            M.Filo, T.Lanza | T.Flecha, new[] { Flat(G.DañoPerforante, 4f) });

        public static readonly ModDefinition Remaches = new ModDefinition("mod.remaches", "Remaches",
            M.Refuerzo, T.Metal, new[] { Percent(G.Durabilidad, 0.25f), Flat(G.Peso, 0.5f) });

        public static readonly ModDefinition Contrapeso = new ModDefinition("mod.contrapeso_plomo", "Contrapeso de plomo",
            M.Cabeza, T.ArmaContundente, new[] { Percent(G.DañoContundente, 0.2f), Percent(G.Velocidad, -0.1f) });

        public static readonly ModDefinition MangoFresno = new ModDefinition("mod.mango_fresno", "Mango de fresno",
            M.Empuñadura, T.Herramienta | T.Arma, new[] { Percent(G.Velocidad, 0.15f) });

        public static readonly ModDefinition PlacasCosidas = new ModDefinition("mod.placas_cosidas", "Placas cosidas",
            M.Refuerzo, T.Armadura, new[] { Flat(G.Armadura, 6f), Flat(G.Peso, 2f) });

        public static readonly ModDefinition ForroAcolchado = new ModDefinition("mod.forro_acolchado", "Forro acolchado",
            M.Forro, T.Armadura, new[] { Flat(G.Armadura, 3f), Percent(G.Durabilidad, 0.1f) });

        public static readonly ModDefinition AfiladoFino = new ModDefinition("mod.afilado_fino", "Afilado fino",
            M.Filo, T.Metal, new[] { Percent(G.DañoCortante, 0.15f) });

        public static readonly ModDefinition GemaTallada = new ModDefinition("mod.gema_tallada", "Gema tallada",
            M.Accesorio, T.Arma | T.Armadura, new[]
            {
                Percent(G.DañoCortante, 0.05f), Percent(G.DañoContundente, 0.05f), Percent(G.DañoPerforante, 0.05f),
                Percent(G.Velocidad, 0.05f), Percent(G.Armadura, 0.05f), Percent(G.Durabilidad, -0.1f)
            });

        public static readonly ModDefinition CorreajeLigero = new ModDefinition("mod.correaje_ligero", "Correaje ligero",
            M.Accesorio, T.Armadura, new[] { Percent(G.Peso, -0.2f) });
    }
}
