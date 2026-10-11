using Specimen.Core.Iteration.Presence;
using Specimen.Core.Time;

namespace Specimen.Core.Iteration.States
{
    /// <summary>Incursión: puertas abiertas; avisa antes de que se cierren.</summary>
    internal sealed class IncursionState : TimedPhaseState
    {
        public IncursionState(IterationContext context) : base(context) { }

        protected override PhaseTimer CreateTimer()
        {
            var timer = new PhaseTimer(Context.Config.IncursionSeconds, Context.Config.DoorWarnings);
            timer.WarningReached += s => Context.Events.Publish(new DoorsClosingWarning(Context.Floor, s));
            return timer;
        }

        public override void Enter()
        {
            Context.EarlyClose.Reset();
            base.Enter();
        }

        public override void Exit()
        {
            var trapped = Context.Presence.TrapThoseOutside();
            if (trapped.Count > 0) Context.Events.Publish(new PlayersTrapped(Context.Floor, trapped));
            base.Exit();
        }
    }
}
