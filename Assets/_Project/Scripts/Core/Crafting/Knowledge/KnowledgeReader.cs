using Specimen.Core.Common;
using Specimen.Core.Professions;

namespace Specimen.Core.Crafting.Knowledge
{
    /// <summary>
    /// Lee un conocimiento con el oficio del jugador (GDD §17.3). Solo <see cref="ReadOutcome.Learned"/>
    /// registra lo aprendido y consume el objeto; el resto deja el objeto intacto.
    /// </summary>
    public static class KnowledgeReader
    {
        public static ReadResult Read(KnowledgeDefinition item, ProfessionSkill skill, KnownRecipes known)
        {
            Guard.NotNull(item, nameof(item));
            Guard.NotNull(skill, nameof(skill));
            Guard.NotNull(known, nameof(known));
            if (IsAlreadyKnown(item, known)) return new ReadResult(item, ReadOutcome.AlreadyKnown);
            if (skill.Profession != item.Profession) return new ReadResult(item, ReadOutcome.WrongProfession);
            if (skill.Level < item.MinLevel) return new ReadResult(item, ReadOutcome.TooLowLevel);
            known.Register(item);
            return new ReadResult(item, ReadOutcome.Learned);
        }

        private static bool IsAlreadyKnown(KnowledgeDefinition item, KnownRecipes known)
        {
            if (known.Knows(item.Id)) return true;
            if (item.IsVolume || item.Teaches.Count == 0) return false;
            foreach (var recipe in item.Teaches)
                if (!known.KnowsRecipe(recipe)) return false;
            return true;
        }
    }
}
