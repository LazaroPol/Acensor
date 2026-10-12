using System;

namespace Specimen.Core.Professions
{
    /// <summary>Lo que abre un nivel de oficio: tier de material, bonus de tiempo y de calidad máxima.</summary>
    public sealed class ProfessionMilestone
    {
        /// <summary>Calidad máxima sin hitos (grado 3, Buena; GDD §17.1).</summary>
        public const int BaseMaxQuality = 3;

        public ProfessionMilestone(int level, int materialTier, float timeBonus, int maxQualityBonus)
        {
            if (level < 1) throw new ArgumentOutOfRangeException(nameof(level));
            if (materialTier < 1) throw new ArgumentOutOfRangeException(nameof(materialTier));
            if (timeBonus < 0f || timeBonus >= 1f) throw new ArgumentOutOfRangeException(nameof(timeBonus));
            if (maxQualityBonus < 0) throw new ArgumentOutOfRangeException(nameof(maxQualityBonus));
            Level = level;
            MaterialTier = materialTier;
            TimeBonus = timeBonus;
            MaxQualityBonus = maxQualityBonus;
        }

        public int Level { get; }
        public int MaterialTier { get; }

        /// <summary>Reducción del tiempo de fabricación (0,1 = −10 %).</summary>
        public float TimeBonus { get; }
        public int MaxQualityBonus { get; }
        public int MaxQuality => BaseMaxQuality + MaxQualityBonus;
    }
}
