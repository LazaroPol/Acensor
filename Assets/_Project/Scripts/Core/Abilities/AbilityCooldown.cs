using System;
using Specimen.Core.Common;

namespace Specimen.Core.Abilities
{
    /// <summary>Enfriamiento de una habilidad: cuenta atrás que avanza con <see cref="Tick"/>.</summary>
    public sealed class AbilityCooldown
    {
        private readonly float _duration;

        public AbilityCooldown(float durationSeconds) => _duration = Guard.NonNegative(durationSeconds, nameof(durationSeconds));

        public float Remaining { get; private set; }
        public bool IsReady => Remaining <= 0f;
        public float Fraction => _duration > 0f ? Remaining / _duration : 0f;

        public void Start() => Remaining = _duration;

        public void Reset() => Remaining = 0f;

        public void Tick(float deltaSeconds)
        {
            Guard.NonNegative(deltaSeconds, nameof(deltaSeconds));
            Remaining = Math.Max(0f, Remaining - deltaSeconds);
        }
    }
}
