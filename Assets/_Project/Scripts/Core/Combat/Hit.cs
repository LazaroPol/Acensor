using Specimen.Core.Common;

namespace Specimen.Core.Combat
{
    /// <summary>Golpe bruto antes de mitigación: cantidad, tipo y si puede ser crítico.</summary>
    public sealed class Hit
    {
        public Hit(float amount, DamageType type, bool canCrit = true)
        {
            Amount = Guard.NonNegative(amount, nameof(amount));
            Type = type;
            CanCrit = canCrit;
        }

        public float Amount { get; }
        public DamageType Type { get; }
        public bool CanCrit { get; }
    }
}
