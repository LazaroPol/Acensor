namespace Specimen.Core.Combat.Enemies
{
    /// <summary>Estrategia que ajusta las estadísticas base de un enemigo según su profundidad.</summary>
    public interface IDepthScaling
    {
        EnemyStats Scale(EnemyStats baseStats, EnemyDepth depth);
    }
}
