using System;

namespace Specimen.Core.Visuals
{
    /// <summary>Color RGB con componentes en [0, 1], sin depender de UnityEngine.</summary>
    public readonly struct GreyboxColor : IEquatable<GreyboxColor>
    {
        public float R { get; }
        public float G { get; }
        public float B { get; }

        public GreyboxColor(float r, float g, float b)
        {
            R = Unit(r, nameof(r));
            G = Unit(g, nameof(g));
            B = Unit(b, nameof(b));
        }

        private static float Unit(float value, string name)
            => value >= 0f && value <= 1f
                ? value
                : throw new ArgumentOutOfRangeException(name, value, "Debe estar en [0, 1].");

        public bool Equals(GreyboxColor other) => R == other.R && G == other.G && B == other.B;

        public override bool Equals(object obj) => obj is GreyboxColor other && Equals(other);

        public override int GetHashCode() => (R, G, B).GetHashCode();

        public override string ToString() => $"({R}, {G}, {B})";
    }
}
