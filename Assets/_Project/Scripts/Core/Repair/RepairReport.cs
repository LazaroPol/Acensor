namespace Specimen.Core.Repair
{
    /// <summary>Qué pasó al reparar: resultado, material gastado y durabilidad recuperada.</summary>
    public readonly struct RepairReport
    {
        public RepairResult Result { get; }
        public int UnitsUsed { get; }
        public int DurabilityRestored { get; }

        public bool Success => Result == RepairResult.Repaired || Result == RepairResult.PartiallyRepaired;

        public RepairReport(RepairResult result, int unitsUsed = 0, int durabilityRestored = 0)
        {
            Result = result;
            UnitsUsed = unitsUsed;
            DurabilityRestored = durabilityRestored;
        }
    }
}
