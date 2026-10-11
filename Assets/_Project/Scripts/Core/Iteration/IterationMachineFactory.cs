using Specimen.Core.Iteration.States;
using Specimen.Core.StateMachines;

namespace Specimen.Core.Iteration
{
    /// <summary>Define los estados y transiciones del bucle de iteración en un único sitio.</summary>
    internal static class IterationMachineFactory
    {
        public static StateMachine<IterationPhase> Create(IterationContext ctx)
        {
            var timerDone = new System.Func<bool>(() => ctx.Timer != null && ctx.Timer.IsExpired);

            return new StateMachine<IterationPhase>()
                .AddState(IterationPhase.Armory, new SignalState())
                .AddState(IterationPhase.Calm, new CalmState(ctx))
                .AddState(IterationPhase.Incursion, new IncursionState(ctx))
                .AddState(IterationPhase.Ascent, new SignalState())
                .AddState(IterationPhase.Siege, new SignalState())
                .AddState(IterationPhase.Extraction, new SignalState())
                .AddState(IterationPhase.Ended, new SignalState())
                .AddTransition(IterationPhase.Armory, IterationPhase.Calm, () => ctx.ArmoryCompleted)
                .AddTransition(IterationPhase.Calm, IterationPhase.Incursion, () => timerDone() || ctx.Ready.AllReady)
                .AddTransition(IterationPhase.Incursion, IterationPhase.Ascent, timerDone)
                .AddTransition(IterationPhase.Ascent, IterationPhase.Siege, () => ctx.IsLastFloor)
                .AddTransition(IterationPhase.Ascent, IterationPhase.Calm, () => !ctx.IsLastFloor)
                .AddTransition(IterationPhase.Siege, IterationPhase.Extraction, () => ctx.SiegeCompleted)
                .AddTransition(IterationPhase.Extraction, IterationPhase.Ended, () => ctx.ExtractionCompleted);
        }
    }
}
