namespace Specimen.Core.World
{
    /// <summary>Pieza modular de una casa en coordenadas locales (centro de la casa en el origen, fachada hacia +Z).</summary>
    public readonly struct HousePiece
    {
        public HousePiece(HousePieceKind kind, string model, float x, float z, float yaw)
        {
            Kind = kind;
            Model = model;
            X = x;
            Z = z;
            Yaw = yaw;
        }

        public HousePieceKind Kind { get; }
        public string Model { get; }
        public float X { get; }
        public float Z { get; }

        /// <summary>0/180: el muro corre a lo largo de X; 90/270: a lo largo de Z.</summary>
        public float Yaw { get; }

        public override string ToString() => $"{Kind} {Model} ({X}, {Z}) {Yaw}°";
    }
}
