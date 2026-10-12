using System;

namespace Specimen.Core.Progression
{
    /// <summary>Curva de experiencia: XP para pasar de un nivel al siguiente (crece de forma suave) y nivel máximo.</summary>
    public static class LevelCurve
    {
        public const int MaxLevel = 20;

        public static int XpToNext(int level)
        {
            if (level < 1) throw new ArgumentOutOfRangeException(nameof(level), level, "El nivel empieza en 1.");
            if (level >= MaxLevel) return 0;
            return (int)Math.Round(80.0 * Math.Pow(level, 1.4) / 10.0) * 10;
        }
    }
}
