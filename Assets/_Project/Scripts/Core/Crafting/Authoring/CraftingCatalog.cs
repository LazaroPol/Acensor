using System;
using System.Collections.Generic;
using System.Linq;
using Specimen.Core.Common;
using Specimen.Core.Crafting.Definitions;
using Specimen.Core.Crafting.Knowledge;
using Specimen.Core.Crafting.Mods;
using Specimen.Core.Crafting.Stations;
using Specimen.Core.Items;
using Specimen.Core.Professions;

namespace Specimen.Core.Crafting.Authoring
{
    /// <summary>
    /// <see cref="ICraftingCatalog"/> compilado desde un <see cref="CraftingDataSet"/>. El espacio de ítems
    /// incluye los objetos, las piezas de estación y los mods (todos se pueden fabricar o llevar).
    /// </summary>
    public sealed class CraftingCatalog : ICraftingCatalog
    {
        private readonly Dictionary<string, ItemDefinition> _items = new Dictionary<string, ItemDefinition>();

        public CraftingCatalog(CraftingDataSet data)
        {
            Guard.NotNull(data, nameof(data));
            Parts = data.Parts.Select(StationPartAssetMapper.ToEntry).ToList().AsReadOnly();
            Mods = data.Mods.Select(ModAssetMapper.ToDefinition).ToList().AsReadOnly();
            foreach (var item in data.Items.Select(ItemAssetMapper.ToDefinition)
                         .Concat(Parts.Select(p => p.Item)).Concat(Mods.Select(m => m.Item)))
                AddItem(item);
            Items = _items.Values.ToList().AsReadOnly();
            Recipes = new RecipeCatalog(data.Recipes.Select(r => RecipeAssetMapper.ToDefinition(r, GetItem)).ToList());
            Knowledge = data.Knowledge.Select(KnowledgeAssetMapper.ToDefinition).ToList().AsReadOnly();
            Curve = CurveAssetMapper.ToCurve(data.Curve);
            Milestones = CurveAssetMapper.ToMilestones(data.Curve);
            Quality = QualityAssetMapper.ToRules(data.Quality);
        }

        public IReadOnlyList<ItemDefinition> Items { get; }
        public RecipeCatalog Recipes { get; }
        public IReadOnlyList<KnowledgeDefinition> Knowledge { get; }
        public IReadOnlyList<StationPartEntry> Parts { get; }
        public IReadOnlyList<ModDefinition> Mods { get; }
        public ProfessionCurve Curve { get; }
        public ProfessionMilestones Milestones { get; }
        public QualityRules Quality { get; }

        public bool TryGetItem(string id, out ItemDefinition item) =>
            _items.TryGetValue(id ?? string.Empty, out item);

        private ItemDefinition GetItem(string id) =>
            TryGetItem(id, out var item) ? item : throw new ArgumentException($"Objeto desconocido: {id}", nameof(id));

        private void AddItem(ItemDefinition item)
        {
            if (_items.ContainsKey(item.Id)) throw new ArgumentException($"Id de objeto repetido: {item.Id}");
            _items.Add(item.Id, item);
        }
    }
}
