using System;
using Specimen.Core.Common;

namespace Specimen.Core.Iteration.Readiness
{
    /// <summary>Bonus proporcional al tiempo ahorrado: ahorrar toda la Calma da <see cref="MaxBonus"/>.</summary>
    public sealed class LinearHasteBonus : IHasteBonusPolicy
    {
        public const float DefaultMaxBonus = 0.25f;

        public float MaxBonus { get; }

        public LinearHasteBonus(float maxBonus = DefaultMaxBonus) => MaxBonus = Guard.NonNegative(maxBonus, nameof(maxBonus));

        public float BonusFor(float savedSeconds, float calmSeconds)
        {
            Guard.Positive(calmSeconds, nameof(calmSeconds));
            float ratio = Math.Max(0f, Math.Min(1f, savedSeconds / calmSeconds));
            return MaxBonus * ratio;
        }
    }
}
