using System;
using Specimen.Core.Common;

namespace Specimen.Core.Professions
{
    /// <summary>Nivel y XP de un personaje en un oficio. No se baja de nivel ni se pasa del máximo.</summary>
    public sealed class ProfessionSkill
    {
        public ProfessionSkill(ProfessionId profession, ProfessionCurve curve = null)
        {
            Profession = profession;
            Curve = curve ?? ProfessionCurve.Default;
        }

        public ProfessionId Profession { get; }
        public ProfessionCurve Curve { get; }
        public int Level { get; private set; } = 1;

        /// <summary>XP acumulada hacia el siguiente nivel (0 en el máximo).</summary>
        public int Xp { get; private set; }

        public bool IsMaxed => Level >= Curve.MaxLevel;

        /// <summary>Suma XP y devuelve cuántos niveles se han subido.</summary>
        public int Gain(int xp)
        {
            Guard.NonNegative(xp, nameof(xp));
            int before = Level;
            Xp += xp;
            while (!IsMaxed && Xp >= Curve.XpToNext(Level))
            {
                Xp -= Curve.XpToNext(Level);
                Level++;
            }
            if (IsMaxed) Xp = 0;
            return Level - before;
        }

        /// <summary>Restaura nivel y XP de un guardado (GDD §17.7); se ajustan a la curva.</summary>
        public void Restore(int level, int xp)
        {
            Level = Math.Max(1, Math.Min(level, Curve.MaxLevel));
            Xp = IsMaxed ? 0 : Math.Max(0, Math.Min(xp, Curve.XpToNext(Level) - 1));
        }
    }
}
