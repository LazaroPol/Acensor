using System.Collections.Generic;
using Specimen.Core.Common;
using Specimen.Core.Stats;

namespace Specimen.Core.Vitals
{
    /// <summary>
    /// Vida, recurso de clase y stamina de un personaje, con sus máximos ligados a la <see cref="StatSheet"/>.
    /// </summary>
    public sealed class CharacterVitals
    {
        private static readonly Dictionary<ResourceKind, StatType> MaxStatOf = new Dictionary<ResourceKind, StatType>
        {
            { ResourceKind.Health, StatType.MaxHealth },
            { ResourceKind.ClassResource, StatType.MaxClassResource },
            { ResourceKind.Stamina, StatType.MaxStamina }
        };

        private readonly Dictionary<ResourceKind, ResourcePool> _pools = new Dictionary<ResourceKind, ResourcePool>();

        public CharacterVitals(StatSheet stats)
        {
            Guard.NotNull(stats, nameof(stats));
            foreach (var pair in MaxStatOf) _pools.Add(pair.Key, new ResourcePool(stats.Get(pair.Value)));
        }

        public ResourcePool Health => _pools[ResourceKind.Health];
        public ResourcePool ClassResource => _pools[ResourceKind.ClassResource];
        public ResourcePool Stamina => _pools[ResourceKind.Stamina];

        public bool IsAlive => !Health.IsEmpty;

        public ResourcePool Get(ResourceKind kind) => _pools[kind];

        public static StatType MaxStatFor(ResourceKind kind) => MaxStatOf[kind];

        /// <summary>Rellena todos los recursos (inicio de partida, revivir completo).</summary>
        public void RefillAll()
        {
            foreach (var pool in _pools.Values) pool.Fill();
        }
    }
}
