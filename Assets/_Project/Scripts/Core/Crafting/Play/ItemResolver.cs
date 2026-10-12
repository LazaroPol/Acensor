using System.Collections.Generic;
using Specimen.Core.Common;
using Specimen.Core.Crafting.Authoring;
using Specimen.Core.Items;
using Specimen.Core.Items.Catalog;

namespace Specimen.Core.Crafting.Play
{
    /// <summary>
    /// Encuentra la definición de un id guardado: catálogo de oficios, conocimientos, mods, piezas de
    /// estación y, por último, el catálogo general de objetos. Null si nadie lo conoce.
    /// </summary>
    public sealed class ItemResolver
    {
        private readonly Dictionary<string, ItemDefinition> _extra = new Dictionary<string, ItemDefinition>();
        private readonly ICraftingCatalog _catalog;

        public ItemResolver(ICraftingCatalog catalog)
        {
            _catalog = Guard.NotNull(catalog, nameof(catalog));
            foreach (var k in catalog.Knowledge) _extra[k.Id] = KnowledgeItems.For(k);
            foreach (var m in catalog.Mods) if (m.Item != null) _extra[m.Item.Id] = m.Item;
            foreach (var p in catalog.Parts) if (p.Item != null) _extra[p.Item.Id] = p.Item;
        }

        public ItemDefinition Find(string id)
        {
            if (string.IsNullOrEmpty(id)) return null;
            if (_catalog.TryGetItem(id, out var item)) return item;
            if (_extra.TryGetValue(id, out item)) return item;
            return ItemCatalog.TryGet(id, out item) ? item : null;
        }
    }
}
