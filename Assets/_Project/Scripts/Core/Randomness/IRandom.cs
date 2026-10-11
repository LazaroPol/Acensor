using System.Collections.Generic;

namespace Specimen.Core.Randomness
{
    /// <summary>Generador aleatorio inyectable. Permite partidas reproducibles por semilla y tests deterministas.</summary>
    public interface IRandom
    {
        /// <summary>Entero en [minInclusive, maxExclusive).</summary>
        int Range(int minInclusive, int maxExclusive);

        /// <summary>Real en [0, 1).</summary>
        float Value();

        /// <summary>Real en [min, max).</summary>
        float Range(float min, float max);

        /// <summary>Probabilidad p de devolver true.</summary>
        bool Chance(float probability);

        T Pick<T>(IReadOnlyList<T> items);

        /// <summary>Índice elegido proporcionalmente a pesos no negativos.</summary>
        int WeightedIndex(IReadOnlyList<float> weights);

        /// <summary>Sub-generador independiente y determinista (p. ej. uno por piso).</summary>
        IRandom Fork(uint salt);
    }
}
