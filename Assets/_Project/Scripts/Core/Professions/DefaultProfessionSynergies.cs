using System.Collections.Generic;

namespace Specimen.Core.Professions
{
    /// <summary>Las 7 sinergias del GDD §7.</summary>
    public static class DefaultProfessionSynergies
    {
        public static IReadOnlyList<ProfessionSynergy> Create() => new[]
        {
            new ProfessionSynergy(ProfessionId.Quimico, ProfessionId.Cocinero, "Festín alquímico"),
            new ProfessionSynergy(ProfessionId.Herrero, ProfessionId.Ingeniero, "Armas mecánicas"),
            new ProfessionSynergy(ProfessionId.Sastre, ProfessionId.Quimico, "Telas ignífugas"),
            new ProfessionSynergy(ProfessionId.Escultor, ProfessionId.Ingeniero, "Gólem"),
            new ProfessionSynergy(ProfessionId.Manitas, ProfessionId.Ingeniero, "Hackear torretas"),
            new ProfessionSynergy(ProfessionId.Escultor, ProfessionId.Quimico, "Estatuas explosivas"),
            new ProfessionSynergy(ProfessionId.Herrero, ProfessionId.Sastre, "Armadura mixta")
        };
    }
}
