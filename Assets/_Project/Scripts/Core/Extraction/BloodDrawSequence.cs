using System.Collections.Generic;
using Specimen.Core.Common;

namespace Specimen.Core.Extraction
{
    /// <summary>
    /// Cinemática de la extracción (GDD §3): al subir al Osprey, felicitación fría, muestra de sangre y sueño.
    /// Avanza por pasos con <see cref="Tick"/>; al acabar, la capa Unity lleva al Laboratorio.
    /// </summary>
    public sealed class BloodDrawSequence
    {
        private readonly IReadOnlyList<BloodDrawStep> _steps;
        private int _index;
        private float _elapsed;

        public BloodDrawSequence(IReadOnlyList<BloodDrawStep> steps)
        {
            _steps = Guard.NotNull(steps, nameof(steps));
            if (steps.Count == 0) throw new System.ArgumentException("Hace falta al menos un paso.", nameof(steps));
        }

        public bool IsFinished => _index >= _steps.Count;

        /// <summary>Paso en curso; el último cuando ya ha terminado.</summary>
        public BloodDrawStep Current => _steps[System.Math.Min(_index, _steps.Count - 1)];

        /// <summary>Fracción [0, 1] recorrida del paso en curso (1 al terminar).</summary>
        public float Progress => IsFinished ? 1f : System.Math.Min(1f, _elapsed / Current.Seconds);

        public void Tick(float deltaSeconds)
        {
            if (IsFinished) return;
            _elapsed += Guard.NonNegative(deltaSeconds, nameof(deltaSeconds));
            while (!IsFinished && _elapsed >= Current.Seconds)
            {
                _elapsed -= Current.Seconds;
                _index++;
            }
        }

        /// <summary>Guion de la demo (unos 8 s en total).</summary>
        public static BloodDrawSequence CreateDefault() => new BloodDrawSequence(new[]
        {
            new BloodDrawStep("«Enhorabuena, espécimen. Extracción completada.»", 3f, BloodDrawTone.Calm),
            new BloodDrawStep("Un pinchazo en el brazo. Te sacan sangre.", 2.5f, BloodDrawTone.Blood),
            new BloodDrawStep("Pierdes el conocimiento…", 2.5f, BloodDrawTone.Blackout)
        });
    }
}
