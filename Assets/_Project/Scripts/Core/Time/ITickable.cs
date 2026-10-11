namespace Specimen.Core.Time
{
    /// <summary>Sistema del dominio que avanza con el tiempo. El adaptador de Unity lo llama desde Update.</summary>
    public interface ITickable
    {
        void Tick(float deltaSeconds);
    }
}
