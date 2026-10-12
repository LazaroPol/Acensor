using System.Linq;
using Specimen.Core.Crafting.Content;
using Specimen.Core.Crafting.Mods;
using Specimen.Core.Crafting.Stations;
using Specimen.Core.Items.Catalog;
using Specimen.Core.Professions;

namespace Specimen.Core.Crafting.Authoring
{
    /// <summary>
    /// Vuelca el catálogo C# por defecto (OF-11/OF-12) a un <see cref="CraftingDataSet"/>: es lo que usa
    /// el menú «Generar assets por defecto» para no teclear las recetas a mano.
    /// </summary>
    public static class DefaultCraftingData
    {
        public static CraftingDataSet Create()
        {
            var set = new CraftingDataSet
            {
                Curve = CurveAssetMapper.FromCurve(ProfessionCurve.Default, ProfessionMilestones.Default),
                Quality = QualityAssetMapper.FromRules(QualityRules.Default)
            };
            var partIds = DefaultStationParts.All.Select(p => p.Id);
            var modIds = DefaultMods.All.Select(m => m.Id);
            var reserved = partIds.Concat(modIds).ToHashSet();
            set.Items.AddRange(ItemCatalog.All.Where(i => !reserved.Contains(i.Id))
                .OrderBy(i => i.Id).Select(ItemAssetMapper.FromDefinition));
            set.Parts.AddRange(DefaultStationParts.All.Select(StationPartAssetMapper.FromEntry));
            set.Mods.AddRange(DefaultMods.All.Select(ModAssetMapper.FromDefinition));
            set.Recipes.AddRange(DefaultRecipeCatalog.CreateAll().Select(RecipeAssetMapper.FromDefinition));
            set.Knowledge.AddRange(DefaultKnowledge.All.Select(KnowledgeAssetMapper.FromDefinition));
            return set;
        }
    }
}
