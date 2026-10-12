namespace Specimen.Core.Crafting.Stations
{
    /// <summary>Resultado de intentar mejorar una estación.</summary>
    public enum UpgradeResult
    {
        Upgraded,
        WrongStation,
        WrongLevel,
        MissingMaterials
    }
}
