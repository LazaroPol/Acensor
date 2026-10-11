using Specimen.Core.Common;

namespace Specimen.Core.Time
{
    /// <summary>Reloj controlado manualmente: útil en tests y en simulaciones del servidor.</summary>
    public sealed class ManualClock : IClock, ITickable
    {
        public double Now { get; private set; }

        public ManualClock(double start = 0d)
        {
            Now = start;
        }

        public void Tick(float deltaSeconds) => Advance(deltaSeconds);

        public void Advance(float seconds) => Now += Guard.NonNegative(seconds, nameof(seconds));
    }
}
