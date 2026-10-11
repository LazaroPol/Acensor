namespace Specimen.Core.Floors
{
    /// <summary>Genera el trazado de un piso de forma reproducible a partir de una semilla.</summary>
    public interface IFloorGenerator
    {
        FloorLayout Generate(ulong seed);
    }
}
