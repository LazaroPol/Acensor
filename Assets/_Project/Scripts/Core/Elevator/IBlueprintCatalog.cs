namespace Specimen.Core.Elevator
{
    /// <summary>Catálogo de planos de módulo consultable por tipo.</summary>
    public interface IBlueprintCatalog
    {
        bool TryGet(ModuleType type, out ModuleBlueprint blueprint);
    }
}
