using Specimen.Core.Common;
using Specimen.Core.Time;

namespace Specimen.Core.Vitals
{
    /// <summary>
    /// Regeneración que se acelera con la calma: recupera a ritmo base mientras el recurso se gasta y, pasados
    /// <see cref="QuietSeconds"/> sin gastar, el ritmo sube linealmente hasta <see cref="MaxMultiplier"/> veces
    /// en <see cref="RampSeconds"/>. Cualquier gasto devuelve el ritmo al base.
    /// </summary>
    public sealed class AcceleratingRegen : ITickable
    {
        private readonly ResourcePool _pool;
        private float _quiet;
        private float _lastValue;

        public AcceleratingRegen(ResourcePool pool, float basePerSecond, float quietSeconds, float rampSeconds, float maxMultiplier)
        {
            _pool = Guard.NotNull(pool, nameof(pool));
            BasePerSecond = Guard.NonNegative(basePerSecond, nameof(basePerSecond));
            QuietSeconds = Guard.NonNegative(quietSeconds, nameof(quietSeconds));
            RampSeconds = Guard.Positive(rampSeconds, nameof(rampSeconds));
            MaxMultiplier = System.Math.Max(1f, maxMultiplier);
            _lastValue = pool.Current;
            _pool.Changed += OnChanged;
        }

        public float BasePerSecond { get; }
        public float QuietSeconds { get; }
        public float RampSeconds { get; }
        public float MaxMultiplier { get; }

        /// <summary>Multiplicador actual sobre el ritmo base (1 hasta que pasa la calma inicial).</summary>
        public float Multiplier
        {
            get
            {
                float over = _quiet - QuietSeconds;
                if (over <= 0f) return 1f;
                return 1f + (MaxMultiplier - 1f) * System.Math.Min(1f, over / RampSeconds);
            }
        }

        public float CurrentPerSecond => BasePerSecond * Multiplier;

        public void Tick(float deltaSeconds)
        {
            Guard.NonNegative(deltaSeconds, nameof(deltaSeconds));
            _quiet += deltaSeconds;
            if (_pool.IsFull) return;
            _pool.Restore(CurrentPerSecond * deltaSeconds);
        }

        private void OnChanged(ResourcePool pool)
        {
            if (pool.Current < _lastValue) _quiet = 0f;
            _lastValue = pool.Current;
        }
    }
}
