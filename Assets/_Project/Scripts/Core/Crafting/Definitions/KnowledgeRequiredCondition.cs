using Specimen.Core.Common;

namespace Specimen.Core.Crafting.Definitions
{
    /// <summary>Hay que haber leído un pergamino, plano o tratado concreto.</summary>
    public sealed class KnowledgeRequiredCondition : UnlockCondition
    {
        public string KnowledgeId { get; }

        internal KnowledgeRequiredCondition(string knowledgeId) =>
            KnowledgeId = RequireId(knowledgeId, nameof(knowledgeId));

        public override bool IsMet(IRecipeKnowledge knowledge) =>
            Guard.NotNull(knowledge, nameof(knowledge)).Knows(KnowledgeId);

        public override string ToString() => $"Conocimiento {KnowledgeId}";
    }
}
