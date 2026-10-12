using System.Linq;
using Specimen.Core.Common;
using Specimen.Core.Floors.Omens;

namespace Specimen.Core.Echoes.Consequences
{
    /// <summary>Añade al Presagio de un piso los modificadores impuestos por Ecos anteriores.</summary>
    public sealed class EchoOmenAdjuster
    {
        private readonly EchoConsequenceTable _table;
        private readonly FloorModifierCatalog _catalog;

        public EchoOmenAdjuster(EchoConsequenceTable table, FloorModifierCatalog catalog)
        {
            _table = Guard.NotNull(table, nameof(table));
            _catalog = Guard.NotNull(catalog, nameof(catalog));
        }

        /// <summary>Presagio original más los modificadores forzados (sin duplicar ni forzar combate en pisos sin él).</summary>
        public Omen Apply(Omen omen, EchoRegistry registry, int floor)
        {
            Guard.NotNull(omen, nameof(omen));
            var extra = _table.ModifiersFor(registry, floor)
                .Where(k => !omen.Has(k))
                .Select(_catalog.Get)
                .Where(m => omen.FloorType.HasCombat || !m.RequiresCombat);
            return new Omen(omen.FloorType, omen.Modifiers.Concat(extra).ToList());
        }
    }
}
