using System;
using Specimen.Core.Common;
using Specimen.Core.Time;
using Specimen.Core.Vitals;

namespace Specimen.Core.Prototype
{
    /// <summary>Acciones de las teclas de depuración del prototipo: dañar, curar y avanzar el tiempo de fase.</summary>
    public sealed class DebugCommands
    {
        private readonly CharacterVitals _vitals;
        private readonly PhaseTimer _timer;

        public DebugCommands(CharacterVitals vitals, PhaseTimer timer,
            float damage = 10f, float heal = 10f, float timeStep = 30f)
        {
            _vitals = Guard.NotNull(vitals, nameof(vitals));
            _timer = Guard.NotNull(timer, nameof(timer));
            Damage = Guard.Positive(damage, nameof(damage));
            Heal = Guard.Positive(heal, nameof(heal));
            TimeStep = Guard.Positive(timeStep, nameof(timeStep));
        }

        public float Damage { get; }
        public float Heal { get; }
        public float TimeStep { get; }

        /// <summary>Resta <see cref="Damage"/> de vida (sin bajar de 0) y devuelve lo restado.</summary>
        public float Hurt() => _vitals.Health.Drain(Damage);

        /// <summary>Suma <see cref="Heal"/> de vida (sin pasar del máximo) y devuelve lo sumado.</summary>
        public float Cure() => _vitals.Health.Restore(Heal);

        /// <summary>Adelanta la fase <see cref="TimeStep"/> segundos (puede hacerla expirar).</summary>
        public void AdvanceTime() => _timer.ShortenTo(Math.Max(0f, _timer.Remaining - TimeStep));
    }
}
