using Specimen.Core.Common;

namespace Specimen.Core.Sandbox
{
    /// <summary>
    /// Efecto de una habilidad de firma: tipo, magnitud (multiplicador de daño, vida curada, metros o
    /// reducción de daño según el tipo), radio (m) y duración (s).
    /// </summary>
    public sealed class SignatureEffect
    {
        public SignatureEffect(SignatureKind kind, float magnitude, float radius = 0f, float duration = 0f)
        {
            Kind = kind;
            Magnitude = Guard.Positive(magnitude, nameof(magnitude));
            Radius = Guard.NonNegative(radius, nameof(radius));
            Duration = Guard.NonNegative(duration, nameof(duration));
        }

        public SignatureKind Kind { get; }
        public float Magnitude { get; }
        public float Radius { get; }
        public float Duration { get; }

        public bool DealsDamage => Kind == SignatureKind.Area || Kind == SignatureKind.Torbellino || Kind == SignatureKind.Orbe;
    }
}
