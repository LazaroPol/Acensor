namespace Specimen.Core.Combat.Perception
{
    /// <summary>Estrategia que ajusta los sentidos y la persecución del enemigo según el objetivo.</summary>
    public interface IPerceptionModifiers
    {
        float VisionMultiplier(PerceivedTarget target);
        float SpeedMultiplier(PerceivedTarget target);
    }
}
