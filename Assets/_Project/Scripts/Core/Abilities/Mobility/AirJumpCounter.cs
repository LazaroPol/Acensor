using Specimen.Core.Common;

namespace Specimen.Core.Abilities.Mobility
{
    /// <summary>
    /// Controla los saltos: en el suelo siempre se puede saltar; en el aire solo mientras queden saltos aéreos
    /// (doble salto). Aterrizar los recarga.
    /// </summary>
    public sealed class AirJumpCounter
    {
        private readonly int _maxAirJumps;

        public AirJumpCounter(int maxAirJumps)
        {
            _maxAirJumps = Guard.NonNegative(maxAirJumps, nameof(maxAirJumps));
            Remaining = _maxAirJumps;
        }

        public int Remaining { get; private set; }

        public bool TryJump(bool grounded)
        {
            if (grounded) return true;
            if (Remaining <= 0) return false;
            Remaining--;
            return true;
        }

        public void Land() => Remaining = _maxAirJumps;
    }
}
