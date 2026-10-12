namespace Specimen.Core.Crafting.Stations
{
    /// <summary>Resultado de colocar una pieza en una estación.</summary>
    public enum PartPlaceResult
    {
        Placed,
        Incompatible,
        AlreadyPlaced,
        NoFreeSlot
    }
}
