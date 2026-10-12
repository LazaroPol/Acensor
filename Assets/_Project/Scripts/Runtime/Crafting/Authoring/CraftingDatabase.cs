using System.Collections.Generic;
using System.Linq;
using Specimen.Core.Crafting.Authoring;
using UnityEngine;

namespace Specimen.Runtime.Crafting.Authoring
{
    /// <summary>
    /// Punto de entrada único de los datos de oficios en Unity: lista todos los assets y los compila a un
    /// <see cref="ICraftingCatalog"/> del núcleo (las recetas resuelven sus ítems por id).
    /// </summary>
    [CreateAssetMenu(menuName = "SPECIMEN/Oficios/Base de datos", fileName = "CraftingDatabase")]
    public sealed class CraftingDatabase : ScriptableObject
    {
        public List<ItemDefinitionAsset> items = new List<ItemDefinitionAsset>();
        public List<RecipeDefinitionAsset> recipes = new List<RecipeDefinitionAsset>();
        public List<KnowledgeAsset> knowledge = new List<KnowledgeAsset>();
        public List<StationPartAsset> parts = new List<StationPartAsset>();
        public List<ModDefinitionAsset> mods = new List<ModDefinitionAsset>();
        public ProfessionCurveAsset curve;
        public QualityRulesAsset quality;

        /// <summary>Vuelca los assets (ignorando huecos vacíos) a datos del núcleo.</summary>
        public CraftingDataSet ToDataSet()
        {
            var set = new CraftingDataSet
            {
                Curve = curve != null ? curve.Data : new CurveData(),
                Quality = quality != null ? quality.Data : new QualityData()
            };
            set.Items.AddRange(DataOf(items));
            set.Recipes.AddRange(DataOf(recipes));
            set.Knowledge.AddRange(DataOf(knowledge));
            set.Parts.AddRange(DataOf(parts));
            set.Mods.AddRange(DataOf(mods));
            return set;
        }

        public IReadOnlyList<DataIssue> Validate() => CraftingDataValidator.Validate(ToDataSet());

        public ICraftingCatalog Build() => new CraftingCatalog(ToDataSet());

        private static IEnumerable<T> DataOf<T>(IEnumerable<CraftingAsset<T>> assets) where T : class, new() =>
            (assets ?? Enumerable.Empty<CraftingAsset<T>>()).Where(a => a != null).Select(a => a.Data);
    }
}
