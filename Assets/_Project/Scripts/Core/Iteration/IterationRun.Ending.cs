using System.Collections.Generic;
using Specimen.Core.Iteration.Ending;

namespace Specimen.Core.Iteration
{
    /// <summary>Condiciones de fin: victoria por extracción, grupo eliminado o abandono.</summary>
    public sealed partial class IterationRun
    {
        private static readonly IterationPhase[] PlayablePhases =
        {
            IterationPhase.Armory, IterationPhase.Calm, IterationPhase.Incursion,
            IterationPhase.Ascent, IterationPhase.Siege, IterationPhase.Extraction
        };

        /// <summary>Resultado final, o null mientras la iteración sigue en marcha.</summary>
        public IterationOutcome? Outcome => Phase == IterationPhase.Ended ? _ctx.Outcome : null;

        public IReadOnlyDictionary<int, PlayerFate> Fates => _ctx.Fates;

        /// <summary>El clon de este jugador termina su iteración. Si caen todos, el grupo pierde.</summary>
        public void EliminatePlayer(int playerId) => Signal(() =>
        {
            _ctx.Presence.Eliminate(playerId);
            if (_ctx.Presence.AllEliminated) _ctx.Outcome = IterationOutcome.Wiped;
        }, PlayablePhases);

        /// <summary>El grupo abandona la iteración (p. ej. el anfitrión sale).</summary>
        public void Abandon() => Signal(() => _ctx.Outcome ??= IterationOutcome.Abandoned, PlayablePhases);
    }
}
