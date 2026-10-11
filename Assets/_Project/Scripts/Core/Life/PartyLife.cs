using System;
using System.Collections.Generic;
using System.Linq;
using Specimen.Core.Common;

namespace Specimen.Core.Life
{
    /// <summary>
    /// Estado vital del grupo: quién es Espectro y si han caído todos (fin de la iteración, GDD §3.3).
    /// </summary>
    public sealed class PartyLife
    {
        private readonly Dictionary<int, CharacterLife> _lives = new Dictionary<int, CharacterLife>();

        public event Action AllFallen;

        public int Count => _lives.Count;
        public bool AllSpectres => _lives.Count > 0 && _lives.Values.All(l => l.IsSpectre);

        public IReadOnlyList<int> Spectres => _lives.Where(p => p.Value.IsSpectre).Select(p => p.Key).ToList();

        public void Add(int playerId, CharacterLife life)
        {
            Guard.NotNull(life, nameof(life));
            if (_lives.ContainsKey(playerId))
                throw new ArgumentException($"El jugador {playerId} ya está registrado.", nameof(playerId));
            _lives.Add(playerId, life);
            life.Died += _ => OnDied();
        }

        public CharacterLife Of(int playerId) => _lives[playerId];

        private void OnDied()
        {
            if (AllSpectres) AllFallen?.Invoke();
        }
    }
}
