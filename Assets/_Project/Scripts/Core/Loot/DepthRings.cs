using System;

namespace Specimen.Core.Loot
{
    /// <summary>
    /// Anillos de profundidad de un piso (1 = cerca del ascensor, 3 = el más profundo).
    /// Más lejos = mejor loot y peores enemigos.
    /// </summary>
    public static class DepthRings
    {
        public const int Min = 1;
        public const int Max = 3;

        public static bool IsValid(int ring) => ring >= Min && ring <= Max;

        public static int Validate(int ring, string paramName)
        {
            if (!IsValid(ring))
                throw new ArgumentOutOfRangeException(paramName, ring, $"El anillo debe estar entre {Min} y {Max}.");
            return ring;
        }
    }
}
