using System;
using System.Collections.Generic;
using System.Linq;

namespace Specimen.Core.Lore
{
    /// <summary>Entrada del lore que se desbloquea al reunir todos sus fragmentos de memoria.</summary>
    public sealed class LoreEntry
    {
        public string Id { get; }
        public string Title { get; }
        public IReadOnlyList<string> RequiredFragments { get; }

        public LoreEntry(string id, string title, params string[] requiredFragments)
        {
            if (string.IsNullOrWhiteSpace(id)) throw new ArgumentException("Id obligatorio.", nameof(id));
            if (requiredFragments == null || requiredFragments.Length == 0)
                throw new ArgumentException("Se necesita al menos un fragmento.", nameof(requiredFragments));
            Id = id;
            Title = title ?? id;
            RequiredFragments = requiredFragments.Distinct().ToArray();
        }
    }
}
