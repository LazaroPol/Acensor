using System.Collections.Generic;
using System.Linq;
using Specimen.Core.Common;

namespace Specimen.Core.Talents
{
    /// <summary>Rama de un árbol de talentos (p. ej. «Protección»): nombre, lema y talentos por fila.</summary>
    public sealed class TalentBranch
    {
        public TalentBranch(string name, string motto, IEnumerable<TalentDefinition> talents)
        {
            Name = Guard.NotNull(name, nameof(name));
            Motto = Guard.NotNull(motto, nameof(motto));
            Talents = Guard.NotNull(talents, nameof(talents)).ToList();
        }

        public string Name { get; }
        public string Motto { get; }
        public IReadOnlyList<TalentDefinition> Talents { get; }

        public IEnumerable<TalentDefinition> InTier(int tier) => Talents.Where(t => t.Tier == tier);
    }
}
