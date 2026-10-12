using System.Collections.Generic;
using System.Linq;
using Specimen.Core.Crafting.Knowledge;
using Specimen.Core.Items;
using Specimen.Core.Professions;

namespace Specimen.Core.Crafting.Content
{
    /// <summary>
    /// Batería de conocimientos de §17.6: pergaminos, planos, tratados y los cinco volúmenes de cada libro de
    /// oficio. Ids estables: «pergamino.*», «plano.*», «tratado.*» y «libro.*.volN».
    /// </summary>
    public static partial class DefaultKnowledge
    {
        public const int ScrollLevel = 5;
        public const int BlueprintLevel = 10;
        public const int TreatiseLevel = 10;
        public const int BookLevel = 15;

        public static IReadOnlyList<KnowledgeDefinition> All { get; } = Build();

        public static bool TryGet(string id, out KnowledgeDefinition knowledge)
        {
            knowledge = All.FirstOrDefault(k => k.Id == id);
            return knowledge != null;
        }

        private static IReadOnlyList<KnowledgeDefinition> Build()
        {
            var list = new List<KnowledgeDefinition>();
            AddScrolls(list);
            AddBlueprints(list);
            AddTreatises(list);
            AddBooks(list);
            return list.AsReadOnly();
        }

        private static void Scroll(List<KnowledgeDefinition> l, ProfessionId p, string slug, string recipe) =>
            l.Add(new KnowledgeDefinition("pergamino." + slug, KnowledgeType.Scroll, Rarity.Common, p, ScrollLevel,
                new[] { Recipe(p, recipe) }));

        private static void Plan(List<KnowledgeDefinition> l, ProfessionId p, string slug, params string[] recipes) =>
            l.Add(new KnowledgeDefinition("plano." + slug, KnowledgeType.Blueprint, Rarity.Uncommon, p,
                BlueprintLevel, recipes.Select(r => Recipe(p, r))));

        private static void Treatise(List<KnowledgeDefinition> l, ProfessionId p, string slug, params string[] recipes) =>
            l.Add(new KnowledgeDefinition("tratado." + slug, KnowledgeType.Treatise, Rarity.Rare, p, TreatiseLevel,
                recipes.Select(r => Recipe(p, r))));

        private static void Book(List<KnowledgeDefinition> l, ProfessionId p, string slug)
        {
            for (var v = 1; v <= KnowledgeDefinition.BookVolumes; v++)
                l.Add(KnowledgeDefinition.BookVolume(RecipeLadder.BookPrefix + slug, v, p, BookLevel));
        }

        private static string Recipe(ProfessionId p, string slug) => $"{p.ToString().ToLowerInvariant()}.{slug}";
    }
}
