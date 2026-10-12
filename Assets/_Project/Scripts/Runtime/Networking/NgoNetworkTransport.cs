using Specimen.Core.Networking;
using Unity.Netcode;
using Unity.Netcode.Transports.UTP;

namespace Specimen.Runtime.Networking
{
    /// <summary>
    /// Adaptador de Netcode for GameObjects a <see cref="INetworkTransport"/>: configura UnityTransport
    /// con el punto de conexión y arranca el NetworkManager en el modo pedido.
    /// </summary>
    public sealed class NgoNetworkTransport : INetworkTransport
    {
        private readonly NetworkManager _manager;
        private int _maxRemoteClients = SessionSettings.MaxPlayersLimit;

        public NgoNetworkTransport(NetworkManager manager)
        {
            _manager = manager;
            _manager.ConnectionApprovalCallback += Approve;
        }

        public bool StartHost(SessionSettings settings)
        {
            Configure(settings.Endpoint, settings.RemoteSlotsAsHost);
            return _manager.StartHost();
        }

        public bool StartServer(SessionSettings settings)
        {
            Configure(settings.Endpoint, settings.MaxPlayers);
            return _manager.StartServer();
        }

        public bool StartClient(SessionEndpoint endpoint)
        {
            Configure(endpoint, SessionSettings.MaxPlayersLimit);
            return _manager.StartClient();
        }

        public void Shutdown() => _manager.Shutdown();

        private void Configure(SessionEndpoint endpoint, int maxRemoteClients)
        {
            _maxRemoteClients = maxRemoteClients;
            _manager.NetworkConfig.ConnectionApproval = true;
            var transport = _manager.GetComponent<UnityTransport>();
            transport.SetConnectionData(endpoint.Address, endpoint.Port, "0.0.0.0");
        }

        /// <summary>Rechaza conexiones remotas por encima de la capacidad de la sesión (el host no cuenta).</summary>
        private void Approve(NetworkManager.ConnectionApprovalRequest request, NetworkManager.ConnectionApprovalResponse response)
        {
            var remotes = _manager.ConnectedClientsIds.Count - (_manager.IsHost ? 1 : 0);
            var isLocalHost = request.ClientNetworkId == NetworkManager.ServerClientId;
            response.Approved = isLocalHost || remotes < _maxRemoteClients;
            response.CreatePlayerObject = true;
            if (!response.Approved) response.Reason = "Sesión completa";
        }
    }
}
