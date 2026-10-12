using System.Collections.Generic;
using R = Specimen.Core.Items.Rarity;
using T = Specimen.Core.Items.MaterialTags;

namespace Specimen.Core.Items.Catalog
{
    /// <summary>Comida, preparados e intermedios del recetario de oficios (§17.6).</summary>
    public static partial class ItemCatalog
    {
        private static void AddProductSupplies(List<ItemDefinition> l)
        {
            Consumable(l, "carne_asada", "Carne asada", R.Common);
            Consumable(l, "pescado_brasa", "Pescado a la brasa", R.Common);
            Consumable(l, "pan_trigo", "Pan de trigo", R.Common);
            Consumable(l, "gachas", "Gachas", R.Common);
            Consumable(l, "guiso_raices", "Guiso de raíces", R.Uncommon);
            Consumable(l, "caldo_huesos", "Caldo de huesos", R.Uncommon);
            Consumable(l, "asado_especiado", "Asado especiado", R.Uncommon);
            Consumable(l, "conserva_miel", "Conserva de miel", R.Uncommon);
            Consumable(l, "racion_marcha", "Ración de marcha", R.Uncommon);
            Consumable(l, "estofado_cazador", "Estofado del cazador", R.Rare);
            Consumable(l, "pastel_carne", "Pastel de carne", R.Rare);
            Consumable(l, "confitura_sauco", "Confitura de saúco", R.Rare);
            Consumable(l, "banquete_senor", "Banquete del señor", R.Epic);
            Consumable(l, "festin_alquimico", "Festín alquímico", R.Epic);
            Consumable(l, "unguento_hierbas", "Ungüento de hierbas", R.Common);
            Consumable(l, "polvo_humo", "Polvo de humo", R.Common);
            Consumable(l, "antidoto", "Antídoto simple", R.Common);
            Consumable(l, "pocion_aguante", "Poción de aguante", R.Uncommon);
            Consumable(l, "tonico_vision", "Tónico de visión nocturna", R.Uncommon);
            Consumable(l, "acido_disolvente", "Ácido disolvente", R.Rare);
            Consumable(l, "bomba_azufre", "Bomba de azufre", R.Rare);
            Consumable(l, "elixir_vigor", "Elixir de vigor", R.Rare);
            Consumable(l, "panacea", "Panacea", R.Epic);
            Consumable(l, "humo_cegador", "Humo cegador mayor", R.Epic);
            AddIntermediates(l);
        }

        private static void AddIntermediates(List<ItemDefinition> l)
        {
            Mat(l, "esquirlas_metal", "Esquirlas de metal", 1, 0.2f, T.Metal);
            Mat(l, "parche_metal", "Parche de chatarra", 1, 0.5f, T.Metal);
            Mat(l, "contrapeso_plomo", "Contrapeso de plomo", 2, 1f, T.Metal);
            Mat(l, "cerrojo_acero", "Cerrojo de acero", 3, 0.5f, T.Metal);
            Mat(l, "mecanismo_apertura", "Mecanismo de apertura", 3, 0.6f, T.Metal);
            Mat(l, "tiras_cuero", "Tiras de cuero", 1, 0.1f, T.Cuero);
            Mat(l, "forro_acolchado", "Forro acolchado", 2, 0.4f, T.Tela);
            Mat(l, "resina_ignea", "Resina ígnea", 3, 0.3f, T.Hierba | T.Mineral);
            Mat(l, "punta_hueso", "Punta de hueso", 1, 0.1f, T.Hueso);
            Mat(l, "mango_fresno", "Mango de fresno", 2, 0.5f, T.Madera);
            Mat(l, "gema_tallada", "Gema tallada", 4, 0.1f, T.Mineral);
        }
    }
}
