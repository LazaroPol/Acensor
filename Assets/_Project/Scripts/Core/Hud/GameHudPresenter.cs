using Specimen.Core.Common;
using Specimen.Core.Iteration;
using Specimen.Core.Prototype;
using Specimen.Core.Vitals;

namespace Specimen.Core.Hud
{
    /// <summary>
    /// Modelo de vista del HUD de partida: temporizador de la fase, piso, barras de recursos del
    /// jugador local y avisos. No sabe de Unity: la capa Runtime solo lo dibuja.
    /// </summary>
    public sealed class GameHudPresenter
    {
        private readonly IterationRun _run;
        private readonly CharacterVitals _vitals;

        public GameHudPresenter(IterationRun run, CharacterVitals vitals, HudNotices notices)
        {
            _run = Guard.NotNull(run, nameof(run));
            _vitals = Guard.NotNull(vitals, nameof(vitals));
            Notices = Guard.NotNull(notices, nameof(notices));
        }

        public HudNotices Notices { get; }

        public string PhaseText => PhaseLabels.Of(_run.Phase);
        public string FloorText => _run.Floor > 0 ? "Piso " + _run.Floor : string.Empty;

        /// <summary>Tiempo restante de la fase («mm:ss»), o vacío si la fase no tiene límite.</summary>
        public string TimerText => _run.PhaseRemaining.HasValue ? HudTime.Format(_run.PhaseRemaining.Value) : string.Empty;

        public bool IsTimerCritical => _run.PhaseRemaining.HasValue && _run.PhaseRemaining.Value <= 15f;

        public HudBar Health => Bar("Vida", _vitals.Health);
        public HudBar ClassResource => Bar("Recurso", _vitals.ClassResource);
        public HudBar Stamina => Bar("Aguante", _vitals.Stamina);

        private static HudBar Bar(string label, ResourcePool pool) => new HudBar(label, pool.Current, pool.Max);
    }
}
