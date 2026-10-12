using Specimen.Core.Crafting.Knowledge;
using Specimen.Core.Items;
using Specimen.Core.Professions;

namespace Specimen.Tests.Core.Crafting.Knowledge
{
    internal static class KnowledgeFixtures
    {
        public static ProfessionSkill Skill(ProfessionId id, int level)
        {
            var skill = new ProfessionSkill(id);
            while (skill.Level < level) skill.Gain(skill.Curve.XpToNext(skill.Level));
            return skill;
        }

        public static KnowledgeDefinition Scroll(string id, string recipe, int minLevel = 1) =>
            new KnowledgeDefinition(id, KnowledgeType.Scroll, Rarity.Common, ProfessionId.Herrero,
                minLevel, new[] { recipe });

        public static KnowledgeDefinition Treatise(string id, params string[] recipes) =>
            new KnowledgeDefinition(id, KnowledgeType.Treatise, Rarity.Rare, ProfessionId.Herrero, 10, recipes);
    }
}
