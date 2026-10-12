using System;

namespace Specimen.Core.Networking
{
    /// <summary>Estado de red visible para el juego: rol actual y autoridad.</summary>
    public interface INetworkSession
    {
        NetworkRole Role { get; }
        bool IsAuthority { get; }
        bool IsRunning { get; }
        event Action<NetworkRole> RoleChanged;
    }
}
