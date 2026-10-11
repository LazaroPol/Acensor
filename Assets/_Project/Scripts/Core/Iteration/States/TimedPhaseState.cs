using Specimen.Core.StateMachines;
using Specimen.Core.Time;

namespace Specimen.Core.Iteration.States
{
    /// <summary>Estado con cuenta atrás propia: crea el temporizador al entrar y lo hace avanzar.</summary>
    internal abstract class TimedPhaseState : StateBase
    {
        protected IterationContext Context { get; }

        protected TimedPhaseState(IterationContext context) => Context = context;

        protected abstract PhaseTimer CreateTimer();

        public override void Enter()
        {
            Context.Timer = CreateTimer();
            Context.Timer.Start();
        }

        public override void Exit() => Context.Timer = null;

        public override void Tick(float deltaSeconds) => Context.Timer.Tick(deltaSeconds);
    }
}
