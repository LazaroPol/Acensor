namespace Specimen.Core.Floors.Remnants
{
    /// <summary>Fragmento de memoria concreto escondido en una sala del piso.</summary>
    public readonly struct FragmentPlacement
    {
        public FragmentPlacement(int roomId, int fragmentId)
        {
            RoomId = roomId;
            FragmentId = fragmentId;
        }

        public int RoomId { get; }
        public int FragmentId { get; }

        public override string ToString() => $"Fragmento{FragmentId}@{RoomId}";
    }
}
