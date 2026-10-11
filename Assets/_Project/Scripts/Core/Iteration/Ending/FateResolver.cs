using System.Collections.Generic;
using Specimen.Core.Iteration.Presence;

namespace Specimen.Core.Iteration.Ending
{
    /// <summary>Decide el destino de cada jugador a partir del resultado y su ubicación final (función pura).</summary>
    public static class FateResolver
    {
        public static IReadOnlyDictionary<int, PlayerFate> Resolve(IterationOutcome outcome, PresenceTracker presence)
        {
            var fates = new Dictionary<int, PlayerFate>();
            foreach (var id in presence.All)
                fates[id] = FateOf(outcome, presence.Location(id));
            return fates;
        }

        public static PlayerFate FateOf(IterationOutcome outcome, PlayerLocation location)
        {
            if (location == PlayerLocation.Eliminated) return PlayerFate.Eliminated;
            if (outcome == IterationOutcome.Abandoned) return PlayerFate.Abandoned;
            return location == PlayerLocation.InElevator ? PlayerFate.Extracted : PlayerFate.LeftBehind;
        }
    }
}
