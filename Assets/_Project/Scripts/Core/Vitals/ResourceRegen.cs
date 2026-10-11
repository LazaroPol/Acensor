using Specimen.Core.Common;
using Specimen.Core.Time;

namespace Specimen.Core.Vitals
{
    /// <summary>
    /// Regenera un <see cref="ResourcePool"/> a ritmo constante, tras una espera desde el último gasto
    /// (p. ej. la stamina no se recupera mientras se corre).
    /// </summary>
    public sealed class ResourceRegen : ITickable
    {
        private readonly ResourcePool _pool;
        private float _sinceLastDrop;
        private float _lastValue;

        public ResourceRegen(ResourcePool pool, float perSecond, float delaySeconds = 0f)
        {
            _pool = Guard.NotNull(pool, nameof(pool));
            PerSecond = Guard.NonNegative(perSecond, nameof(perSecond));
            DelaySeconds = Guard.NonNegative(delaySeconds, nameof(delaySeconds));
            _lastValue = pool.Current;
            _sinceLastDrop = DelaySeconds;
            _pool.Changed += OnChanged;
        }

        public float PerSecond { get; }
        public float DelaySeconds { get; }
        public bool Paused { get; set; }

        public void Tick(float deltaSeconds)
        {
            Guard.NonNegative(deltaSeconds, nameof(deltaSeconds));
            if (Paused || _pool.IsFull) return;
            float waitLeft = DelaySeconds - _sinceLastDrop;
            _sinceLastDrop += deltaSeconds;
            float regenTime = waitLeft > 0f ? deltaSeconds - waitLeft : deltaSeconds;
            if (regenTime > 0f) _pool.Restore(regenTime * PerSecond);
        }

        private void OnChanged(ResourcePool pool)
        {
            if (pool.Current < _lastValue) _sinceLastDrop = 0f;
            _lastValue = pool.Current;
        }
    }
}
