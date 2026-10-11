namespace Specimen.Core.Iteration.Ending
{
    /// <summary>Cómo terminó la iteración para el grupo.</summary>
    public enum IterationOutcome
    {
        Victory,
        Wiped,
        Abandoned
    }

    /// <summary>Qué le pasó a cada clon al terminar.</summary>
    public enum PlayerFate
    {
        Extracted,
        LeftBehind,
        Eliminated,
        Abandoned
    }
}
