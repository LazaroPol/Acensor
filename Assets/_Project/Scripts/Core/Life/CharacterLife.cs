using System;
using Specimen.Core.Common;
using Specimen.Core.Iteration;
using Specimen.Core.Vitals;

namespace Specimen.Core.Life
{
    /// <summary>
    /// Ciclo muerte → Espectro → revivir de un jugador. Al vaciarse la vida pasa a Espectro: no combate,
    /// pero puede activar mecanismos y marcar enemigos. Se le revive en la Litera (solo en Calma) o con un objeto.
    /// </summary>
    public sealed class CharacterLife
    {
        private readonly CharacterVitals _vitals;
        private readonly ReviveRules _rules;
        private readonly IReviveCost _cost;

        public event Action<CharacterLife> Died;
        public event Action<CharacterLife, ReviveMethod> Revived;

        public CharacterLife(CharacterVitals vitals, ReviveRules rules, IReviveCost cost)
        {
            _vitals = Guard.NotNull(vitals, nameof(vitals));
            _rules = Guard.NotNull(rules, nameof(rules));
            _cost = Guard.NotNull(cost, nameof(cost));
            State = vitals.IsAlive ? LifeState.Alive : LifeState.Spectre;
            _vitals.Health.Depleted += _ => BecomeSpectre();
        }

        public LifeState State { get; private set; }
        public bool IsSpectre => State == LifeState.Spectre;
        public bool CanFight => !IsSpectre;
        public bool CanActivateMechanisms => true;
        public bool CanMarkEnemies => IsSpectre;

        public ReviveResult TryRevive(ReviveMethod method, IterationPhase phase)
        {
            if (!IsSpectre) return ReviveResult.NotSpectre;
            if (method == ReviveMethod.ElevatorBunk && phase != IterationPhase.Calm) return ReviveResult.WrongPhase;
            if (!_cost.TryPay(method)) return ReviveResult.CannotPay;
            if (method == ReviveMethod.ElevatorBunk) RefillSecondaryPools();
            _vitals.Health.Restore(_vitals.Health.Max * _rules.HealthFractionFor(method));
            State = LifeState.Alive;
            Revived?.Invoke(this, method);
            return ReviveResult.Revived;
        }

        private void RefillSecondaryPools()
        {
            _vitals.ClassResource.Fill();
            _vitals.Stamina.Fill();
        }

        private void BecomeSpectre()
        {
            if (IsSpectre) return;
            State = LifeState.Spectre;
            Died?.Invoke(this);
        }
    }
}
