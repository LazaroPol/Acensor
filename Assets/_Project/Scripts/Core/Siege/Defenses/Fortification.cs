using System;
using Specimen.Core.Common;

namespace Specimen.Core.Siege.Defenses
{
    /// <summary>Fortificación (muro, barricada) que absorbe daño y se puede reparar hasta su vida máxima.</summary>
    public sealed class Fortification
    {
        public Fortification(float maxHealth)
        {
            MaxHealth = Guard.Positive(maxHealth, nameof(maxHealth));
            Health = maxHealth;
        }

        public float MaxHealth { get; }
        public float Health { get; private set; }
        public bool IsDestroyed => Health <= 0f;

        /// <summary>Recibe daño y devuelve la vida perdida realmente. Una fortificación destruida no se repara.</summary>
        public float TakeDamage(float amount)
        {
            Guard.NonNegative(amount, nameof(amount));
            var lost = Math.Min(Health, amount);
            Health -= lost;
            return lost;
        }

        /// <summary>Repara y devuelve la vida recuperada; no hace nada si está destruida.</summary>
        public float Repair(float amount)
        {
            Guard.NonNegative(amount, nameof(amount));
            if (IsDestroyed) return 0f;
            var gained = Math.Min(MaxHealth - Health, amount);
            Health += gained;
            return gained;
        }
    }
}
