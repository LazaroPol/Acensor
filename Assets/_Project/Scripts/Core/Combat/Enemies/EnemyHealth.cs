using System;
using Specimen.Core.Common;

namespace Specimen.Core.Combat.Enemies
{
    /// <summary>Vida de un enemigo en combate: nunca baja de 0 y avisa de su muerte una sola vez.</summary>
    public sealed class EnemyHealth
    {
        public EnemyHealth(float max)
        {
            Max = Guard.Positive(max, nameof(max));
            Current = Max;
        }

        public event Action Died;

        public float Max { get; }
        public float Current { get; private set; }
        public bool IsDead => Current <= 0f;
        public float Fraction => Current / Max;

        /// <summary>Aplica daño; devuelve true solo en el golpe que lo mata.</summary>
        public bool TakeDamage(float amount)
        {
            Guard.NonNegative(amount, nameof(amount));
            if (IsDead) return false;
            Current = Math.Max(0f, Current - amount);
            if (!IsDead) return false;
            Died?.Invoke();
            return true;
        }

        public void Revive() => Current = Max;
    }
}
