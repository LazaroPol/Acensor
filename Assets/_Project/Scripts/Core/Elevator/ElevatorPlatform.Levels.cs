using System.Linq;

namespace Specimen.Core.Elevator
{
    public sealed partial class ElevatorPlatform : IModuleLevels
    {
        /// <summary>Nivel del módulo de ese tipo, o 0 si no está construido.</summary>
        public int LevelOf(ModuleType type) => _slots.FirstOrDefault(s => s.Module == type)?.Level ?? 0;
    }
}
