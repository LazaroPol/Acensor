using System.Collections.Generic;
using Specimen.Core.Crafting.Definitions;
using Specimen.Core.Items;
using Specimen.Core.Items.Catalog;

namespace Specimen.Core.Crafting.Queue
{
    /// <summary>Combustibles por defecto de §17.5, derivados del catálogo de objetos.</summary>
    public sealed partial class FuelTable
    {
        public static FuelTable Default { get; } = BuildDefault();

        private static FuelTable BuildDefault()
        {
            var burn = new Dictionary<string, float>
            {
                [ItemIds.Madera] = Lena,
                [FuelIds.Lena] = Lena,
                [FuelIds.CarbonVegetal] = Carbon,
                [FuelIds.Hulla] = Hulla,
                [FuelIds.AceiteLampara] = AceiteLampara
            };
            foreach (var item in ItemCatalog.All)
                if (item.Tier == 1 && item.HasTag(MaterialTags.Madera) && !burn.ContainsKey(item.Id))
                    burn.Add(item.Id, MaderaTier1);
            var onlyIn = new[] { new KeyValuePair<string, StationType>(FuelIds.AceiteLampara, StationType.MesaAlquimia) };
            return new FuelTable(burn, onlyIn);
        }
    }
}
