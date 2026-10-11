namespace Specimen.Core.Combat
{
    /// <summary>Resultado de resolver un golpe: daño bruto, final tras mitigación y si fue crítico.</summary>
    public sealed class DamageResult
    {
        public DamageResult(float raw, float final, bool isCritical, DamageType type)
        {
            Raw = raw;
            Final = final;
            IsCritical = isCritical;
            Type = type;
        }

        public float Raw { get; }
        public float Final { get; }
        public bool IsCritical { get; }
        public DamageType Type { get; }
        public float Mitigated => Raw - Final;
    }
}
