using System.Collections.Generic;

namespace Specimen.Core.Iteration.Presence
{
    /// <summary>Las puertas se cerraron con estos jugadores fuera.</summary>
    public readonly struct PlayersTrapped
    {
        public readonly int Floor;
        public readonly IReadOnlyList<int> PlayerIds;

        public PlayersTrapped(int floor, IReadOnlyList<int> playerIds)
        {
            Floor = floor;
            PlayerIds = playerIds;
        }
    }

    /// <summary>Un jugador atrapado alcanzó el Hueco de Emergencia y vuelve al ascensor (con fatiga extra, F2-03).</summary>
    public readonly struct PlayerRescued
    {
        public readonly int PlayerId;
        public readonly int Floor;

        public PlayerRescued(int playerId, int floor)
        {
            PlayerId = playerId;
            Floor = floor;
        }
    }
}
