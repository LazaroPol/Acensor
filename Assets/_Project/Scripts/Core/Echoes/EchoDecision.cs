using System;

namespace Specimen.Core.Echoes
{
    /// <summary>Decisión tomada sobre un Eco: cuál era, en qué piso se resolvió y qué opción ganó.</summary>
    public sealed class EchoDecision
    {
        public EchoDecision(string echoId, int floor, int option)
        {
            if (string.IsNullOrWhiteSpace(echoId))
                throw new ArgumentException("El id del Eco no puede estar vacío.", nameof(echoId));
            if (floor < 1) throw new ArgumentOutOfRangeException(nameof(floor));
            if (option < 0) throw new ArgumentOutOfRangeException(nameof(option));
            EchoId = echoId;
            Floor = floor;
            Option = option;
        }

        public string EchoId { get; }
        public int Floor { get; }
        public int Option { get; }
    }
}
