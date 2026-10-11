using System.Collections.Generic;

namespace Specimen.Core.Professions
{
    /// <summary>Resuelve qué sinergias desbloquea un conjunto de profesiones.</summary>
    public interface ISynergyCatalog
    {
        IReadOnlyList<ProfessionSynergy> AvailableFor(IEnumerable<ProfessionId> group);
    }
}
