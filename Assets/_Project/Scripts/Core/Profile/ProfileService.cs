using System;
using System.Linq;
using Specimen.Core.Dna;
using Specimen.Core.Lore;

namespace Specimen.Core.Profile
{
    /// <summary>Guarda y carga el perfil (ADN + fragmentos) en un <see cref="IProfileStore"/>.</summary>
    public sealed class ProfileService
    {
        private readonly IProfileStore _store;

        public ProfileService(IProfileStore store) => _store = store ?? throw new ArgumentNullException(nameof(store));

        public void Save(DnaAllocation dna, MemoryArchive archive)
        {
            if (dna == null) throw new ArgumentNullException(nameof(dna));
            if (archive == null) throw new ArgumentNullException(nameof(archive));
            var data = new ProfileData(dna.FreePoints, dna.Unlocked.ToArray(), archive.Fragments.ToArray());
            _store.Write(ProfileSerializer.Serialize(data));
        }

        /// <summary>Carga sobre los objetos dados. Devuelve false (sin tocarlos) si no hay perfil guardado.</summary>
        public bool Load(DnaAllocation dna, MemoryArchive archive)
        {
            if (dna == null) throw new ArgumentNullException(nameof(dna));
            if (archive == null) throw new ArgumentNullException(nameof(archive));
            var text = _store.Read();
            if (string.IsNullOrEmpty(text)) return false;
            var data = ProfileSerializer.Deserialize(text);
            dna.Restore(data.FreeDnaPoints, data.UnlockedNodes);
            foreach (var id in data.Fragments) archive.Collect(id);
            return true;
        }
    }
}
