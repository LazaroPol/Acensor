namespace Specimen.Core.Floors.Remnants
{
    /// <summary>Resto de un clon anterior colocado en una sala del piso.</summary>
    public readonly struct Remnant
    {
        public Remnant(int roomId, RemnantKind kind)
        {
            RoomId = roomId;
            Kind = kind;
        }

        public int RoomId { get; }
        public RemnantKind Kind { get; }

        /// <summary>Los diarios están escritos con tu letra: son de clones anteriores de ti.</summary>
        public bool IsInYourHandwriting => Kind == RemnantKind.Diary;

        public override string ToString() => $"{Kind}@{RoomId}";
    }
}
