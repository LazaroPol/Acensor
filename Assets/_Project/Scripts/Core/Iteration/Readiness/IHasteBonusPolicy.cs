namespace Specimen.Core.Iteration.Readiness
{
    /// <summary>Calcula el bonus Prisa (fracción extra de loot del piso) según el tiempo de Calma ahorrado.</summary>
    public interface IHasteBonusPolicy
    {
        float BonusFor(float savedSeconds, float calmSeconds);
    }
}
