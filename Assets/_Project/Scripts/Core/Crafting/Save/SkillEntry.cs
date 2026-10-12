using Specimen.Core.Professions;

namespace Specimen.Core.Crafting.Save
{
    /// <summary>Nivel y XP de un oficio: <c>skill=Herrero:7:340</c>.</summary>
    public sealed class SkillEntry
    {
        public ProfessionId Profession { get; }
        public int Level { get; }
        public int Xp { get; }

        public SkillEntry(ProfessionId profession, int level, int xp)
        {
            Profession = profession;
            Level = level;
            Xp = xp;
        }
    }
}
