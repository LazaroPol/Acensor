using Specimen.Core.Common;

namespace Specimen.Core.Combat
{
    /// <summary>Armadura con rendimientos decrecientes: reducción = armor / (armor + K). Solo daño físico.</summary>
    public sealed class ArmorMitigation
    {
        public const float DefaultK = 100f;

        public ArmorMitigation(float k = DefaultK) => K = Guard.Positive(k, nameof(k));

        public float K { get; }

        public float ReductionFor(float armor)
        {
            var a = Guard.NonNegative(armor, nameof(armor));
            return a / (a + K);
        }

        public float MultiplierFor(DamageType type, float armor)
            => type == DamageType.Physical ? 1f - ReductionFor(armor) : 1f;
    }
}
