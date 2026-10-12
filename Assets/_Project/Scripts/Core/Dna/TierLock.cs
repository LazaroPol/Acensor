using System;

namespace Specimen.Core.Dna
{
    /// <summary>Umbrales por nivel (índice 0 = tier 1). Por defecto 0, 5, 12, 20, 30 (GDD §12).</summary>
    public sealed class TierLock : ITierLock
    {
        private readonly int[] _thresholds;

        public TierLock(params int[] thresholds)
        {
            if (thresholds == null || thresholds.Length == 0)
                throw new ArgumentException("Se necesita al menos un umbral.", nameof(thresholds));
            _thresholds = (int[])thresholds.Clone();
        }

        public static TierLock CreateDefault() => new TierLock(0, 5, 12, 20, 30);

        public int MaxTier => _thresholds.Length;

        public int RequiredPoints(int tier)
        {
            if (tier < 1 || tier > _thresholds.Length)
                throw new ArgumentOutOfRangeException(nameof(tier), tier, "Tier fuera de rango.");
            return _thresholds[tier - 1];
        }
    }
}
