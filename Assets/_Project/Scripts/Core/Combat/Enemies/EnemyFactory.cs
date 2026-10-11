using Specimen.Core.Common;

namespace Specimen.Core.Combat.Enemies
{
    /// <summary>Crea enemigos aplicando la estrategia de escalado inyectada al arquetipo.</summary>
    public sealed class EnemyFactory
    {
        private readonly IDepthScaling _scaling;

        public EnemyFactory(IDepthScaling scaling)
        {
            _scaling = Guard.NotNull(scaling, nameof(scaling));
        }

        public Enemy Create(EnemyArchetype archetype, EnemyDepth depth)
        {
            Guard.NotNull(archetype, nameof(archetype));
            return new Enemy(archetype, depth, _scaling.Scale(archetype.BaseStats, depth));
        }
    }
}
