using System;
using Specimen.Core.Common;
using Specimen.Core.Siege.Scoring;

namespace Specimen.Core.Siege.Extraction
{
    /// <summary>Convierte la puntuación final en puntos de ADN: floor(Puntos × Multiplicador / constante) (GDD §3.5).</summary>
    public sealed class DnaConversion
    {
        public DnaConversion(float constant) => Constant = Guard.Positive(constant, nameof(constant));

        public float Constant { get; }

        public int ToDnaPoints(ScoreResult score) => (int)Math.Floor(score.Total / Constant);

        /// <summary>Constante provisional: 100 puntos de puntuación por punto de ADN.</summary>
        public static DnaConversion Default => new DnaConversion(100f);
    }
}
