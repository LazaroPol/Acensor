using System;
using Specimen.Core.Common;

namespace Specimen.Core.Siege
{
    /// <summary>Parámetros del asedio: oleadas a sobrevivir, tamaño inicial, crecimiento y pausa entre oleadas.</summary>
    public sealed class WaveConfig
    {
        public WaveConfig(int waveCount, int firstWaveSize, int growthPerWave, float intermissionSeconds)
        {
            WaveCount = Guard.Positive(waveCount, nameof(waveCount));
            FirstWaveSize = Guard.Positive(firstWaveSize, nameof(firstWaveSize));
            GrowthPerWave = Guard.NonNegative(growthPerWave, nameof(growthPerWave));
            IntermissionSeconds = Guard.NonNegative(intermissionSeconds, nameof(intermissionSeconds));
        }

        public int WaveCount { get; }
        public int FirstWaveSize { get; }
        public int GrowthPerWave { get; }
        public float IntermissionSeconds { get; }

        /// <summary>Enemigos de la oleada (1-based).</summary>
        public int SizeOf(int wave)
        {
            if (wave < 1 || wave > WaveCount) throw new ArgumentOutOfRangeException(nameof(wave));
            return FirstWaveSize + GrowthPerWave * (wave - 1);
        }

        /// <summary>Vertical slice: 5 oleadas (GDD 15).</summary>
        public static WaveConfig Default => new WaveConfig(5, 6, 3, 20f);
    }
}
