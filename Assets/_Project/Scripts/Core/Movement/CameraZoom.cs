using System;
using Specimen.Core.Common;

namespace Specimen.Core.Movement
{
    /// <summary>Distancia de la cámara al objetivo, acotada entre un mínimo y un máximo, ajustable con la rueda.</summary>
    public sealed class CameraZoom
    {
        public CameraZoom(float initial = 12f, float min = 6f, float max = 20f, float stepPerUnit = 1.5f)
        {
            Min = Guard.Positive(min, nameof(min));
            Max = max >= min ? max : throw new ArgumentOutOfRangeException(nameof(max), "Max debe ser >= min.");
            StepPerUnit = Guard.Positive(stepPerUnit, nameof(stepPerUnit));
            Distance = Math.Min(Max, Math.Max(Min, initial));
        }

        public float Min { get; }
        public float Max { get; }
        public float StepPerUnit { get; }
        public float Distance { get; private set; }

        /// <summary>Aplica un desplazamiento de rueda: positivo (muescas de rueda) acerca, negativo aleja. Devuelve la distancia resultante.</summary>
        public float Apply(float scroll)
        {
            Distance = Math.Min(Max, Math.Max(Min, Distance - scroll * StepPerUnit));
            return Distance;
        }
    }
}
