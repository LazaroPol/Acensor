namespace Specimen.Editor.Arte
{
    /// <summary>Un paso del generador de arte (personajes, enemigos, entorno…).</summary>
    public interface IArtStep
    {
        string Name { get; }

        void Run(ArtContext context);
    }
}
