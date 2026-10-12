using System;
using Specimen.Core.Crafting.Stations;
using Specimen.Core.Items;
using Specimen.Core.Items.Catalog;

namespace Specimen.Core.Crafting.Content
{
    /// <summary>
    /// Catálogo que ven las recetas de contenido: los objetos de <see cref="ItemCatalog"/> y las piezas
    /// de estación de <see cref="DefaultStationParts"/> (algunas recetas fabrican piezas).
    /// </summary>
    public static class ContentItems
    {
        public static bool TryGet(string id, out ItemDefinition item)
        {
            if (ItemCatalog.TryGet(id, out item)) return true;
            item = DefaultStationParts.TryGet(id, out var part) ? part.Item : null;
            return item != null;
        }

        public static ItemDefinition Get(string id) =>
            TryGet(id, out var item) ? item : throw new ArgumentException($"Objeto desconocido: {id}", nameof(id));
    }
}
