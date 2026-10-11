using Specimen.Core.Iteration.Readiness;
using Specimen.Core.Time;

namespace Specimen.Core.Iteration.States
{
    /// <summary>
    /// Calma: el ascensor llega a un piso nuevo y las puertas están cerradas.
    /// Si todo el grupo marca "Listo" antes de tiempo, termina antes y se gana el bonus Prisa.
    /// </summary>
    internal sealed class CalmState : TimedPhaseState
    {
        public CalmState(IterationContext context) : base(context) { }

        protected override PhaseTimer CreateTimer() => new PhaseTimer(Context.Config.CalmSeconds);

        public override void Enter()
        {
            Context.Floor++;
            Context.Ready.Reset();
            base.Enter();
            Context.Events.Publish(new FloorReached(Context.Floor));
        }

        public override void Exit()
        {
            float saved = Context.Timer.IsExpired ? 0f : Context.Timer.Remaining;
            if (saved > 0f) AwardHaste(saved);
            Context.Ready.Reset();
            base.Exit();
        }

        private void AwardHaste(float saved)
        {
            float bonus = Context.HastePolicy.BonusFor(saved, Context.Config.CalmSeconds);
            Context.HasteBonuses[Context.Floor] = bonus;
            Context.Events.Publish(new HasteBonusEarned(Context.Floor, saved, bonus));
        }
    }
}
