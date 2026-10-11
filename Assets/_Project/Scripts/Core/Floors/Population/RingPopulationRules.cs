using System;
using System.Collections.Generic;
using Specimen.Core.Common;
using Specimen.Core.Loot;

namespace Specimen.Core.Floors.Population
{
    /// <summary>Cuotas de población por anillo de profundidad (1-3). La amenaza no puede bajar al profundizar.</summary>
    public sealed class RingPopulationRules
    {
        private readonly RingQuota[] _quotas;

        public RingPopulationRules(IReadOnlyList<RingQuota> quotas)
        {
            Guard.NotNull(quotas, nameof(quotas));
            if (quotas.Count != DepthRings.Max)
                throw new ArgumentException($"Hacen falta {DepthRings.Max} cuotas, una por anillo.", nameof(quotas));
            _quotas = new RingQuota[quotas.Count];
            for (int i = 0; i < quotas.Count; i++)
            {
                _quotas[i] = quotas[i] ?? throw new ArgumentException("Cuota nula.", nameof(quotas));
                if (i > 0 && _quotas[i].Threat < _quotas[i - 1].Threat)
                    throw new ArgumentException("La amenaza no puede bajar en anillos más profundos.", nameof(quotas));
            }
        }

        /// <summary>Anillo 1: 1-2 loot, 0-1 enemigos; anillo 2: 2-3, 1-2; anillo 3: 3-4, 2-4.</summary>
        public static RingPopulationRules Default { get; } = new RingPopulationRules(new[]
        {
            new RingQuota(1, 2, 0, 1, 1),
            new RingQuota(2, 3, 1, 2, 2),
            new RingQuota(3, 4, 2, 4, 3)
        });

        public RingQuota For(int ring) => _quotas[DepthRings.Validate(ring, nameof(ring)) - DepthRings.Min];
    }
}
