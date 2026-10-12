namespace Specimen.Core.Crafting.Queue
{
    /// <summary>Resultado de intentar encolar una receta en una estación.</summary>
    public enum EnqueueResult
    {
        Queued,
        WrongStation,
        QueueFull,
        MissingMaterials
    }
}
