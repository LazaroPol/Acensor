using System;
using System.Collections.Generic;
using System.Linq;
using Specimen.Core.Common;

namespace Specimen.Core.Time
{
    /// <summary>
    /// Cuenta atrás de una fase (Calma, Incursión…). Avisa una vez por umbral cuando el tiempo restante
    /// lo alcanza (aunque un Tick grande salte varios, en orden) y notifica la expiración una sola vez.
    /// </summary>
    public sealed class PhaseTimer : ITickable
    {
        private readonly float[] _warnings;
        private int _nextWarning;

        public float Duration { get; }
        public float Remaining { get; private set; }
        public float Elapsed => Duration - Remaining;
        public bool IsRunning { get; private set; }
        public bool IsExpired { get; private set; }

        /// <summary>Se dispara con el umbral alcanzado (segundos restantes configurados).</summary>
        public event Action<float> WarningReached;
        public event Action Expired;

        public PhaseTimer(float durationSeconds, IEnumerable<float> warningThresholds = null)
        {
            Duration = Guard.Positive(durationSeconds, nameof(durationSeconds));
            Remaining = Duration;
            _warnings = (warningThresholds ?? Enumerable.Empty<float>())
                .Where(w => w > 0f && w < Duration).Distinct().OrderByDescending(w => w).ToArray();
        }

        public void Start()
        {
            if (IsExpired) throw new InvalidOperationException("El temporizador ya expiró.");
            IsRunning = true;
        }

        public void Pause() => IsRunning = false;

        public void Tick(float deltaSeconds)
        {
            Guard.NonNegative(deltaSeconds, nameof(deltaSeconds));
            if (!IsRunning) return;
            Remaining = Math.Max(0f, Remaining - deltaSeconds);
            Evaluate();
        }

        /// <summary>Acorta la fase (p. ej. voto de cierre anticipado). Nunca la alarga.</summary>
        public void ShortenTo(float remainingSeconds)
        {
            Guard.NonNegative(remainingSeconds, nameof(remainingSeconds));
            if (IsExpired || remainingSeconds >= Remaining) return;
            Remaining = remainingSeconds;
            if (IsRunning) Evaluate();
        }

        private void Evaluate()
        {
            while (_nextWarning < _warnings.Length && Remaining <= _warnings[_nextWarning])
                WarningReached?.Invoke(_warnings[_nextWarning++]);

            if (Remaining > 0f || IsExpired) return;
            IsExpired = true;
            IsRunning = false;
            Expired?.Invoke();
        }
    }
}
