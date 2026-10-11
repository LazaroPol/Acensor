namespace Specimen.Core.Crafting
{
    /// <summary>Resultado de un intento de fabricación.</summary>
    public enum CraftResult
    {
        Crafted,
        UnknownRecipe,
        WrongProfession,
        MissingIngredients,
        NoSpace
    }
}
