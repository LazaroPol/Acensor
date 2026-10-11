using System;
using System.Collections.Generic;
using System.Linq;
using Specimen.Core.Common;

namespace Specimen.Core.Classes
{
    /// <summary>Catálogo en memoria de kits; rechaza ids duplicados.</summary>
    public sealed class ClassKitCatalog : IClassKitCatalog
    {
        private readonly Dictionary<ClassKitId, ClassKit> _byId = new Dictionary<ClassKitId, ClassKit>();
        private readonly List<ClassKit> _all = new List<ClassKit>();

        public ClassKitCatalog(IEnumerable<ClassKit> kits)
        {
            Guard.NotNull(kits, nameof(kits));
            foreach (var kit in kits)
            {
                Guard.NotNull(kit, nameof(kits));
                if (_byId.ContainsKey(kit.Id)) throw new ArgumentException($"Kit duplicado: {kit.Id}.", nameof(kits));
                _byId.Add(kit.Id, kit);
                _all.Add(kit);
            }
        }

        /// <summary>Los 6 kits del GDD.</summary>
        public static ClassKitCatalog CreateDefault() => new ClassKitCatalog(DefaultClassKits.Create());

        public IReadOnlyList<ClassKit> All => _all;

        public ClassKit Get(ClassKitId id)
            => _byId.TryGetValue(id, out var kit) ? kit : throw new KeyNotFoundException($"Kit no definido: {id}.");

        public IReadOnlyList<ClassKit> ByRole(ClassRole role) => _all.Where(k => k.Role == role).ToList();
    }
}
