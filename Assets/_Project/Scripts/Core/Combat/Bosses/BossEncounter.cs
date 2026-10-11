using System;
using System.Collections.Generic;
using Specimen.Core.Combat.Enemies;
using Specimen.Core.Common;

namespace Specimen.Core.Combat.Bosses
{
    /// <summary>Combate contra un jefe: lleva su vida y avanza de fase al perderla.</summary>
    public sealed class BossEncounter
    {
        private readonly Enemy _boss;
        private readonly IReadOnlyList<BossPhase> _phases;
        private int _phaseIndex;

        public BossEncounter(Enemy boss, IReadOnlyList<BossPhase> phases)
        {
            _boss = Guard.NotNull(boss, nameof(boss));
            _phases = Guard.NotNull(phases, nameof(phases));
            Validate(phases);
            Health = boss.Stats.MaxHealth;
        }

        /// <summary>Se dispara una vez por cada fase nueva alcanzada, en orden.</summary>
        public event Action<BossPhase> PhaseEntered;

        public float Health { get; private set; }
        public bool IsDefeated => Health <= 0f;
        public BossPhase CurrentPhase => _phases[_phaseIndex];
        public Enemy Boss => _boss;

        public void TakeDamage(float amount)
        {
            Guard.NonNegative(amount, nameof(amount));
            if (IsDefeated) return;
            Health = Math.Max(0f, Health - amount);
            var fraction = Health / _boss.Stats.MaxHealth;
            while (_phaseIndex + 1 < _phases.Count && fraction <= _phases[_phaseIndex + 1].EntryHealthFraction)
            {
                _phaseIndex++;
                PhaseEntered?.Invoke(_phases[_phaseIndex]);
            }
        }

        /// <summary>Golpe del jefe con el daño multiplicado por su fase actual.</summary>
        public Hit Attack()
        {
            var hit = _boss.Attack();
            return new Hit(hit.Amount * CurrentPhase.DamageMultiplier, hit.Type, hit.CanCrit);
        }

        private static void Validate(IReadOnlyList<BossPhase> phases)
        {
            if (phases.Count == 0) throw new ArgumentException("El jefe necesita al menos una fase.", nameof(phases));
            if (phases[0].EntryHealthFraction != 1f)
                throw new ArgumentException("La primera fase debe empezar con vida completa (1).", nameof(phases));
            for (var i = 1; i < phases.Count; i++)
                if (phases[i].EntryHealthFraction >= phases[i - 1].EntryHealthFraction)
                    throw new ArgumentException("Las fases deben ir con vida decreciente.", nameof(phases));
        }
    }
}
