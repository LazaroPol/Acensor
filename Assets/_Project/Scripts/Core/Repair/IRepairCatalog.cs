using Specimen.Core.Items;

namespace Specimen.Core.Repair
{
    /// <summary>Consulta de reglas de reparación por categoría de objeto.</summary>
    public interface IRepairCatalog
    {
        bool TryGet(ItemCategory category, out RepairRule rule);
    }
}
