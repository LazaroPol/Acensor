namespace Specimen.Core.Networking
{
    /// <summary>Papel de esta instancia en la sesión de red.</summary>
    public enum NetworkRole
    {
        Offline,
        Host,
        Server,
        Client
    }

    /// <summary>Reglas de autoridad: el servidor (o la partida en solitario) es quien muta el estado.</summary>
    public static class NetworkAuthority
    {
        public static bool IsAuthority(NetworkRole role) => role != NetworkRole.Client;

        /// <summary>Hay simulación local de jugador (todos menos el servidor dedicado).</summary>
        public static bool HasLocalPlayer(NetworkRole role) => role != NetworkRole.Server;
    }
}
