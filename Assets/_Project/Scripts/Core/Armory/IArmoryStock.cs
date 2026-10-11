namespace Specimen.Core.Armory
{
    /// <summary>Stock limitado por tipo de objeto de la Armería (GDD §4).</summary>
    public interface IArmoryStock<TItem>
    {
        int Capacity(TItem item);
        int Remaining(TItem item);
        bool TryTake(TItem item);
        bool Return(TItem item);
        /// <summary>Repone todo el stock (nueva iteración).</summary>
        void Restock();
    }
}
