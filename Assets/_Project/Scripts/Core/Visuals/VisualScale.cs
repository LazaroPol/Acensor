using System;
using Specimen.Core.Common;

namespace Specimen.Core.Visuals
{
    /// <summary>Escala en metros (1 unidad = 1 m) con componentes estrictamente positivas.</summary>
    public readonly struct VisualScale : IEquatable<VisualScale>
    {
        public float X { get; }
        public float Y { get; }
        public float Z { get; }

        public VisualScale(float x, float y, float z)
        {
            X = Guard.Positive(x, nameof(x));
            Y = Guard.Positive(y, nameof(y));
            Z = Guard.Positive(z, nameof(z));
        }

        public bool Equals(VisualScale other) => X == other.X && Y == other.Y && Z == other.Z;

        public override bool Equals(object obj) => obj is VisualScale other && Equals(other);

        public override int GetHashCode() => (X, Y, Z).GetHashCode();

        public override string ToString() => $"({X}, {Y}, {Z})";
    }
}
