namespace Specimen.Core.Elevator
{
    /// <summary>Consulta del nivel de cada módulo del Ascensor-Refugio (0 si no está construido).</summary>
    public interface IModuleLevels
    {
        int LevelOf(ModuleType type);
    }
}
