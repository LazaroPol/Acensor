using System;
using System.Collections.Generic;
using System.Linq;
using Specimen.Core.Common;
using Specimen.Core.Floors.Omens;

namespace Specimen.Core.Echoes.Consequences
{
    /// <summary>Consecuencias de los Ecos y modificadores que provocan en un piso según lo decidido.</summary>
    public sealed class EchoConsequenceTable
    {
        private readonly List<EchoConsequence> _consequences;

        public EchoConsequenceTable(IEnumerable<EchoConsequence> consequences)
        {
            _consequences = Guard.NotNull(consequences, nameof(consequences)).ToList();
            if (_consequences.Any(c => c == null)) throw new ArgumentException("Consecuencia nula.", nameof(consequences));
        }

        /// <summary>Modificadores sin repetir que las decisiones ya tomadas imponen al piso, en orden de registro.</summary>
        public IReadOnlyList<FloorModifierKind> ModifiersFor(EchoRegistry registry, int floor)
        {
            Guard.NotNull(registry, nameof(registry));
            return _consequences.Where(c => c.AppliesTo(registry, floor)).Select(c => c.Modifier).Distinct().ToList();
        }
    }
}
