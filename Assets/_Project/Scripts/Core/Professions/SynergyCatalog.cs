using System.Collections.Generic;
using Specimen.Core.Common;

namespace Specimen.Core.Professions
{
    public sealed class SynergyCatalog : ISynergyCatalog
    {
        private readonly IReadOnlyList<ProfessionSynergy> _synergies;

        public SynergyCatalog(IReadOnlyList<ProfessionSynergy> synergies)
        {
            _synergies = Guard.NotNull(synergies, nameof(synergies));
        }

        public static SynergyCatalog CreateDefault() =>
            new SynergyCatalog(DefaultProfessionSynergies.Create());

        public IReadOnlyList<ProfessionSynergy> AvailableFor(IEnumerable<ProfessionId> group)
        {
            var present = new HashSet<ProfessionId>(Guard.NotNull(group, nameof(group)));
            var result = new List<ProfessionSynergy>();
            foreach (var s in _synergies)
                if (present.Contains(s.First) && present.Contains(s.Second)) result.Add(s);
            return result;
        }
    }
}
