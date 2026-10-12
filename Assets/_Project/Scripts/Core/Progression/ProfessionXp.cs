using Specimen.Core.Common;
using Specimen.Core.Professions;

namespace Specimen.Core.Progression
{
    /// <summary>
    /// XP de oficio al terminar una fabricación (GDD §17.1): recetas muy por debajo del nivel dan menos
    /// (gris 10 %, verde 50 %, amarillo o más 100 %), con el mismo color que <see cref="LevelDifficulty"/>.
    /// </summary>
    public static class ProfessionXp
    {
        public static float Multiplier(int skillLevel, int recipeLevel)
        {
            switch (LevelDifficulty.Of(skillLevel, recipeLevel))
            {
                case Difficulty.Gris: return 0.1f;
                case Difficulty.Verde: return 0.5f;
                default: return 1f;
            }
        }

        public static int Scaled(int recipeXp, int skillLevel, int recipeLevel)
        {
            Guard.NonNegative(recipeXp, nameof(recipeXp));
            return (int)System.Math.Round(recipeXp * Multiplier(skillLevel, recipeLevel));
        }

        /// <summary>Aplica la XP reducida al oficio y devuelve los niveles subidos.</summary>
        public static int Award(ProfessionSkill skill, int recipeXp, int recipeLevel)
        {
            Guard.NotNull(skill, nameof(skill));
            return skill.Gain(Scaled(recipeXp, skill.Level, recipeLevel));
        }
    }
}
