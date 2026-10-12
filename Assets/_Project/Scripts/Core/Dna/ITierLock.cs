namespace Specimen.Core.Dna
{
    /// <summary>Puntos que hay que haber invertido en una rama para acceder a un nivel.</summary>
    public interface ITierLock
    {
        int RequiredPoints(int tier);
    }
}
