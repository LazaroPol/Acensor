namespace Specimen.Core.Visuals
{
    /// <summary>Desplazamiento local en metros, sin depender de UnityEngine.</summary>
    public readonly struct VisualOffset
    {
        public float X { get; }
        public float Y { get; }
        public float Z { get; }

        public VisualOffset(float x, float y, float z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        public override string ToString() => $"({X}, {Y}, {Z})";
    }
}
