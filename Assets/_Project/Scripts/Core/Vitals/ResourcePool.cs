using System;
using Specimen.Core.Common;
using Specimen.Core.Stats;

namespace Specimen.Core.Vitals
{
    /// <summary>
    /// Recurso consumible (vida, recurso de clase, stamina) cuyo máximo lo dicta un <see cref="Stat"/>.
    /// El valor actual siempre está en [0, Max]; si el máximo baja, se recorta.
    /// </summary>
    public sealed class ResourcePool
    {
        private readonly Stat _max;
        private float _current;

        public event Action<ResourcePool> Changed;
        public event Action<ResourcePool> Depleted;

        public ResourcePool(Stat max, bool startFull = true)
        {
            _max = Guard.NotNull(max, nameof(max));
            _current = startFull ? _max.Value : 0f;
            _max.Changed += _ => SetCurrent(_current);
        }

        public float Current => _current;
        public float Max => _max.Value;
        public bool IsEmpty => _current <= 0f;
        public bool IsFull => _current >= Max;
        public float Fraction => Max > 0f ? _current / Max : 0f;

        /// <summary>Gasta exactamente <paramref name="amount"/> si hay suficiente; si no, no toca nada.</summary>
        public bool TrySpend(float amount)
        {
            Guard.NonNegative(amount, nameof(amount));
            if (amount > _current) return false;
            SetCurrent(_current - amount);
            return true;
        }

        /// <summary>Resta hasta <paramref name="amount"/> (sin bajar de 0) y devuelve lo restado de verdad.</summary>
        public float Drain(float amount)
        {
            Guard.NonNegative(amount, nameof(amount));
            float before = _current;
            SetCurrent(_current - amount);
            return before - _current;
        }

        /// <summary>Suma hasta <paramref name="amount"/> (sin pasar del máximo) y devuelve lo sumado de verdad.</summary>
        public float Restore(float amount)
        {
            Guard.NonNegative(amount, nameof(amount));
            float before = _current;
            SetCurrent(_current + amount);
            return _current - before;
        }

        public void Fill() => SetCurrent(Max);

        public void Empty() => SetCurrent(0f);

        private void SetCurrent(float value)
        {
            float clamped = Math.Max(0f, Math.Min(value, Max));
            if (clamped == _current) return;
            bool wasEmpty = IsEmpty;
            _current = clamped;
            Changed?.Invoke(this);
            if (!wasEmpty && IsEmpty) Depleted?.Invoke(this);
        }
    }
}
