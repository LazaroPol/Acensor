using System.Collections.Generic;
using T = Specimen.Core.Items.MaterialTags;

namespace Specimen.Core.Items.Catalog
{
    /// <summary>Intermedios sin calidad (un oficio los fabrica para otro) y combustibles de §17.5.</summary>
    public static partial class ItemCatalog
    {
        private static void AddCraftedMaterials(List<ItemDefinition> list)
        {
            Mat(list, "cuerda", "Cuerda", 1, 0.2f, T.Tela);
            Mat(list, "correa_cuero", "Correa de cuero", 2, 0.2f, T.Cuero);
            Mat(list, "clavos", "Clavos", 1, 0.1f, T.Metal);
            Mat(list, "remaches", "Remaches", 2, 0.1f, T.Metal);
            Mat(list, "mango_madera", "Mango de madera", 1, 0.5f, T.Madera);
            Mat(list, "resina", "Resina", 2, 0.2f, T.Hierba);
            Mat(list, "aceite_afilar", "Aceite de afilar", 2, 0.3f, T.Mineral);
            Mat(list, "mortero_cal", "Mortero de cal", 2, 2f, T.Piedra | T.Mineral);
            Mat(list, "engranaje_madera", "Engranaje de madera", 2, 0.4f, T.Madera);
            Mat(list, FuelIds.Lena, "Leña", 1, 1f, T.Madera | T.Combustible);
            Mat(list, FuelIds.CarbonVegetal, "Carbón vegetal", 2, 0.5f, T.Mineral | T.Combustible);
            Mat(list, FuelIds.Hulla, "Hulla", 3, 1f, T.Mineral | T.Combustible);
            Mat(list, FuelIds.AceiteLampara, "Aceite de lámpara", 2, 0.5f, T.Combustible);
        }
    }
}
