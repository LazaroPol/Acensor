using System.Collections.Generic;
using System.Linq;
using Specimen.Core.Common;

namespace Specimen.Core.Progression
{
    /// <summary>Libro de hechizos de un kit: todos sus hechizos ordenados por nivel y los ya aprendidos a un nivel dado.</summary>
    public sealed class Spellbook
    {
        private readonly List<SpellDefinition> _spells;

        public Spellbook(IEnumerable<SpellDefinition> spells)
            => _spells = Guard.NotNull(spells, nameof(spells)).OrderBy(s => s.Level).ToList();

        public IReadOnlyList<SpellDefinition> All => _spells;

        public IReadOnlyList<SpellDefinition> KnownAt(int level) => _spells.Where(s => s.Level <= level).ToList();

        /// <summary>Hechizos que se aprenden justo al llegar a <paramref name="level"/>.</summary>
        public IReadOnlyList<SpellDefinition> LearnedAt(int level) => _spells.Where(s => s.Level == level).ToList();
    }
}
