using Specimen.Core.Iteration.Ending;
using Specimen.Core.StateMachines;

namespace Specimen.Core.Iteration.States
{
    /// <summary>Fin: fija el resultado (victoria si no había otro), resuelve destinos y lo publica.</summary>
    internal sealed class EndedState : StateBase
    {
        private readonly IterationContext _ctx;

        public EndedState(IterationContext ctx) => _ctx = ctx;

        public override void Enter()
        {
            _ctx.Outcome ??= IterationOutcome.Victory;
            _ctx.Fates = FateResolver.Resolve(_ctx.Outcome.Value, _ctx.Presence);
            _ctx.Events.Publish(new IterationEnded(_ctx.Outcome.Value, _ctx.Floor, _ctx.Fates));
        }
    }
}
