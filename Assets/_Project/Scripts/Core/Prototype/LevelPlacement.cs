using Specimen.Core.Visuals;

namespace Specimen.Core.Prototype
{
    /// <summary>
    /// Un visual del nivel: ID lógico, posición del pivote en metros y giro en Y (grados).
    /// Con giro 0 un muro se extiende hacia +X; con 270 hacia +Z.
    /// </summary>
    public sealed class LevelPlacement
    {
        public VisualId Id { get; }
        public float X { get; }
        public float Y { get; }
        public float Z { get; }
        public float YawDegrees { get; }

        public LevelPlacement(VisualId id, float x, float y, float z, float yawDegrees = 0f)
        {
            Id = id;
            X = x;
            Y = y;
            Z = z;
            YawDegrees = yawDegrees;
        }

        public override string ToString() => $"{Id} @ ({X}, {Y}, {Z}) yaw {YawDegrees}";
    }
}
