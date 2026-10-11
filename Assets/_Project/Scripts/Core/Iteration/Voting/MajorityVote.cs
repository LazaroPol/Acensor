using System;
using System.Collections.Generic;
using Specimen.Core.Common;

namespace Specimen.Core.Iteration.Voting
{
    /// <summary>Votación sí/no por mayoría estricta del censo (más de la mitad vota sí). Se puede cambiar el voto.</summary>
    public sealed class MajorityVote
    {
        private readonly HashSet<int> _voters;
        private readonly Dictionary<int, bool> _ballots = new Dictionary<int, bool>();

        public MajorityVote(IEnumerable<int> voters)
        {
            _voters = new HashSet<int>(Guard.NotNull(voters, nameof(voters)));
            if (_voters.Count == 0) throw new ArgumentException("El censo está vacío.", nameof(voters));
        }

        public int VoterCount => _voters.Count;

        public int YesCount
        {
            get
            {
                int yes = 0;
                foreach (var b in _ballots.Values) if (b) yes++;
                return yes;
            }
        }

        public bool HasPassed => YesCount * 2 > _voters.Count;

        public void Cast(int voter, bool yes)
        {
            if (!_voters.Contains(voter)) throw new ArgumentException($"Votante desconocido: {voter}", nameof(voter));
            _ballots[voter] = yes;
        }

        public void Reset() => _ballots.Clear();
    }
}
