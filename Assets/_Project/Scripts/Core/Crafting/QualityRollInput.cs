using System;
using System.Collections.Generic;
using Specimen.Core.Items;

namespace Specimen.Core.Crafting
{
    /// <summary>Entradas de una tirada de calidad: palancas del jugador y topes (hito y receta).</summary>
    public sealed class QualityRollInput
    {
        public QualityRollInput(int professionLevel, float averageTier, int stationLevel, int partsBonus,
            int maxQuality, Quality recipeCap = Quality.Legendary)
        {
            if (professionLevel < 1) throw new ArgumentOutOfRangeException(nameof(professionLevel));
            if (averageTier < 0f) throw new ArgumentOutOfRangeException(nameof(averageTier));
            if (stationLevel < 0) throw new ArgumentOutOfRangeException(nameof(stationLevel));
            if (maxQuality < 1) throw new ArgumentOutOfRangeException(nameof(maxQuality));
            if (recipeCap < Quality.Crude) throw new ArgumentOutOfRangeException(nameof(recipeCap));
            ProfessionLevel = professionLevel;
            AverageTier = averageTier;
            StationLevel = stationLevel;
            PartsBonus = partsBonus;
            MaxQuality = maxQuality;
            RecipeCap = recipeCap;
        }

        public int ProfessionLevel { get; }

        /// <summary>Tier medio de los materiales, ponderado por cantidad.</summary>
        public float AverageTier { get; }
        public int StationLevel { get; }
        public int PartsBonus { get; }

        /// <summary>Calidad máxima: la del hito de oficio más el bonus de pieza.</summary>
        public int MaxQuality { get; }
        public Quality RecipeCap { get; }

        /// <summary>Media de tier ponderada por cantidad; 0 si no hay ingredientes.</summary>
        public static float WeightedTier(IEnumerable<(int Tier, int Quantity)> ingredients)
        {
            if (ingredients == null) throw new ArgumentNullException(nameof(ingredients));
            long sum = 0, count = 0;
            foreach (var (tier, quantity) in ingredients)
            {
                if (quantity <= 0) continue;
                sum += (long)tier * quantity;
                count += quantity;
            }
            return count == 0 ? 0f : (float)sum / count;
        }
    }
}
