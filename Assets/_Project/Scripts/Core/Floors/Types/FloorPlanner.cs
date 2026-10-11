namespace Specimen.Core.Floors.Types
{
    /// <summary>Genera el plan de un piso delegando el trazado en la estrategia de su tipo.</summary>
    public sealed class FloorPlanner
    {
        private readonly FloorTypeCatalog _catalog;

        public FloorPlanner(FloorTypeCatalog catalog)
        {
            _catalog = Common.Guard.NotNull(catalog, nameof(catalog));
        }

        public FloorPlan Plan(FloorTypeKind kind, ulong seed)
        {
            var type = _catalog.Get(kind);
            return new FloorPlan(type, type.CreateGenerator().Generate(seed));
        }
    }
}
