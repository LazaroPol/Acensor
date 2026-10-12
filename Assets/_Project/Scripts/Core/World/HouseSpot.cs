using System;

namespace Specimen.Core.World
{
    /// <summary>
    /// Casa del poblado: centro, giro (la puerta mira a +Z local), planta en metros (múltiplos de 2: el módulo de
    /// muro del pack mide 2 m) y estilo de muros.
    /// </summary>
    public sealed class HouseSpot
    {
        public const float Module = 2f;

        public HouseSpot(string id, float x, float z, float yaw, int width, int depth, HouseStyle style)
        {
            if (width < 4 || depth < 4 || width % 2 != 0 || depth % 2 != 0)
                throw new ArgumentException("La planta va en múltiplos de 2 m y mínimo 4 × 4.");
            Id = id;
            X = x;
            Z = z;
            Yaw = yaw;
            Width = width;
            Depth = depth;
            Style = style;
        }

        public string Id { get; }
        public float X { get; }
        public float Z { get; }
        public float Yaw { get; }
        public int Width { get; }
        public int Depth { get; }
        public HouseStyle Style { get; }

        /// <summary>Nombre de la plantilla de casa generada (p. ej. «Casa_4x6_Yeso»).</summary>
        public string Template => $"Casa_{Width}x{Depth}_{Style}";

        /// <summary>Radio que ocupa en planta (para evitar solapes).</summary>
        public float Radius => (float)Math.Sqrt(Width * Width + Depth * Depth) / 2f;
    }
}
