using Specimen.Core.Networking;

namespace Specimen.Tests.Core.Networking
{
    internal sealed class FakeTransport : INetworkTransport
    {
        public bool Succeeds = true;
        public int Shutdowns;
        public SessionSettings LastSettings;
        public SessionEndpoint LastEndpoint;

        public bool StartHost(SessionSettings settings) { LastSettings = settings; return Succeeds; }
        public bool StartServer(SessionSettings settings) { LastSettings = settings; return Succeeds; }
        public bool StartClient(SessionEndpoint endpoint) { LastEndpoint = endpoint; return Succeeds; }
        public void Shutdown() => Shutdowns++;
    }
}
