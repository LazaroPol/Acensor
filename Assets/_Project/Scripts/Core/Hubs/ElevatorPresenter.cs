using System.Collections.Generic;
using System.Linq;
using Specimen.Core.Common;
using Specimen.Core.Elevator;
using Specimen.Core.Iteration;
using Specimen.Core.Iteration.Presence;

namespace Specimen.Core.Hubs
{
    /// <summary>Pantalla del Ascensor-Refugio: ranuras de módulos y el «listo» del jugador local durante la Calma.</summary>
    public sealed class ElevatorPresenter
    {
        private readonly ElevatorPlatform _platform;
        private readonly IterationRun _run;
        private readonly int _playerId;

        public ElevatorPresenter(ElevatorPlatform platform, IterationRun run, int playerId)
        {
            _platform = Guard.NotNull(platform, nameof(platform));
            _run = Guard.NotNull(run, nameof(run));
            _playerId = playerId;
        }

        public bool IsCalm => _run.HasStarted && _run.Phase == IterationPhase.Calm;
        /// <summary>Hay incursión en marcha y el jugador local está en el ascensor: puede salir al piso.</summary>
        public bool CanExit => _run.HasStarted && _run.Phase == IterationPhase.Incursion
                               && _run.Location(_playerId) == PlayerLocation.InElevator;

        public bool IsReady => IsCalm && _run.IsReady(_playerId);

        public IReadOnlyList<string> SlotLabels() => _platform.Slots.Select(Label).ToList();

        /// <summary>Alterna «listo para salir»; solo vale en la Calma. Devuelve el nuevo estado.</summary>
        public bool ToggleReady()
        {
            if (!IsCalm) return false;
            if (_run.IsReady(_playerId)) _run.UnmarkReady(_playerId);
            else _run.MarkReady(_playerId);
            return _run.IsReady(_playerId);
        }

        private static string Label(ModuleSlot slot) =>
            slot.IsEmpty ? $"Ranura {slot.Index + 1}: vacía" : $"Ranura {slot.Index + 1}: {slot.Module} (nv. {slot.Level})";
    }
}
