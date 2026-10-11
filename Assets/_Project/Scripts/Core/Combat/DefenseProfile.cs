using Specimen.Core.Common;

namespace Specimen.Core.Combat
{
    /// <summary>Defensas del objetivo: armadura plana y resistencias por tipo.</summary>
    public sealed class DefenseProfile
    {
        public DefenseProfile(float armor, Resistances resistances)
        {
            Armor = Guard.NonNegative(armor, nameof(armor));
            Resistances = Guard.NotNull(resistances, nameof(resistances));
        }

        public static DefenseProfile None => new DefenseProfile(0f, Resistances.None);

        public float Armor { get; }
        public Resistances Resistances { get; }
    }
}
