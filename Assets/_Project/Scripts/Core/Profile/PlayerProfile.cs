using Specimen.Core.Common;
using Specimen.Core.Dna;
using Specimen.Core.Lore;
using Specimen.Core.Stats;

namespace Specimen.Core.Profile
{
    /// <summary>
    /// Perfil del jugador entre partidas: árbol de ADN, puntos y fragmentos de memoria, guardado en un
    /// <see cref="IProfileStore"/>. Sobrevive a «nueva partida» porque se recarga del almacén.
    /// </summary>
    public sealed class PlayerProfile
    {
        private readonly ProfileService _service;

        private PlayerProfile(ProfileService service, DnaTree tree, DnaAllocation dna, MemoryArchive archive)
        {
            _service = service;
            Tree = tree;
            Dna = dna;
            Archive = archive;
        }

        public DnaTree Tree { get; }
        public DnaAllocation Dna { get; }
        public MemoryArchive Archive { get; }

        /// <summary>Crea el perfil con el árbol por defecto y carga lo guardado, si hay algo.</summary>
        public static PlayerProfile Load(IProfileStore store)
        {
            Guard.NotNull(store, nameof(store));
            var tree = DnaTree.CreateDefault();
            var dna = new DnaAllocation(tree, TierLock.CreateDefault());
            var archive = new MemoryArchive(new LoreEntry[0]);
            var service = new ProfileService(store);
            service.Load(dna, archive);
            return new PlayerProfile(service, tree, dna, archive);
        }

        public void Save() => _service.Save(Dna, Archive);

        /// <summary>Aplica las mutaciones compradas a la hoja de un clon nuevo; devuelve cuántas aplicó.</summary>
        public int ApplyTo(StatSheet sheet) => new DnaMutationApplier(Tree, new DefaultDnaEffects()).Apply(sheet, Dna);

        /// <summary>Suma los puntos de ADN ganados en una iteración y guarda.</summary>
        public void Reward(int dnaPoints)
        {
            Dna.AddPoints(dnaPoints);
            Save();
        }

        /// <summary>Desbloquea un nodo y, si lo consigue, guarda.</summary>
        public UnlockResult Unlock(string nodeId)
        {
            var result = Dna.TryUnlock(nodeId);
            if (result == UnlockResult.Ok) Save();
            return result;
        }
    }
}
