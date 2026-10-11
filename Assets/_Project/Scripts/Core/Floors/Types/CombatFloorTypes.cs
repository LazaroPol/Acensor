namespace Specimen.Core.Floors.Types
{
    /// <summary>Piso base: exploración y riesgo/recompensa por anillos.</summary>
    public sealed class LootFloor : FloorType
    {
        public LootFloor() : base(FloorTypeKind.Loot, "Saqueo", "Exploración, riesgo/recompensa por anillos",
            FloorObjective.ReachExit, true, RingLayoutConfig.Default) { }
    }

    /// <summary>Antesala corta que desemboca en la arena del jefe.</summary>
    public sealed class BossFloor : FloorType
    {
        public BossFloor() : base(FloorTypeKind.Boss, "Jefe", "Roles, coordinación",
            FloorObjective.DefeatBoss, true, Rings(0f, 0f, 2, 1)) { }
    }

    /// <summary>Arena amplia y muy conectada para defender posiciones.</summary>
    public sealed class WavesFloor : FloorType
    {
        public WavesFloor() : base(FloorTypeKind.Waves, "Oleadas", "Control, posicionamiento, torretas",
            FloorObjective.SurviveWaves, true, Rings(1f, 0.5f, 4, 4)) { }
    }

    /// <summary>Muchos anillos estrechos sin atajos laterales.</summary>
    public sealed class TrapMazeFloor : FloorType
    {
        public TrapMazeFloor() : base(FloorTypeKind.TrapMaze, "Laberinto de trampas", "Movilidad, stamina",
            FloorObjective.ReachExit, false, Rings(0f, 0.1f, 3, 4, 5)) { }
    }

    /// <summary>Todo a la vez: el trazado más grande y enrevesado.</summary>
    public sealed class HybridFloor : FloorType
    {
        public HybridFloor() : base(FloorTypeKind.Hybrid, "Híbrido", "Todo a la vez",
            FloorObjective.ReachExit, true, Rings(0.6f, 0.4f, 4, 5, 6)) { }
    }
}
