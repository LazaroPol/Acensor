using Specimen.Core.Iteration;

namespace Specimen.Core.Hud
{
    /// <summary>Nombres en español de las fases de la iteración para el HUD.</summary>
    public static class PhaseLabels
    {
        public static string Of(IterationPhase phase)
        {
            switch (phase)
            {
                case IterationPhase.Armory: return "Armería";
                case IterationPhase.Calm: return "Calma";
                case IterationPhase.Incursion: return "Incursión";
                case IterationPhase.Ascent: return "Ascenso";
                case IterationPhase.Siege: return "Asedio";
                case IterationPhase.Extraction: return "Extracción";
                default: return "Fin";
            }
        }
    }
}
