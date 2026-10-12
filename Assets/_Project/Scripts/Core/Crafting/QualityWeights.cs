using System;

namespace Specimen.Core.Crafting
{
    /// <summary>Pesos de la puntuación de calidad y rango aleatorio inclusivo (GDD §17.1).</summary>
    public sealed class QualityWeights
    {
        public QualityWeights(int perLevel, int perTier, int perStationLevel, int randomMin, int randomMax)
        {
            if (perLevel < 0) throw new ArgumentOutOfRangeException(nameof(perLevel));
            if (perTier < 0) throw new ArgumentOutOfRangeException(nameof(perTier));
            if (perStationLevel < 0) throw new ArgumentOutOfRangeException(nameof(perStationLevel));
            if (randomMax < randomMin) throw new ArgumentOutOfRangeException(nameof(randomMax));
            PerLevel = perLevel;
            PerTier = perTier;
            PerStationLevel = perStationLevel;
            RandomMin = randomMin;
            RandomMax = randomMax;
        }

        /// <summary>nivel × 1, tier medio × 3, estación × 2, aleatorio [-2, +3].</summary>
        public static QualityWeights Default { get; } = new QualityWeights(1, 3, 2, -2, 3);

        public int PerLevel { get; }
        public int PerTier { get; }
        public int PerStationLevel { get; }
        public int RandomMin { get; }
        public int RandomMax { get; }

        /// <summary>Puntuación sin el componente aleatorio (tier medio redondeado tras ponderar).</summary>
        public int BaseScore(QualityRollInput input)
        {
            if (input == null) throw new ArgumentNullException(nameof(input));
            var tier = (int)Math.Round(input.AverageTier * PerTier, MidpointRounding.AwayFromZero);
            return input.ProfessionLevel * PerLevel + tier + input.StationLevel * PerStationLevel + input.PartsBonus;
        }
    }
}
