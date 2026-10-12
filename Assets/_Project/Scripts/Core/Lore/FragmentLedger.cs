using System;
using System.Collections.Generic;
using Specimen.Core.Echoes.Voting;

namespace Specimen.Core.Lore
{
    /// <summary>Fragmentos de memoria recogidos por cada jugador; sirve de desempate en las votaciones.</summary>
    public sealed class FragmentLedger : IFragmentSource
    {
        private readonly Dictionary<int, HashSet<string>> _byPlayer = new Dictionary<int, HashSet<string>>();

        public bool Collect(int player, string fragmentId)
        {
            if (string.IsNullOrWhiteSpace(fragmentId)) throw new ArgumentException("Id obligatorio.", nameof(fragmentId));
            if (!_byPlayer.TryGetValue(player, out var set)) _byPlayer[player] = set = new HashSet<string>();
            return set.Add(fragmentId);
        }

        public int FragmentsOf(int player) => _byPlayer.TryGetValue(player, out var set) ? set.Count : 0;
    }
}
