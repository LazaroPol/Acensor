using Specimen.Core.Professions;

namespace Specimen.Core.Crafting.Save
{
    /// <summary>
    /// Trabajo en cola con sus segundos restantes y el oficio/nivel del artesano:
    /// <c>job=fragua_aldea:herrero.espada_acero:12.5:Herrero:7</c>.
    /// </summary>
    public sealed class JobEntry
    {
        public string Station { get; }
        public string RecipeId { get; }
        public double RemainingSeconds { get; }
        public ProfessionId Profession { get; }
        public int Level { get; }

        public JobEntry(string station, string recipeId, double remainingSeconds, ProfessionId profession, int level)
        {
            Station = station;
            RecipeId = recipeId;
            RemainingSeconds = remainingSeconds;
            Profession = profession;
            Level = level;
        }
    }
}
