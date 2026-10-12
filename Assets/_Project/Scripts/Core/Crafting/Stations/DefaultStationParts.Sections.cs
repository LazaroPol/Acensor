using System.Collections.Generic;
using S = Specimen.Core.Crafting.Definitions.StationType;

namespace Specimen.Core.Crafting.Stations
{
    /// <summary>Tabla de piezas de §17.5 por estación (bonus: calidad, fracción de tiempo y de combustible ahorrada).</summary>
    public static partial class DefaultStationParts
    {
        private static void AddForgeKitchenAlchemy(List<StationPartEntry> l)
        {
            Add(l, "crisol", S.Fragua, "Crisol", "Fundir lingotes de acero, plata y aleaciones", 4f);
            Add(l, "fuelle_cuero", S.Fragua, "Fuelle de cuero", "Recetas tier 3+ de la fragua", 3f, time: 0.2f, fuel: 0.25f);
            Add(l, "yunque_acero", S.Fragua, "Yunque de acero", "Armas y armaduras tier 3–4", 20f, quality: 3);
            Add(l, "muela_afilar", S.Fragua, "Muela de afilar", "Mods de filo y afilado fino", 8f);
            Add(l, "olla_hierro", S.HogarCocina, "Olla de hierro", "Guisos y caldos tier 2+", 3f, quality: 2);
            Add(l, "parrilla", S.HogarCocina, "Parrilla", "Asados (buff de fuerza)", 2f, time: 0.15f);
            Add(l, "cazo_cobre", S.HogarCocina, "Cazo de cobre", "Confituras y conservas", 1.5f);
            Add(l, "mortero_piedra", S.MesaAlquimia, "Mortero de piedra", "Polvos y ungüentos", 2f);
            Add(l, "alambique", S.MesaAlquimia, "Alambique", "Pociones destiladas tier 2+", 3f, quality: 2);
            Add(l, "retorta_vidrio", S.MesaAlquimia, "Retorta de vidrio", "Ácidos, humo y bombas", 1f);
        }

        private static void AddWorkshops(List<StationPartEntry> l)
        {
            Add(l, "bastidor_telar", S.TallerSastre, "Bastidor de telar", "Telas tier 2+", 6f);
            Add(l, "tina_curtido", S.TallerSastre, "Tina de curtido", "Cuero curtido y endurecido", 8f);
            Add(l, "tijeras_maestro", S.TallerSastre, "Tijeras de maestro", "Armaduras ligeras tier 3+", 0.5f, quality: 2);
            Add(l, "torno_pedal", S.TallerIngenios, "Torno de pedal", "Engranajes y ballestas", 10f, time: 0.15f);
            Add(l, "compas_maestro", S.TallerIngenios, "Compás de maestro", "Trampas y torretas tier 3", 0.5f, quality: 2);
            Add(l, "tornillo_banco", S.BancoCarpintero, "Tornillo de banco", "Mangos, arcos y escudos tier 2+", 5f);
            Add(l, "juego_limas", S.BancoCarpintero, "Juego de limas", "Ganzúas y reparaciones finas", 1f, quality: 1);
            Add(l, "cincel_acero", S.BancoCantero, "Cincel de acero", "Piedra tallada tier 3, tótems", 0.5f, quality: 2);
            Add(l, "mazo_cantero", S.BancoCantero, "Mazo de cantero", "Bloques y señuelos", 3f, time: 0.15f);
        }
    }
}
