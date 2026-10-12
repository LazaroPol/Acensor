using System;

namespace Specimen.Core.Echoes.Secrets
{
    /// <summary>Decisión secreta de un jugador sobre un Eco: se oculta al resto hasta el piso de revelación.</summary>
    public sealed class SecretDecision
    {
        public SecretDecision(int voter, string echoId, int floor, int option, int revealFloor)
        {
            if (string.IsNullOrWhiteSpace(echoId))
                throw new ArgumentException("El id del Eco no puede estar vacío.", nameof(echoId));
            if (floor < 1) throw new ArgumentOutOfRangeException(nameof(floor));
            if (option < 0) throw new ArgumentOutOfRangeException(nameof(option));
            if (revealFloor <= floor)
                throw new ArgumentOutOfRangeException(nameof(revealFloor), "La revelación debe ser en un piso posterior.");
            Voter = voter;
            EchoId = echoId;
            Floor = floor;
            Option = option;
            RevealFloor = revealFloor;
        }

        public int Voter { get; }
        public string EchoId { get; }
        public int Floor { get; }
        public int Option { get; }
        public int RevealFloor { get; }

        public bool IsRevealedAt(int currentFloor) => currentFloor >= RevealFloor;
    }
}
