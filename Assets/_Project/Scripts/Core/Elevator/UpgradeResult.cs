namespace Specimen.Core.Elevator
{
    /// <summary>Resultado de un intento de mejorar el módulo de una ranura.</summary>
    public enum UpgradeResult
    {
        Upgraded,
        InvalidSlot,
        EmptySlot,
        MaxLevel,
        MissingMaterials
    }
}
