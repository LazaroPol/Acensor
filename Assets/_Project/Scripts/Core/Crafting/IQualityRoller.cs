using Specimen.Core.Items;

namespace Specimen.Core.Crafting
{
    /// <summary>Estrategia que decide la calidad de un objeto fabricado.</summary>
    public interface IQualityRoller
    {
        Quality Roll(QualityRollInput input);
    }
}
