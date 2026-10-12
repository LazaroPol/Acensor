using System;
using System.Collections.Generic;
using System.Linq;

namespace Specimen.Core.Lore
{
    /// <summary>Fragmentos de memoria del perfil y entradas de lore que desbloquean (GDD §11).</summary>
    public sealed class MemoryArchive
    {
        private readonly IReadOnlyList<LoreEntry> _entries;
        private readonly HashSet<string> _fragments = new HashSet<string>();
        private readonly HashSet<string> _unlocked = new HashSet<string>();

        public MemoryArchive(IEnumerable<LoreEntry> entries)
        {
            _entries = (entries ?? throw new ArgumentNullException(nameof(entries))).ToArray();
            if (_entries.Select(e => e.Id).Distinct().Count() != _entries.Count)
                throw new ArgumentException("Ids de entrada duplicados.", nameof(entries));
        }

        public int FragmentCount => _fragments.Count;

        public IReadOnlyCollection<string> Fragments => _fragments;

        public IReadOnlyCollection<string> UnlockedEntries => _unlocked;

        public bool HasFragment(string id) => id != null && _fragments.Contains(id);

        public bool IsUnlocked(string entryId) => entryId != null && _unlocked.Contains(entryId);

        /// <summary>Guarda un fragmento y devuelve las entradas que se desbloquean con él (vacío si ya lo tenía).</summary>
        public IReadOnlyList<LoreEntry> Collect(string fragmentId)
        {
            if (string.IsNullOrWhiteSpace(fragmentId)) throw new ArgumentException("Id obligatorio.", nameof(fragmentId));
            if (!_fragments.Add(fragmentId)) return Array.Empty<LoreEntry>();
            var fresh = _entries.Where(e => !_unlocked.Contains(e.Id) && e.RequiredFragments.All(_fragments.Contains)).ToArray();
            foreach (var entry in fresh) _unlocked.Add(entry.Id);
            return fresh;
        }
    }
}
