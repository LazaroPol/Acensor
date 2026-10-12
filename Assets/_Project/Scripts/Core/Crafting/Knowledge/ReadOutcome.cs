namespace Specimen.Core.Crafting.Knowledge
{
    /// <summary>Resultado de leer un conocimiento. Solo <see cref="Learned"/> consume el objeto.</summary>
    public enum ReadOutcome
    {
        Learned,
        AlreadyKnown,
        TooLowLevel,
        WrongProfession
    }
}
