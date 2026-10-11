using System;
using System.Collections.Generic;
using System.Linq;
using Specimen.Core.Common;

namespace Specimen.Core.Iteration.Readiness
{
    /// <summary>Registro de qué jugadores han marcado "Listo" en la Calma.</summary>
    public sealed class ReadyCheck
    {
        private readonly HashSet<int> _players;
        private readonly HashSet<int> _ready = new HashSet<int>();

        public ReadyCheck(IEnumerable<int> playerIds)
        {
            _players = new HashSet<int>(Guard.NotNull(playerIds, nameof(playerIds)));
            if (_players.Count == 0) throw new ArgumentException("Hace falta al menos un jugador.", nameof(playerIds));
        }

        public IReadOnlyCollection<int> Players => _players;
        public int ReadyCount => _ready.Count;
        public bool AllReady => _ready.Count == _players.Count;

        /// <summary>True si hay alguien en el grupo dado y todos están listos (p. ej. ignorando a los atrapados).</summary>
        public bool AllReadyAmong(IEnumerable<int> playerIds)
        {
            bool any = false;
            foreach (var id in playerIds)
            {
                any = true;
                if (!_ready.Contains(id)) return false;
            }
            return any;
        }

        public bool IsReady(int playerId) => _ready.Contains(playerId);

        public void MarkReady(int playerId) => _ready.Add(Known(playerId));

        public void Unmark(int playerId) => _ready.Remove(Known(playerId));

        public void Reset() => _ready.Clear();

        private int Known(int playerId) =>
            _players.Contains(playerId)
                ? playerId
                : throw new ArgumentException($"Jugador desconocido: {playerId}", nameof(playerId));

        public override string ToString() => $"{_ready.Count}/{_players.Count} listos ({string.Join(",", _ready.OrderBy(p => p))})";
    }
}
