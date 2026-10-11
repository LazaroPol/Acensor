namespace Specimen.Core.Floors.Types
{
    /// <summary>Estrategia de un tipo de piso: qué pone a prueba y cómo se genera su trazado.</summary>
    public interface IFloorType
    {
        FloorTypeKind Kind { get; }
        string DisplayName { get; }
        /// <summary>Qué pone a prueba el piso (GDD §8).</summary>
        string Challenge { get; }
        FloorObjective Objective { get; }
        bool HasCombat { get; }
        RingLayoutConfig Layout { get; }

        IFloorGenerator CreateGenerator();
    }
}
