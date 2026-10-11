using System.Collections.Generic;
using Specimen.Core.Common;

namespace Specimen.Core.Professions
{
    /// <summary>
    /// Banco de trabajo de la Armería: cada jugador elige exactamente una profesión
    /// (GDD §4). Se permiten duplicados; se puede cambiar hasta bloquear la selección.
    /// </summary>
    public sealed class ProfessionBench
    {
        private readonly ISynergyCatalog _synergies;
        private readonly Dictionary<int, ProfessionId> _choices = new Dictionary<int, ProfessionId>();

        public ProfessionBench(ISynergyCatalog synergies)
        {
            _synergies = Guard.NotNull(synergies, nameof(synergies));
        }

        public bool IsLocked { get; private set; }

        public int ChosenCount => _choices.Count;

        /// <summary>Elige o cambia la profesión del jugador; falla si está bloqueado.</summary>
        public bool TryChoose(int playerId, ProfessionId id)
        {
            if (IsLocked) return false;
            _choices[playerId] = id;
            return true;
        }

        public bool Release(int playerId) => !IsLocked && _choices.Remove(playerId);

        public bool TryGetProfession(int playerId, out ProfessionId id) =>
            _choices.TryGetValue(playerId, out id);

        public bool GroupHas(ProfessionId id) => _choices.ContainsValue(id);

        /// <summary>Bloquea la selección al salir de la Armería; exige que todos hayan elegido.</summary>
        public bool TryLock(IEnumerable<int> players)
        {
            foreach (var p in Guard.NotNull(players, nameof(players)))
                if (!_choices.ContainsKey(p)) return false;
            IsLocked = true;
            return true;
        }

        public IReadOnlyList<ProfessionSynergy> ActiveSynergies() => _synergies.AvailableFor(_choices.Values);

        /// <summary>Vacía y desbloquea (nueva iteración).</summary>
        public void Reset()
        {
            _choices.Clear();
            IsLocked = false;
        }
    }
}
