using System.Collections.Generic;
using Specimen.Core.Classes;
using Specimen.Core.Common;

namespace Specimen.Core.Armory
{
    /// <summary>
    /// Reparto de kits de clase entre jugadores: un kit por jugador y un máximo
    /// global de kits duplicados (GDD §4: en coop de 4, máx. 1 duplicado).
    /// </summary>
    public sealed class ClassKitArmory
    {
        public const int DefaultMaxDuplicates = 1;

        private readonly IClassKitCatalog _catalog;
        private readonly int _maxDuplicates;
        private readonly Dictionary<int, ClassKitId> _claims = new Dictionary<int, ClassKitId>();
        private readonly Dictionary<ClassKitId, int> _counts = new Dictionary<ClassKitId, int>();

        public ClassKitArmory(IClassKitCatalog catalog, int maxDuplicates = DefaultMaxDuplicates)
        {
            _catalog = Guard.NotNull(catalog, nameof(catalog));
            _maxDuplicates = Guard.NonNegative(maxDuplicates, nameof(maxDuplicates));
        }

        public int DuplicatesInUse { get; private set; }

        public int CountOf(ClassKitId id) => _counts.TryGetValue(id, out var n) ? n : 0;

        public bool CanClaim(ClassKitId id)
        {
            var count = CountOf(id);
            return count == 0 || DuplicatesInUse < _maxDuplicates;
        }

        /// <summary>Asigna el kit al jugador; falla si ya tiene uno o no hay stock.</summary>
        public bool TryClaim(int playerId, ClassKitId id)
        {
            if (_claims.ContainsKey(playerId) || !CanClaim(id)) return false;
            _catalog.Get(id);
            if (CountOf(id) > 0) DuplicatesInUse++;
            _counts[id] = CountOf(id) + 1;
            _claims[playerId] = id;
            return true;
        }

        public bool Release(int playerId)
        {
            if (!_claims.TryGetValue(playerId, out var id)) return false;
            _claims.Remove(playerId);
            _counts[id] = CountOf(id) - 1;
            if (CountOf(id) > 0) DuplicatesInUse--;
            return true;
        }

        public bool TryGetKit(int playerId, out ClassKit kit)
        {
            kit = _claims.TryGetValue(playerId, out var id) ? _catalog.Get(id) : null;
            return kit != null;
        }

        /// <summary>Vacía todas las asignaciones (nueva iteración).</summary>
        public void Reset()
        {
            _claims.Clear();
            _counts.Clear();
            DuplicatesInUse = 0;
        }
    }
}
