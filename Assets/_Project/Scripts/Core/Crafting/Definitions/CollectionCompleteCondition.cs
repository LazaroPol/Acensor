using Specimen.Core.Common;

namespace Specimen.Core.Crafting.Definitions
{
    /// <summary>Todos los volúmenes de un libro de oficio leídos.</summary>
    public sealed class CollectionCompleteCondition : UnlockCondition
    {
        public string CollectionId { get; }

        internal CollectionCompleteCondition(string collectionId) =>
            CollectionId = RequireId(collectionId, nameof(collectionId));

        public override bool IsMet(IRecipeKnowledge knowledge) =>
            Guard.NotNull(knowledge, nameof(knowledge)).HasCompleted(CollectionId);

        public override string ToString() => $"Colección {CollectionId}";
    }
}
