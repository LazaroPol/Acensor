using System;
using Specimen.Core.Common;

namespace Specimen.Core.Combat
{
    /// <summary>Probabilidad de crítico en [0, 1] y multiplicador >= 1 del atacante.</summary>
    public sealed class CritRules
    {
        public const float DefaultMultiplier = 1.5f;

        public CritRules(float chance, float multiplier = DefaultMultiplier)
        {
            if (float.IsNaN(chance) || chance < 0f || chance > 1f)
                throw new ArgumentOutOfRangeException(nameof(chance), chance, "Debe estar en [0, 1].");
            if (float.IsNaN(multiplier) || multiplier < 1f)
                throw new ArgumentOutOfRangeException(nameof(multiplier), multiplier, "Debe ser >= 1.");
            Chance = chance;
            Multiplier = Guard.Positive(multiplier, nameof(multiplier));
        }

        public static CritRules None => new CritRules(0f, 1f);

        public float Chance { get; }
        public float Multiplier { get; }
    }
}
