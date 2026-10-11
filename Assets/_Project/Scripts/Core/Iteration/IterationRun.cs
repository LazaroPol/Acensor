using System;
using Specimen.Core.Events;
using Specimen.Core.Iteration.States;
using Specimen.Core.StateMachines;
using Specimen.Core.Time;

namespace Specimen.Core.Iteration
{
    /// <summary>
    /// Orquesta una iteración: Armería → (Calma → Incursión → Ascenso) × pisos → Asedio → Extracción → Fin.
    /// Las fases sin temporizador avanzan con señales explícitas (salir de la armería, asedio superado…).
    /// </summary>
    public sealed class IterationRun : ITickable
    {
        private readonly IterationContext _ctx;
        private readonly StateMachine<IterationPhase> _machine;

        public IterationPhase Phase => _machine.CurrentId;
        public int Floor => _ctx.Floor;
        public bool HasStarted => _machine.IsRunning;

        /// <summary>Segundos restantes de la fase actual, o null si la fase no tiene cuenta atrás.</summary>
        public float? PhaseRemaining => _ctx.Timer?.Remaining;

        public IterationRun(IterationConfig config, IEventPublisher events)
        {
            _ctx = new IterationContext(config, events);
            _machine = IterationMachineFactory.Create(_ctx);
            _machine.StateChanged += (from, to) =>
                _ctx.Events.Publish(new IterationPhaseChanged(from, to, _ctx.Floor));
        }

        public void Start() => _machine.Start(IterationPhase.Armory);

        public void Tick(float deltaSeconds) => _machine.Tick(deltaSeconds);

        public void LeaveArmory() => Signal(IterationPhase.Armory, () => _ctx.ArmoryCompleted = true);

        public void CompleteSiege() => Signal(IterationPhase.Siege, () => _ctx.SiegeCompleted = true);

        public void CompleteExtraction() => Signal(IterationPhase.Extraction, () => _ctx.ExtractionCompleted = true);

        private void Signal(IterationPhase expected, Action apply)
        {
            if (!HasStarted || Phase != expected)
                throw new InvalidOperationException($"Solo válido en {expected}; fase actual: {Phase}.");
            apply();
        }
    }
}
