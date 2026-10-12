namespace Specimen.Core.Crafting.Queue
{
    /// <summary>Estado de la cola tras el último avance: pausada no es cancelada (no se pierde nada).</summary>
    public enum CraftQueueState
    {
        Idle,
        Working,
        NoFuel,
        OutputFull
    }
}
