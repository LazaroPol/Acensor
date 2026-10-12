using System;
using Specimen.Core.Common;

namespace Specimen.Core.Networking
{
    /// <summary>
    /// Ciclo de vida de la sesión sobre un <see cref="INetworkTransport"/>: arranca como host, servidor
    /// o cliente una sola vez, vuelve a Offline si el transporte falla o al apagar y avisa de cada cambio de rol.
    /// </summary>
    public sealed class NetworkSession : INetworkSession
    {
        private readonly INetworkTransport _transport;

        public NetworkRole Role { get; private set; } = NetworkRole.Offline;
        public bool IsAuthority => NetworkAuthority.IsAuthority(Role);
        public bool IsRunning => Role != NetworkRole.Offline;

        public event Action<NetworkRole> RoleChanged;

        public NetworkSession(INetworkTransport transport) => _transport = Guard.NotNull(transport, nameof(transport));

        public bool StartHost(SessionSettings settings) =>
            Start(NetworkRole.Host, () => _transport.StartHost(Guard.NotNull(settings, nameof(settings))));

        public bool StartServer(SessionSettings settings) =>
            Start(NetworkRole.Server, () => _transport.StartServer(Guard.NotNull(settings, nameof(settings))));

        public bool StartClient(SessionEndpoint endpoint) =>
            Start(NetworkRole.Client, () => _transport.StartClient(Guard.NotNull(endpoint, nameof(endpoint))));

        /// <summary>Cierra la sesión; sin efecto si ya estaba Offline.</summary>
        public void Shutdown()
        {
            if (!IsRunning) return;
            _transport.Shutdown();
            SetRole(NetworkRole.Offline);
        }

        /// <summary>El transporte perdió la conexión por su cuenta (p. ej. el host se fue).</summary>
        public void NotifyDisconnected()
        {
            if (IsRunning) SetRole(NetworkRole.Offline);
        }

        private bool Start(NetworkRole role, Func<bool> start)
        {
            if (IsRunning)
                throw new InvalidOperationException($"La sesión ya está en marcha como {Role}.");
            if (!start()) return false;
            SetRole(role);
            return true;
        }

        private void SetRole(NetworkRole role)
        {
            Role = role;
            RoleChanged?.Invoke(role);
        }
    }
}
