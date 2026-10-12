using System.Collections.Generic;
using T = Specimen.Core.Items.MaterialTags;

namespace Specimen.Core.Items.Catalog
{
    /// <summary>Materiales de §17.5: hierbas y comida (reutiliza carne y hierba de ceniza).</summary>
    public static partial class ItemCatalog
    {
        private static void AddOrganicMaterials(List<ItemDefinition> list)
        {
            Mat(list, "hierba_comun", "Hierba común", 1, 0.1f, T.Hierba);
            Mat(list, "raiz_amarga", "Raíz amarga", 1, 0.1f, T.Hierba);
            Mat(list, "flor_sauco", "Flor de saúco", 2, 0.05f, T.Hierba);
            Mat(list, "musgo_cueva", "Musgo de cueva", 2, 0.05f, T.Hierba);
            Mat(list, "belladona", "Belladona", 3, 0.05f, T.Hierba);
            Mat(list, "mandragora", "Mandrágora", 4, 0.2f, T.Hierba);
            Mat(list, "pescado", "Pescado", 1, 0.5f, T.Comida);
            Mat(list, "trigo", "Trigo", 1, 0.2f, T.Comida);
            Mat(list, "manzana", "Manzana", 1, 0.2f, T.Comida);
            Mat(list, "harina", "Harina", 2, 0.5f, T.Comida);
            Mat(list, "sal", "Sal", 2, 0.2f, T.Comida | T.Mineral);
            Mat(list, "miel", "Miel", 2, 0.3f, T.Comida);
            Mat(list, "manteca", "Manteca", 2, 0.3f, T.Comida);
            Mat(list, "especias_sur", "Especias del sur", 3, 0.1f, T.Comida);
            Mat(list, "trufa_negra", "Trufa negra", 4, 0.1f, T.Comida);
        }
    }
}
