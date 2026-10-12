using System.Collections.Generic;
using T = Specimen.Core.Items.MaterialTags;

namespace Specimen.Core.Items.Catalog
{
    /// <summary>Materiales de §17.5: madera, piedra, cuero, tela y hueso (reutiliza madera, piedra y cuero).</summary>
    public static partial class ItemCatalog
    {
        private static void AddNaturalMaterials(List<ItemDefinition> list)
        {
            Mat(list, "rama_seca", "Rama seca", 1, 0.3f, T.Madera | T.Combustible);
            Mat(list, "tablon_roble", "Tablón de roble", 2, 2f, T.Madera);
            Mat(list, "madera_fresno", "Madera de fresno", 2, 1f, T.Madera);
            Mat(list, "madera_tejo", "Madera de tejo", 3, 1f, T.Madera);
            Mat(list, "madera_corazon_negro", "Madera de corazón negro", 4, 1.5f, T.Madera);
            Mat(list, "silex", "Sílex", 1, 0.5f, T.Piedra);
            Mat(list, "piedra_labrada", "Piedra labrada", 2, 3f, T.Piedra);
            Mat(list, "bloque_granito", "Bloque de granito", 2, 4f, T.Piedra);
            Mat(list, "marmol", "Mármol", 3, 3f, T.Piedra);
            Mat(list, "obsidiana", "Obsidiana", 4, 1.5f, T.Piedra | T.Vidrio);
            Mat(list, "piel_cruda", "Piel cruda", 1, 0.8f, T.Cuero);
            Mat(list, "cuero_endurecido", "Cuero endurecido", 3, 0.7f, T.Cuero);
            Mat(list, "piel_bestia_mayor", "Piel de bestia mayor", 4, 1.5f, T.Cuero);
            Mat(list, "fibra_vegetal", "Fibra vegetal", 1, 0.1f, T.Tela | T.Hierba);
            Mat(list, "lino", "Lino", 2, 0.2f, T.Tela);
            Mat(list, "lana", "Lana", 2, 0.3f, T.Tela);
            Mat(list, "pano_encerado", "Paño encerado", 3, 0.4f, T.Tela);
            Mat(list, "seda", "Seda", 4, 0.1f, T.Tela);
            Mat(list, "hueso", "Hueso", 1, 0.4f, T.Hueso);
            Mat(list, "astilla_hueso", "Astilla de hueso", 1, 0.1f, T.Hueso);
            Mat(list, "colmillo", "Colmillo", 2, 0.3f, T.Hueso);
            Mat(list, "asta_ciervo_blanco", "Asta de ciervo blanco", 3, 1f, T.Hueso);
            Mat(list, "hueso_bestia_mayor", "Hueso de bestia mayor", 4, 2f, T.Hueso);
        }
    }
}
