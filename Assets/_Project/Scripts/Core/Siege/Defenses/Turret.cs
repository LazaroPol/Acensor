using System;
using Specimen.Core.Common;

namespace Specimen.Core.Siege.Defenses
{
    /// <summary>Torreta del ingeniero: dispara a objetivos en alcance con munición limitada y cadencia fija.</summary>
    public sealed class Turret
    {
        private readonly float _damage;
        private readonly float _fireInterval;
        private float _cooldown;

        public Turret(float damage, float range, float fireIntervalSeconds, int maxAmmo)
        {
            _damage = Guard.Positive(damage, nameof(damage));
            Range = Guard.Positive(range, nameof(range));
            _fireInterval = Guard.Positive(fireIntervalSeconds, nameof(fireIntervalSeconds));
            MaxAmmo = Guard.Positive(maxAmmo, nameof(maxAmmo));
            Ammo = maxAmmo;
        }

        public float Range { get; }
        public int MaxAmmo { get; }
        public int Ammo { get; private set; }
        public bool IsReady => _cooldown <= 0f && Ammo > 0;

        public void Tick(float deltaSeconds)
        {
            Guard.NonNegative(deltaSeconds, nameof(deltaSeconds));
            _cooldown = Math.Max(0f, _cooldown - deltaSeconds);
        }

        /// <summary>Recarga munición (sin pasar del máximo) y devuelve la repuesta.</summary>
        public int Reload(int rounds)
        {
            Guard.NonNegative(rounds, nameof(rounds));
            var added = Math.Min(MaxAmmo - Ammo, rounds);
            Ammo += added;
            return added;
        }

        /// <summary>Dispara al objetivo si está en alcance, hay munición y no está en enfriamiento; si no, devuelve null.</summary>
        public Combat.Hit TryFire(float targetDistance)
        {
            Guard.NonNegative(targetDistance, nameof(targetDistance));
            if (!IsReady || targetDistance > Range) return null;
            Ammo--;
            _cooldown = _fireInterval;
            return new Combat.Hit(_damage, Combat.DamageType.Physical, false);
        }
    }
}
