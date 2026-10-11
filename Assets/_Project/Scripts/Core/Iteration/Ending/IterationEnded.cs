using System.Collections.Generic;

namespace Specimen.Core.Iteration.Ending
{
    /// <summary>La iteración ha terminado. Base para la puntuación y la vuelta al Laboratorio (F8/F9).</summary>
    public readonly struct IterationEnded
    {
        public readonly IterationOutcome Outcome;
        public readonly int Floor;
        public readonly IReadOnlyDictionary<int, PlayerFate> Fates;

        public IterationEnded(IterationOutcome outcome, int floor, IReadOnlyDictionary<int, PlayerFate> fates)
        {
            Outcome = outcome;
            Floor = floor;
            Fates = fates;
        }
    }
}
