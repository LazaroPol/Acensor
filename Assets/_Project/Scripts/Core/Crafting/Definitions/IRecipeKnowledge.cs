namespace Specimen.Core.Crafting.Definitions
{
    /// <summary>Lo que el jugador sabe: conocimientos leídos y colecciones completas (OF-07).</summary>
    public interface IRecipeKnowledge
    {
        bool Knows(string knowledgeId);
        bool HasCompleted(string collectionId);
    }
}
