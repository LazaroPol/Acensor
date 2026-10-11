namespace Specimen.Core.Elevator
{
    /// <summary>Resultado de un intento de construir un módulo en una ranura.</summary>
    public enum BuildResult
    {
        Built,
        InvalidSlot,
        SlotOccupied,
        AlreadyBuilt,
        UnknownBlueprint,
        MissingMaterials
    }
}
