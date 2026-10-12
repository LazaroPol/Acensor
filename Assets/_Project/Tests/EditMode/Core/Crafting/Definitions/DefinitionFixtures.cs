using System.Collections.Generic;
using Specimen.Core.Crafting;
using Specimen.Core.Crafting.Definitions;
using Specimen.Core.Professions;
using static Specimen.Tests.Core.Crafting.CraftingFixtures;

namespace Specimen.Tests.Core.Crafting.Definitions
{
    internal sealed class FakeKnowledge : IRecipeKnowledge
    {
        public readonly HashSet<string> Known = new HashSet<string>();
        public readonly HashSet<string> Completed = new HashSet<string>();

        public bool Knows(string knowledgeId) => Known.Contains(knowledgeId);
        public bool HasCompleted(string collectionId) => Completed.Contains(collectionId);
    }

    internal static class DefinitionFixtures
    {
        public static RecipeDefinition Make(string id, ProfessionId profession = ProfessionId.Herrero,
            StationType station = StationType.Fragua, UnlockCondition unlock = null, string improves = null) =>
            new RecipeDefinition(id, profession, station,
                new[] { IngredientSpec.ForItem(Ore, 3) }, new[] { new RecipeOutput(Sword) },
                unlock: unlock, improves: improves);
    }
}
