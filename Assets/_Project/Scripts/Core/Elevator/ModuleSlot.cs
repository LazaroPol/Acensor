namespace Specimen.Core.Elevator
{
    /// <summary>Ranura de la plataforma: vacía o con un módulo construido.</summary>
    public sealed class ModuleSlot
    {
        public int Index { get; }
        public ModuleType? Module { get; private set; }
        public bool IsEmpty => Module == null;

        internal ModuleSlot(int index) => Index = index;

        internal void Place(ModuleType type) => Module = type;

        internal void Clear() => Module = null;
    }
}
