namespace Specimen.Core.Crafting.Session
{
    /// <summary>Resultado de pedir una receta en la ventana de una estación.</summary>
    public enum StationCraftResult
    {
        Queued,
        NotAvailable,
        ProfessionLevel,
        MissingMaterials,
        QueueFull
    }
}
