using System;
using System.Collections.Generic;
using Specimen.Core.Common;

namespace Specimen.Core.Randomness
{
    /// <summary>
    /// PCG32 (O'Neill). Implementación propia para que la misma semilla produzca la misma secuencia
    /// en Mono/IL2CPP (Unity) y en .NET (CI/servidor), cosa que System.Random no garantiza.
    /// </summary>
    public sealed class Pcg32Random : IRandom
    {
        private const ulong Multiplier = 6364136223846793005UL;
        private ulong _state;
        private readonly ulong _increment;

        public ulong Seed { get; }

        public Pcg32Random(ulong seed, ulong stream = 54UL)
        {
            Seed = seed;
            _increment = (stream << 1) | 1UL;
            _state = 0UL;
            NextUInt();
            _state += seed;
            NextUInt();
        }

        public uint NextUInt()
        {
            ulong old = _state;
            _state = unchecked(old * Multiplier + _increment);
            uint xorshifted = (uint)(((old >> 18) ^ old) >> 27);
            int rot = (int)(old >> 59);
            return (xorshifted >> rot) | (xorshifted << ((-rot) & 31));
        }

        public int Range(int minInclusive, int maxExclusive)
        {
            if (maxExclusive <= minInclusive)
                throw new ArgumentException("maxExclusive debe ser mayor que minInclusive.");
            uint bound = (uint)((long)maxExclusive - minInclusive);
            // Rechazo para evitar sesgo de módulo.
            uint threshold = unchecked((uint)-bound) % bound;
            uint r;
            do { r = NextUInt(); } while (r < threshold);
            return (int)(minInclusive + (long)(r % bound));
        }

        public float Value() => (NextUInt() >> 8) * (1f / 16777216f);

        public float Range(float min, float max)
        {
            if (max < min) throw new ArgumentException("max debe ser >= min.");
            float v = min + (max - min) * Value();
            return v < max ? v : min; // protege contra redondeo en el límite superior
        }

        public bool Chance(float probability) => probability > 0f && Value() < probability;

        public T Pick<T>(IReadOnlyList<T> items)
        {
            Guard.NotNull(items, nameof(items));
            if (items.Count == 0) throw new ArgumentException("La lista está vacía.", nameof(items));
            return items[Range(0, items.Count)];
        }

        public int WeightedIndex(IReadOnlyList<float> weights)
        {
            Guard.NotNull(weights, nameof(weights));
            float total = 0f;
            for (int i = 0; i < weights.Count; i++)
                total += Guard.NonNegative(weights[i], nameof(weights));
            if (total <= 0f) throw new ArgumentException("La suma de pesos debe ser > 0.", nameof(weights));

            float roll = Value() * total;
            for (int i = 0; i < weights.Count; i++)
            {
                if (weights[i] <= 0f) continue;
                if (roll < weights[i]) return i;
                roll -= weights[i];
            }
            for (int i = weights.Count - 1; i >= 0; i--)
                if (weights[i] > 0f) return i; // redondeo
            throw new InvalidOperationException("Inalcanzable.");
        }

        public IRandom Fork(uint salt)
        {
            ulong seed = ((ulong)NextUInt() << 32) | NextUInt();
            return new Pcg32Random(seed ^ ((ulong)salt * 0x9E3779B97F4A7C15UL), salt);
        }
    }
}
