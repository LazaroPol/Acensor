using System;

namespace Specimen.Core.Networking
{
    /// <summary>Dirección y puerto de la sesión (validados).</summary>
    public sealed class SessionEndpoint
    {
        public const ushort DefaultPort = 7777;

        public string Address { get; }
        public ushort Port { get; }

        public SessionEndpoint(string address, int port)
        {
            if (string.IsNullOrWhiteSpace(address))
                throw new ArgumentException("La dirección no puede estar vacía.", nameof(address));
            if (port < 1 || port > ushort.MaxValue)
                throw new ArgumentOutOfRangeException(nameof(port), port, "Puerto fuera de rango (1-65535).");
            Address = address.Trim();
            Port = (ushort)port;
        }

        /// <summary>Host local en el puerto por defecto.</summary>
        public static SessionEndpoint Local() => new SessionEndpoint("127.0.0.1", DefaultPort);

        public override string ToString() => $"{Address}:{Port}";
    }
}
