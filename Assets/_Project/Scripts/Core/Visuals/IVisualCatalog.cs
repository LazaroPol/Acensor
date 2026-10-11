namespace Specimen.Core.Visuals
{
    /// <summary>Resuelve un ID lógico a su asset asignado o a su fallback greybox.</summary>
    public interface IVisualCatalog<TAsset> where TAsset : class
    {
        VisualResolution<TAsset> Resolve(VisualId id);
    }
}
