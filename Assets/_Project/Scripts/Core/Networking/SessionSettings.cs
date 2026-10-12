using System;
using Specimen.Core.Common;

namespace Specimen.Core.Networking
{
    /// <summary>Ajustes de una sesión: dónde y para cuántos jugadores (coop 1-6).</summary>
    public sealed class SessionSettings
    {
        public const int MinPlayers = 1;
        public const int MaxPlayersLimit = 6;

        public SessionEndpoint Endpoint { get; }
        public int MaxPlayers { get; }

        public SessionSettings(SessionEndpoint endpoint, int maxPlayers = MaxPlayersLimit)
        {
            Endpoint = Guard.NotNull(endpoint, nameof(endpoint));
            if (maxPlayers < MinPlayers || maxPlayers > MaxPlayersLimit)
                throw new ArgumentOutOfRangeException(nameof(maxPlayers), maxPlayers, "Jugadores fuera de rango (1-6).");
            MaxPlayers = maxPlayers;
        }

        /// <summary>Conexiones remotas admitidas por un host (él ocupa una plaza).</summary>
        public int RemoteSlotsAsHost => MaxPlayers - 1;
    }
}
