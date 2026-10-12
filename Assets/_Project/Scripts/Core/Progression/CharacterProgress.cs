using System;
using Specimen.Core.Common;

namespace Specimen.Core.Progression
{
    /// <summary>Nivel y experiencia del personaje. Cada nivel por encima del 1 concede un punto de talento.</summary>
    public sealed class CharacterProgress
    {
        public CharacterProgress(int level = 1, int xp = 0)
        {
            if (level < 1 || level > LevelCurve.MaxLevel) throw new ArgumentOutOfRangeException(nameof(level));
            Level = level;
            Xp = Guard.NonNegative(xp, nameof(xp));
        }

        /// <summary>Nivel nuevo alcanzado (se dispara una vez por nivel subido).</summary>
        public event Action<int> LeveledUp;

        public int Level { get; private set; }
        public int Xp { get; private set; }
        public int XpToNext => LevelCurve.XpToNext(Level);
        public bool IsMaxLevel => Level >= LevelCurve.MaxLevel;
        public float Fraction => IsMaxLevel ? 1f : (float)Xp / XpToNext;
        public int TalentPoints => Level - 1;

        /// <summary>Suma experiencia y sube los niveles que toque; devuelve cuántos ha subido.</summary>
        public int AddXp(int amount)
        {
            Guard.NonNegative(amount, nameof(amount));
            if (IsMaxLevel) return 0;
            Xp += amount;
            int gained = 0;
            while (!IsMaxLevel && Xp >= XpToNext)
            {
                Xp -= XpToNext;
                Level++;
                gained++;
                LeveledUp?.Invoke(Level);
            }
            if (IsMaxLevel) Xp = 0;
            return gained;
        }
    }
}
