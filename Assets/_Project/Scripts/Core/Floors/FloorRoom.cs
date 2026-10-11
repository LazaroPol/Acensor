namespace Specimen.Core.Floors
{
    /// <summary>Sala del piso. El anillo 0 es la entrada; 1-3 son anillos de profundidad.</summary>
    public readonly struct FloorRoom
    {
        public FloorRoom(int id, int ring, RoomKind kind)
        {
            Id = id;
            Ring = ring;
            Kind = kind;
        }

        public int Id { get; }
        public int Ring { get; }
        public RoomKind Kind { get; }

        public override string ToString() => $"{Kind}#{Id}@{Ring}";
    }
}
