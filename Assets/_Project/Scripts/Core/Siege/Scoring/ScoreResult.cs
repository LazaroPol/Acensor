namespace Specimen.Core.Siege.Scoring
{
    /// <summary>Resultado final de la puntuación: puntos base, multiplicador aplicado y puntos totales.</summary>
    public readonly struct ScoreResult
    {
        public ScoreResult(float basePoints, float multiplier)
        {
            BasePoints = basePoints;
            Multiplier = multiplier;
        }

        public float BasePoints { get; }
        public float Multiplier { get; }
        public float Total => BasePoints * Multiplier;
    }
}
