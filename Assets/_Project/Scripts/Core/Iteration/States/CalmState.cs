using Specimen.Core.Time;

namespace Specimen.Core.Iteration.States
{
    /// <summary>Calma: el ascensor llega a un piso nuevo y las puertas están cerradas.</summary>
    internal sealed class CalmState : TimedPhaseState
    {
        public CalmState(IterationContext context) : base(context) { }

        protected override PhaseTimer CreateTimer() => new PhaseTimer(Context.Config.CalmSeconds);

        public override void Enter()
        {
            Context.Floor++;
            base.Enter();
            Context.Events.Publish(new FloorReached(Context.Floor));
        }
    }
}
