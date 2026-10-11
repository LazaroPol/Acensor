using System;

namespace Specimen.Core.Floors
{
    /// <summary>Pasillo no dirigido entre dos salas (A &lt; B).</summary>
    public readonly struct Corridor : IEquatable<Corridor>
    {
        public Corridor(int a, int b)
        {
            if (a == b) throw new ArgumentException("Un pasillo no puede unir una sala consigo misma.");
            A = Math.Min(a, b);
            B = Math.Max(a, b);
        }

        public int A { get; }
        public int B { get; }

        public bool Equals(Corridor other) => A == other.A && B == other.B;
        public override bool Equals(object obj) => obj is Corridor c && Equals(c);
        public override int GetHashCode() => (A * 397) ^ B;
        public override string ToString() => $"{A}-{B}";
    }
}
