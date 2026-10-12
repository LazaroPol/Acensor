namespace Specimen.Core.Crafting.Definitions
{
    /// <summary>Receta de partida del oficio: siempre conocida.</summary>
    public sealed class AlwaysKnownCondition : UnlockCondition
    {
        internal AlwaysKnownCondition() { }

        public override bool IsMet(IRecipeKnowledge knowledge) => true;

        public override string ToString() => "Siempre conocida";
    }
}
