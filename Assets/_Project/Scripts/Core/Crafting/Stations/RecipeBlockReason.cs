namespace Specimen.Core.Crafting.Stations
{
    /// <summary>Por qué una receta visible en la estación no se puede fabricar (la UI lo traduce).</summary>
    public enum RecipeBlockReason
    {
        None = 0,
        ProfessionLevel = 1,
        MissingMaterials = 2
    }
}
