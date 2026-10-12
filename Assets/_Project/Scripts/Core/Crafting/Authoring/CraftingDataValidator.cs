using System.Collections.Generic;
using System.Linq;
using Specimen.Core.Common;

namespace Specimen.Core.Crafting.Authoring
{
    /// <summary>
    /// Valida un <see cref="CraftingDataSet"/> antes de usarlo: ids vacíos o repetidos, referencias rotas
    /// (ítems, piezas, recetas, conocimientos) y curvas/umbrales no crecientes. No lanza: devuelve la lista.
    /// </summary>
    public static partial class CraftingDataValidator
    {
        public const string ItemKind = "Ítem";
        public const string RecipeKind = "Receta";
        public const string KnowledgeKind = "Conocimiento";
        public const string PartKind = "Pieza";
        public const string ModKind = "Mod";
        public const string CurveKind = "Curva";
        public const string QualityKind = "Calidad";

        public static IReadOnlyList<DataIssue> Validate(CraftingDataSet data)
        {
            Guard.NotNull(data, nameof(data));
            var issues = new List<DataIssue>();
            CheckIds(data, issues);
            CheckReferences(data, issues);
            CheckCurve(data.Curve, issues);
            CheckQuality(data.Quality, issues);
            return issues.AsReadOnly();
        }

        private static void CheckIds(CraftingDataSet data, List<DataIssue> issues)
        {
            var itemSpace = data.Items.Select(i => (ItemKind, i.id))
                .Concat(data.Parts.Select(p => (PartKind, p.id)))
                .Concat(data.Mods.Select(m => (ModKind, m.id)));
            CheckUnique(itemSpace, issues);
            CheckUnique(data.Recipes.Select(r => (RecipeKind, r.id)), issues);
            CheckUnique(data.Knowledge.Select(k => (KnowledgeKind, k.id)), issues);
        }

        private static void CheckUnique(IEnumerable<(string kind, string id)> entries, List<DataIssue> issues)
        {
            var seen = new HashSet<string>();
            foreach (var (kind, id) in entries)
            {
                if (string.IsNullOrWhiteSpace(id)) issues.Add(new DataIssue(kind, id, "id vacío"));
                else if (!seen.Add(id)) issues.Add(new DataIssue(kind, id, "id duplicado"));
            }
        }
    }
}
