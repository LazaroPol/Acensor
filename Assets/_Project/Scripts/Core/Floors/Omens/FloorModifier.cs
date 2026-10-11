using System;

namespace Specimen.Core.Floors.Omens
{
    /// <summary>Modificador de piso: nombre, pista que lo anuncia en el Presagio y si exige combate.</summary>
    public sealed class FloorModifier
    {
        public FloorModifier(FloorModifierKind kind, string displayName, string hint, bool requiresCombat = false)
        {
            if (string.IsNullOrWhiteSpace(displayName))
                throw new ArgumentException("Nombre vacío.", nameof(displayName));
            if (string.IsNullOrWhiteSpace(hint)) throw new ArgumentException("Pista vacía.", nameof(hint));
            Kind = kind;
            DisplayName = displayName;
            Hint = hint;
            RequiresCombat = requiresCombat;
        }

        public FloorModifierKind Kind { get; }
        public string DisplayName { get; }
        /// <summary>Frase con la que el Presagio insinúa el modificador.</summary>
        public string Hint { get; }
        /// <summary>Solo tiene sentido en pisos con combate (p. ej. enemigos ígneos).</summary>
        public bool RequiresCombat { get; }

        public override string ToString() => DisplayName;
    }
}
