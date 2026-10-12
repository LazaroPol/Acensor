using System.Collections.Generic;
using System.Linq;

namespace Specimen.Core.Crafting.Authoring
{
    public static partial class CraftingDataValidator
    {
        private static void CheckReferences(CraftingDataSet data, List<DataIssue> issues)
        {
            var items = new HashSet<string>(data.Items.Select(i => i.id)
                .Concat(data.Parts.Select(p => p.id)).Concat(data.Mods.Select(m => m.id)).Where(Has));
            var parts = new HashSet<string>(data.Parts.Select(p => p.id).Where(Has));
            var recipes = new HashSet<string>(data.Recipes.Select(r => r.id).Where(Has));
            foreach (var r in data.Recipes) CheckRecipe(r, items, parts, recipes, issues);
            foreach (var k in data.Knowledge)
                foreach (var taught in (k.teaches ?? new string[0]).Where(t => !recipes.Contains(t ?? string.Empty)))
                    issues.Add(new DataIssue(KnowledgeKind, k.id, $"enseña una receta inexistente: {taught}"));
        }

        private static void CheckRecipe(RecipeData r, HashSet<string> items, HashSet<string> parts,
            HashSet<string> recipes, List<DataIssue> issues)
        {
            var inputs = (r.inputs ?? new IngredientData[0]).Where(i => Has(i.itemId)).Select(i => i.itemId);
            var outputs = (r.outputs ?? new OutputData[0]).Select(o => o.itemId);
            foreach (var id in inputs.Concat(outputs).Where(id => !items.Contains(id ?? string.Empty)))
                issues.Add(new DataIssue(RecipeKind, r.id, $"pide un ítem inexistente: {id}"));
            foreach (var id in (r.requiredParts ?? new string[0]).Where(p => !parts.Contains(p ?? string.Empty)))
                issues.Add(new DataIssue(RecipeKind, r.id, $"exige una pieza inexistente: {id}"));
            if (Has(r.improves) && !recipes.Contains(r.improves))
                issues.Add(new DataIssue(RecipeKind, r.id, $"mejora una receta inexistente: {r.improves}"));
            if ((r.outputs ?? new OutputData[0]).Length == 0)
                issues.Add(new DataIssue(RecipeKind, r.id, "no produce nada"));
        }

        private static void CheckCurve(CurveData curve, List<DataIssue> issues)
        {
            var xp = curve?.xpToNext ?? new int[0];
            if (xp.Length == 0 || xp[0] <= 0)
                issues.Add(new DataIssue(CurveKind, "xp", "la curva está vacía o empieza sin XP positiva"));
            for (int i = 1; i < xp.Length; i++)
                if (xp[i] <= xp[i - 1])
                    issues.Add(new DataIssue(CurveKind, "xp", $"no creciente en el nivel {i + 1} ({xp[i - 1]} → {xp[i]})"));
            var steps = curve?.milestones ?? new MilestoneData[0];
            if (steps.Length == 0 || steps[0].level != 1)
                issues.Add(new DataIssue(CurveKind, "hitos", "los hitos deben empezar en el nivel 1"));
            for (int i = 1; i < steps.Length; i++)
                if (steps[i].level <= steps[i - 1].level)
                    issues.Add(new DataIssue(CurveKind, "hitos", $"nivel de hito no creciente: {steps[i].level}"));
        }

        private static void CheckQuality(QualityData quality, List<DataIssue> issues)
        {
            var grades = (quality?.grades ?? new GradeData[0]).OrderBy(g => g.quality).ToArray();
            if (grades.Length != 6 || grades.Select(g => g.quality).Distinct().Count() != 6)
                issues.Add(new DataIssue(QualityKind, "grados", "se esperan los seis grados sin repetir"));
            for (int i = 1; i < grades.Length; i++)
                if (grades[i].minScore <= grades[i - 1].minScore)
                    issues.Add(new DataIssue(QualityKind, grades[i].quality.ToString(), "umbral no creciente"));
        }

        private static bool Has(string id) => !string.IsNullOrWhiteSpace(id);
    }
}
