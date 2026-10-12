using Specimen.Core.Common;

namespace Specimen.Core.Sandbox
{
    /// <summary>Cuenta atrás de reaparición: se arma al morir y avisa una sola vez cuando se cumple.</summary>
    public sealed class RespawnTimer
    {
        public RespawnTimer(float delaySeconds) => Delay = Guard.NonNegative(delaySeconds, nameof(delaySeconds));

        public float Delay { get; }
        public bool IsRunning { get; private set; }
        public float Remaining { get; private set; }

        public void Begin()
        {
            IsRunning = true;
            Remaining = Delay;
        }

        public void Cancel()
        {
            IsRunning = false;
            Remaining = 0f;
        }

        /// <summary>Avanza el tiempo; devuelve true solo en el tick en que toca reaparecer.</summary>
        public bool Tick(float deltaSeconds)
        {
            Guard.NonNegative(deltaSeconds, nameof(deltaSeconds));
            if (!IsRunning) return false;
            Remaining -= deltaSeconds;
            if (Remaining > 0f) return false;
            Cancel();
            return true;
        }
    }
}
