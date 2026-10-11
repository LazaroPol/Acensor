namespace Specimen.Core.Time
{
    /// <summary>Fuente de tiempo de juego en segundos. Abstrae UnityEngine.Time para poder testear.</summary>
    public interface IClock
    {
        double Now { get; }
    }
}
