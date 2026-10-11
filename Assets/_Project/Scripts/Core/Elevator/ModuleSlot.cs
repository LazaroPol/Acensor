namespace Specimen.Core.Elevator
{
    /// <summary>Ranura de la plataforma: vacía (nivel 0) o con un módulo construido y su nivel.</summary>
    public sealed class ModuleSlot
    {
        public int Index { get; }
        public ModuleType? Module { get; private set; }
        public int Level { get; private set; }
        public bool IsEmpty => Module == null;

        internal ModuleSlot(int index) => Index = index;

        internal void Place(ModuleType type)
        {
            Module = type;
            Level = 1;
        }

        internal void LevelUp() => Level++;

        internal void Clear()
        {
            Module = null;
            Level = 0;
        }
    }
}
