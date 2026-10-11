using System;
using System.Collections.Generic;
using Specimen.Core.Events;
using Specimen.Core.Iteration.Readiness;
using Specimen.Core.StateMachines;
using Specimen.Core.Time;

namespace Specimen.Core.Iteration
{
    /// <summary>
    /// Orquesta una iteración: Armería → (Calma → Incursión → Ascenso) × pisos → Asedio → Extracción → Fin.
    /// Las fases sin temporizador avanzan con señales explícitas (salir de la armería, asedio superado…).
    /// </summary>
    public sealed partial class IterationRun : ITickable
    {
        private readonly IterationContext _ctx;
        private readonly StateMachine<IterationPhase> _machine;

        public IterationPhase Phase => _machine.CurrentId;
        public int Floor => _ctx.Floor;
        public bool HasStarted => _machine.IsRunning;

        /// <summary>Segundos restantes de la fase actual, o null si la fase no tiene cuenta atrás.</summary>
        public float? PhaseRemaining => _ctx.Timer?.Remaining;

        public IterationRun(IterationConfig config, IEventPublisher events, IEnumerable<int> playerIds,
                            IHasteBonusPolicy hastePolicy = null)
        {
            _ctx = new IterationContext(config, events, new ReadyCheck(playerIds), hastePolicy ?? new LinearHasteBonus());
            _machine = IterationMachineFactory.Create(_ctx);
            _machine.StateChanged += (from, to) =>
                _ctx.Events.Publish(new IterationPhaseChanged(from, to, _ctx.Floor));
        }

        public IReadOnlyCollection<int> Players => _ctx.Ready.Players;
        public bool IsReady(int playerId) => _ctx.Ready.IsReady(playerId);

        /// <summary>Bonus Prisa ganado en un piso (0 si no se ganó).</summary>
        public float HasteBonus(int floor) => _ctx.HasteBonuses.TryGetValue(floor, out var b) ? b : 0f;

        public void Start() => _machine.Start(IterationPhase.Armory);

        public void Tick(float deltaSeconds) => _machine.Tick(deltaSeconds);

        public void LeaveArmory() => Signal(IterationPhase.Armory, () => _ctx.ArmoryCompleted = true);

        public void CompleteSiege() => Signal(IterationPhase.Siege, () => _ctx.SiegeCompleted = true);

        public void CompleteExtraction() => Signal(IterationPhase.Extraction, () => _ctx.ExtractionCompleted = true);

        public void MarkReady(int playerId) => Signal(IterationPhase.Calm, () => _ctx.Ready.MarkReady(playerId));

        public void UnmarkReady(int playerId) => Signal(IterationPhase.Calm, () => _ctx.Ready.Unmark(playerId));

        /// <summary>Voto de cierre anticipado de puertas (solo durante la Incursión).</summary>
        public void VoteEarlyClose(int playerId, bool yes) =>
            Signal(IterationPhase.Incursion, () => _ctx.EarlyClose.Cast(_ctx, playerId, yes));

        private void Signal(IterationPhase expected, Action apply) => Signal(apply, expected);

        private void Signal(Action apply, params IterationPhase[] allowed)
        {
            if (!HasStarted || Array.IndexOf(allowed, Phase) < 0)
                throw new InvalidOperationException($"No válido en la fase actual: {Phase}.");
            apply();
        }
    }
}
