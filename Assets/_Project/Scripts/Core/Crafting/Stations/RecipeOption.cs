using Specimen.Core.Common;
using Specimen.Core.Crafting.Definitions;

namespace Specimen.Core.Crafting.Stations
{
    /// <summary>Receta que ofrece una estación y, si está en gris, el motivo.</summary>
    public sealed class RecipeOption
    {
        public RecipeDefinition Recipe { get; }
        public RecipeBlockReason Block { get; }

        public RecipeOption(RecipeDefinition recipe, RecipeBlockReason block)
        {
            Recipe = Guard.NotNull(recipe, nameof(recipe));
            Block = block;
        }

        public bool IsCraftable => Block == RecipeBlockReason.None;

        public override string ToString() => IsCraftable ? Recipe.Id : $"{Recipe.Id} [{Block}]";
    }
}
