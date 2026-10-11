namespace Specimen.Core.Repair
{
    /// <summary>Resultado de un intento de reparación.</summary>
    public enum RepairResult
    {
        Repaired,
        PartiallyRepaired,
        NotRepairable,
        NothingToRepair,
        NoRule,
        MissingMaterials
    }
}
