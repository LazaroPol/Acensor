namespace Specimen.Core.Floors.Types
{
    /// <summary>Salas conectadas entre sí para repartir al equipo y comunicarse.</summary>
    public sealed class PuzzleFloor : FloorType
    {
        public PuzzleFloor() : base(FloorTypeKind.Puzzle, "Puzzle", "Comunicación, profesiones",
            FloorObjective.SolvePuzzle, false, Rings(1f, 0f, 2, 3)) { }
    }

    /// <summary>Bifurcación: cada sala del anillo final es una opción del Eco.</summary>
    public sealed class DecisionFloor : FloorType
    {
        public DecisionFloor() : base(FloorTypeKind.Decision, "Decisión", "Ecos, moral",
            FloorObjective.MakeChoice, false, Rings(0f, 0f, 1, 2)) { }
    }

    /// <summary>Piso seguro: mercader, forja especial y recuperar fatiga.</summary>
    public sealed class SanctuaryFloor : FloorType
    {
        public SanctuaryFloor() : base(FloorTypeKind.Sanctuary, "Santuario",
            "Mercader, forja especial, recuperar fatiga", FloorObjective.Rest, false, Rings(1f, 0f, 3)) { }
    }

    /// <summary>Rutas que premian la variedad de kit de profesiones.</summary>
    public sealed class ProfessionFloor : FloorType
    {
        public ProfessionFloor() : base(FloorTypeKind.Profession, "Profesión", "Variedad de kit",
            FloorObjective.ReachExit, true, Rings(0.3f, 0.5f, 3, 3, 3)) { }
    }
}
