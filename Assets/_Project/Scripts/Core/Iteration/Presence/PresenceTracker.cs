using System;
using System.Collections.Generic;
using System.Linq;
using Specimen.Core.Common;

namespace Specimen.Core.Iteration.Presence
{
    /// <summary>
    /// Ubicación de cada jugador. Quien sigue en el piso cuando se cierran las puertas queda Atrapado
    /// hasta alcanzar el Hueco de Emergencia (GDD §3.2).
    /// </summary>
    public sealed class PresenceTracker
    {
        private readonly Dictionary<int, PlayerLocation> _locations;

        public PresenceTracker(IEnumerable<int> playerIds)
        {
            _locations = Guard.NotNull(playerIds, nameof(playerIds)).Distinct()
                .ToDictionary(id => id, _ => PlayerLocation.InElevator);
        }

        public PlayerLocation Location(int playerId) =>
            _locations.TryGetValue(playerId, out var loc) ? loc : throw Unknown(playerId);

        public IReadOnlyList<int> With(PlayerLocation location) =>
            _locations.Where(kv => kv.Value == location).Select(kv => kv.Key).OrderBy(id => id).ToList();

        /// <summary>Jugadores que no están atrapados (en el ascensor o en el piso).</summary>
        public IReadOnlyList<int> NotTrapped =>
            _locations.Where(kv => kv.Value != PlayerLocation.Trapped).Select(kv => kv.Key).OrderBy(id => id).ToList();

        public void LeaveElevator(int playerId) => Move(playerId, PlayerLocation.InElevator, PlayerLocation.InFloor);

        public void ReturnToElevator(int playerId) => Move(playerId, PlayerLocation.InFloor, PlayerLocation.InElevator);

        public void Rescue(int playerId) => Move(playerId, PlayerLocation.Trapped, PlayerLocation.InElevator);

        /// <summary>Cierra las puertas: quien esté en el piso pasa a Atrapado. Devuelve los nuevos atrapados.</summary>
        public IReadOnlyList<int> TrapThoseOutside()
        {
            var outside = With(PlayerLocation.InFloor);
            foreach (var id in outside) _locations[id] = PlayerLocation.Trapped;
            return outside;
        }

        private void Move(int playerId, PlayerLocation from, PlayerLocation to)
        {
            var current = Location(playerId);
            if (current != from)
                throw new InvalidOperationException($"Jugador {playerId}: está en {current}, se esperaba {from}.");
            _locations[playerId] = to;
        }

        private static ArgumentException Unknown(int id) => new ArgumentException($"Jugador desconocido: {id}");
    }
}
