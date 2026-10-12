using System;
using System.Collections.Generic;
using System.Linq;
using Specimen.Core.Common;

namespace Specimen.Core.Siege.Extraction
{
    /// <summary>Extracción final (GDD §3.4): tras sobrevivir al asedio llega el Osprey, los jugadores suben y se parte.</summary>
    public sealed class OspreyExtraction
    {
        private readonly List<int> _players;
        private readonly List<int> _boarded = new List<int>();

        public OspreyExtraction(IEnumerable<int> players)
        {
            _players = Guard.NotNull(players, nameof(players)).Distinct().ToList();
            if (_players.Count == 0) throw new ArgumentException("No hay jugadores.", nameof(players));
        }

        public bool HasArrived { get; private set; }
        public bool HasDeparted { get; private set; }
        public bool AllBoarded => _boarded.Count == _players.Count;

        public void Arrive()
        {
            if (HasArrived) throw new InvalidOperationException("El Osprey ya llegó.");
            HasArrived = true;
        }

        /// <summary>Sube un jugador; false si ya estaba a bordo.</summary>
        public bool Board(int player)
        {
            if (!HasArrived || HasDeparted) throw new InvalidOperationException("El Osprey no está disponible.");
            if (!_players.Contains(player)) throw new ArgumentException($"Jugador desconocido: {player}", nameof(player));
            if (_boarded.Contains(player)) return false;
            _boarded.Add(player);
            return true;
        }

        /// <summary>Despega con quien haya subido; el resto se queda atrás.</summary>
        public ExtractionReport Depart()
        {
            if (!HasArrived) throw new InvalidOperationException("El Osprey aún no ha llegado.");
            if (HasDeparted) throw new InvalidOperationException("El Osprey ya despegó.");
            HasDeparted = true;
            return new ExtractionReport(_boarded.ToList(), _players.Except(_boarded).ToList());
        }
    }
}
