using System;
using System.Collections.Generic;
using Specimen.Core.Common;
using Specimen.Core.Loot;

namespace Specimen.Core.Floors.Remnants
{
    /// <summary>Probabilidades de restos y fragmentos por anillo (1-3). No pueden bajar al profundizar.</summary>
    public sealed class RemnantRules
    {
        private readonly RingRemnantOdds[] _odds;

        public RemnantRules(IReadOnlyList<RingRemnantOdds> odds)
        {
            Guard.NotNull(odds, nameof(odds));
            if (odds.Count != DepthRings.Max)
                throw new ArgumentException($"Hacen falta {DepthRings.Max} probabilidades, una por anillo.", nameof(odds));
            _odds = new RingRemnantOdds[odds.Count];
            for (int i = 0; i < odds.Count; i++)
            {
                _odds[i] = odds[i] ?? throw new ArgumentException("Probabilidad nula.", nameof(odds));
                if (i > 0 && (_odds[i].RemnantChance < _odds[i - 1].RemnantChance
                              || _odds[i].FragmentChance < _odds[i - 1].FragmentChance))
                    throw new ArgumentException("Las probabilidades no pueden bajar en anillos más profundos.", nameof(odds));
            }
        }

        /// <summary>Restos: 20/35/50 %; fragmentos: 5/10/20 % por sala de saqueo.</summary>
        public static RemnantRules Default { get; } = new RemnantRules(new[]
        {
            new RingRemnantOdds(0.20f, 0.05f),
            new RingRemnantOdds(0.35f, 0.10f),
            new RingRemnantOdds(0.50f, 0.20f)
        });

        public RingRemnantOdds For(int ring) => _odds[DepthRings.Validate(ring, nameof(ring)) - DepthRings.Min];
    }
}
