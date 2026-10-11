namespace Specimen.Core.Iteration
{
    /// <summary>La iteración ha cambiado de fase.</summary>
    public readonly struct IterationPhaseChanged
    {
        public readonly IterationPhase From;
        public readonly IterationPhase To;
        public readonly int Floor;

        public IterationPhaseChanged(IterationPhase from, IterationPhase to, int floor)
        {
            From = from;
            To = to;
            Floor = floor;
        }
    }

    /// <summary>El ascensor ha llegado a un piso nuevo (empieza su Calma).</summary>
    public readonly struct FloorReached
    {
        public readonly int Floor;
        public FloorReached(int floor) => Floor = floor;
    }

    /// <summary>Quedan <see cref="SecondsLeft"/> segundos para que se cierren las puertas.</summary>
    public readonly struct DoorsClosingWarning
    {
        public readonly int Floor;
        public readonly float SecondsLeft;

        public DoorsClosingWarning(int floor, float secondsLeft)
        {
            Floor = floor;
            SecondsLeft = secondsLeft;
        }
    }
}
