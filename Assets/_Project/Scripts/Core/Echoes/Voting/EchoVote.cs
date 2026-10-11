using System;
using System.Collections.Generic;
using Specimen.Core.Common;

namespace Specimen.Core.Echoes.Voting
{
    /// <summary>
    /// Voto de un Eco entre varias opciones (GDD §9): gana la más votada; en empate decide, entre los
    /// votantes de las opciones empatadas, el que tenga más Fragmentos (y si persiste, la opción de menor índice).
    /// </summary>
    public sealed class EchoVote
    {
        private readonly HashSet<int> _voters;
        private readonly Dictionary<int, int> _ballots = new Dictionary<int, int>();
        private readonly IFragmentSource _fragments;

        public EchoVote(IEnumerable<int> voters, int optionCount, IFragmentSource fragments)
        {
            _voters = new HashSet<int>(Guard.NotNull(voters, nameof(voters)));
            if (_voters.Count == 0) throw new ArgumentException("El censo está vacío.", nameof(voters));
            if (optionCount < 2) throw new ArgumentOutOfRangeException(nameof(optionCount));
            OptionCount = optionCount;
            _fragments = Guard.NotNull(fragments, nameof(fragments));
        }

        public int OptionCount { get; }
        public bool IsComplete => _ballots.Count == _voters.Count;

        public void Cast(int voter, int option)
        {
            if (!_voters.Contains(voter)) throw new ArgumentException($"Votante desconocido: {voter}", nameof(voter));
            if (option < 0 || option >= OptionCount) throw new ArgumentOutOfRangeException(nameof(option));
            _ballots[voter] = option;
        }

        public int CountFor(int option)
        {
            var n = 0;
            foreach (var o in _ballots.Values) if (o == option) n++;
            return n;
        }

        public EchoVoteResult Resolve()
        {
            if (_ballots.Count == 0) throw new InvalidOperationException("Nadie ha votado.");
            var best = 0;
            for (var o = 0; o < OptionCount; o++) best = Math.Max(best, CountFor(o));
            var tied = new List<int>();
            for (var o = 0; o < OptionCount; o++) if (CountFor(o) == best) tied.Add(o);
            return tied.Count == 1 ? new EchoVoteResult(tied[0], false) : new EchoVoteResult(BreakTie(tied), true);
        }

        private int BreakTie(List<int> tied)
        {
            var winner = tied[0];
            var most = int.MinValue;
            foreach (var option in tied)
                foreach (var ballot in _ballots)
                {
                    if (ballot.Value != option) continue;
                    var f = _fragments.FragmentsOf(ballot.Key);
                    if (f > most || (f == most && option < winner)) { most = f; winner = option; }
                }
            return winner;
        }
    }
}
