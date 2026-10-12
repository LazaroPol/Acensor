using System;
using System.Collections.Generic;
using System.Linq;
using Specimen.Core.Items;

namespace Specimen.Core.Crafting
{
    /// <summary>Constantes de calidad (datos): grados con umbrales, pesos de la puntuación y rango aleatorio.</summary>
    public sealed class QualityRules
    {
        private readonly QualityGrade[] _grades;

        public QualityRules(IEnumerable<QualityGrade> grades, QualityWeights weights)
        {
            if (grades == null) throw new ArgumentNullException(nameof(grades));
            _grades = grades.OrderBy(g => g.Quality).ToArray();
            Weights = weights ?? throw new ArgumentNullException(nameof(weights));
            if (_grades.Length != 6)
                throw new ArgumentException("Se esperan los seis grados.", nameof(grades));
            for (int i = 0; i < _grades.Length; i++)
            {
                if ((int)_grades[i].Quality != i + 1)
                    throw new ArgumentException("Grado repetido o ausente.", nameof(grades));
                if (i > 0 && _grades[i].MinScore <= _grades[i - 1].MinScore)
                    throw new ArgumentException("Los umbrales deben ser crecientes.", nameof(grades));
            }
        }

        /// <summary>Tabla del GDD §17.1.</summary>
        public static QualityRules Default { get; } = new QualityRules(new[]
        {
            new QualityGrade(Quality.Crude, "Tosca", int.MinValue, 1, 0.85f, 0.8f),
            new QualityGrade(Quality.Common, "Corriente", 8, 1, 1.00f, 1.0f),
            new QualityGrade(Quality.Good, "Buena", 14, 2, 1.10f, 1.15f),
            new QualityGrade(Quality.Fine, "Fina", 21, 2, 1.20f, 1.3f),
            new QualityGrade(Quality.Masterwork, "Magistral", 29, 3, 1.35f, 1.5f),
            new QualityGrade(Quality.Legendary, "Legendaria", 37, 4, 1.50f, 1.8f)
        }, QualityWeights.Default);

        public IReadOnlyList<QualityGrade> Grades => _grades;
        public QualityWeights Weights { get; }

        public QualityGrade Grade(Quality quality)
        {
            if (quality == Quality.None) throw new ArgumentException("Sin calidad.", nameof(quality));
            return _grades[(int)quality - 1];
        }

        /// <summary>Grado que corresponde a una puntuación (el mayor cuyo umbral se alcanza).</summary>
        public Quality FromScore(int score)
        {
            var result = Quality.Crude;
            foreach (var g in _grades)
                if (score >= g.MinScore) result = g.Quality;
            return result;
        }
    }
}
