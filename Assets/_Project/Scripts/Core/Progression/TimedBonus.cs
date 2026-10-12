using System;

namespace Specimen.Core.Progression
{
    /// <summary>Bonificación temporal (p. ej. «Furia»: +X % de daño durante unos segundos). No se acumula: gana la mayor.</summary>
    public sealed class TimedBonus
    {
        public float Value { get; private set; }
        public float Remaining { get; private set; }
        public bool IsActive => Remaining > 0f;
        public float Current => IsActive ? Value : 0f;

        public void Begin(float value, float seconds)
        {
            if (IsActive && value < Value) return;
            Value = value;
            Remaining = Math.Max(Remaining, seconds);
        }

        public void Tick(float dt)
        {
            Remaining = Math.Max(0f, Remaining - dt);
            if (!IsActive) Value = 0f;
        }
    }
}
