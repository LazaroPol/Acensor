using System.Collections.Generic;

namespace Specimen.Core.Crafting.Authoring
{
    /// <summary>
    /// Todos los datos de oficios en forma serializable: lo que guarda la <c>CraftingDatabase</c> de Unity
    /// y lo que genera <see cref="DefaultCraftingData"/> desde el catálogo C#.
    /// </summary>
    public sealed class CraftingDataSet
    {
        public List<ItemData> Items { get; } = new List<ItemData>();
        public List<RecipeData> Recipes { get; } = new List<RecipeData>();
        public List<KnowledgeData> Knowledge { get; } = new List<KnowledgeData>();
        public List<StationPartData> Parts { get; } = new List<StationPartData>();
        public List<ModData> Mods { get; } = new List<ModData>();
        public CurveData Curve { get; set; } = new CurveData();
        public QualityData Quality { get; set; } = new QualityData();
    }
}
