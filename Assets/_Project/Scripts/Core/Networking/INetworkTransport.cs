namespace Specimen.Core.Networking
{
    /// <summary>Puerto hacia la librería de red (NGO en Runtime). Devuelve false si no pudo arrancar.</summary>
    public interface INetworkTransport
    {
        bool StartHost(SessionSettings settings);
        bool StartServer(SessionSettings settings);
        bool StartClient(SessionEndpoint endpoint);
        void Shutdown();
    }
}
