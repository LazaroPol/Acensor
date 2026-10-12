using Specimen.Core.Common;
using Specimen.Core.Crafting.Queue;
using Specimen.Core.Professions;

namespace Specimen.Core.Crafting.Session
{
    /// <summary>Aviso de trabajo terminado para el HUD: qué se fabricó y cuánta XP de oficio dio.</summary>
    public sealed class CraftCompletion
    {
        public CraftCompletion(CraftJob job, int levelsGained)
        {
            Job = Guard.NotNull(job, nameof(job));
            LevelsGained = levelsGained;
        }

        public CraftJob Job { get; }
        public ProfessionId Profession => Job.Crafter.Profession;
        public int Xp => Job.Recipe.Xp;
        public int Level => Job.Crafter.Level;
        public int LevelsGained { get; }

        public override string ToString() => $"{Job.Recipe.Id} +{Xp} XP {Profession}";
    }
}
