using System.Collections.Generic;
using T = Specimen.Core.Items.MaterialTags;

namespace Specimen.Core.Items.Catalog
{
    /// <summary>Materiales de §17.5: metal, mineral y vidrio (reutiliza hierro, lingote, chatarra y cristal).</summary>
    public static partial class ItemCatalog
    {
        private static void AddMineralMaterials(List<ItemDefinition> list)
        {
            Mat(list, "mineral_cobre", "Mineral de cobre", 1, 1.5f, T.Metal | T.Mineral);
            Mat(list, "lingote_cobre", "Lingote de cobre", 2, 1f, T.Metal);
            Mat(list, "lingote_acero", "Lingote de acero", 3, 1f, T.Metal);
            Mat(list, "lingote_plata", "Lingote de plata", 3, 1f, T.Metal);
            Mat(list, "acero_crisol", "Acero de crisol", 4, 1f, T.Metal);
            Mat(list, "arcilla", "Arcilla", 1, 1f, T.Mineral);
            Mat(list, "barro", "Barro", 1, 1f, T.Mineral);
            Mat(list, "azufre", "Azufre", 2, 0.3f, T.Mineral);
            Mat(list, "salitre", "Salitre", 2, 0.3f, T.Mineral);
            Mat(list, "polvo_estrella", "Polvo de estrella", 4, 0.1f, T.Mineral);
            Mat(list, "arena_fina", "Arena fina", 2, 1f, T.Vidrio | T.Mineral);
            Mat(list, "vidrio_soplado", "Vidrio soplado", 3, 0.5f, T.Vidrio);
            Mat(list, "vidrio_cuarzo", "Vidrio de cuarzo", 4, 0.5f, T.Vidrio);
        }
    }
}
