namespace Specimen.Core.Floors.Types
{
    /// <summary>Condición principal para superar un piso.</summary>
    public enum FloorObjective
    {
        /// <summary>Explorar y salir por el Hueco de Emergencia.</summary>
        ReachExit,
        DefeatBoss,
        SurviveWaves,
        SolvePuzzle,
        /// <summary>Tomar una decisión que genera un Eco.</summary>
        MakeChoice,
        /// <summary>Piso seguro: descansar, comerciar o forjar.</summary>
        Rest
    }
}
