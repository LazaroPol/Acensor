using System;
using System.Collections.Generic;
using System.Linq;

namespace Specimen.Core.Professions
{
    /// <summary>
    /// Curva de XP de un oficio (GDD §17.1): tabla «nivel → XP para el siguiente». El nivel máximo es
    /// el tamaño de la tabla + 1. Es un dato; <see cref="Seed"/> solo genera la semilla del asset.
    /// </summary>
    public sealed class ProfessionCurve
    {
        public const int DefaultMaxLevel = 20;
        private readonly int[] _xpToNext;

        public ProfessionCurve(IEnumerable<int> xpToNext)
        {
            if (xpToNext == null) throw new ArgumentNullException(nameof(xpToNext));
            _xpToNext = xpToNext.ToArray();
            if (_xpToNext.Length == 0) throw new ArgumentException("La curva no puede estar vacía.", nameof(xpToNext));
            if (_xpToNext[0] <= 0) throw new ArgumentException("La XP debe ser positiva.", nameof(xpToNext));
            for (int i = 1; i < _xpToNext.Length; i++)
                if (_xpToNext[i] <= _xpToNext[i - 1])
                    throw new ArgumentException("La curva debe ser estrictamente creciente.", nameof(xpToNext));
        }

        public static ProfessionCurve Default { get; } = Seed();

        public int MaxLevel => _xpToNext.Length + 1;

        /// <summary>XP necesaria para pasar de <paramref name="level"/> al siguiente; 0 en el máximo.</summary>
        public int XpToNext(int level)
        {
            if (level < 1) throw new ArgumentOutOfRangeException(nameof(level));
            return level >= MaxLevel ? 0 : _xpToNext[level - 1];
        }

        /// <summary>Semilla <c>base · n^exponente</c> (por defecto 40 · n^1,5, nivel máx. 20).</summary>
        public static ProfessionCurve Seed(int baseXp = 40, double exponent = 1.5, int maxLevel = DefaultMaxLevel)
        {
            if (maxLevel < 2) throw new ArgumentOutOfRangeException(nameof(maxLevel));
            return new ProfessionCurve(Enumerable.Range(1, maxLevel - 1)
                .Select(n => (int)Math.Round(baseXp * Math.Pow(n, exponent))));
        }
    }
}
