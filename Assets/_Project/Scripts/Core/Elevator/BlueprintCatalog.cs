using System;
using System.Collections.Generic;
using Specimen.Core.Common;

namespace Specimen.Core.Elevator
{
    /// <summary>Catálogo de planos en memoria; como mucho un plano por tipo de módulo.</summary>
    public sealed class BlueprintCatalog : IBlueprintCatalog
    {
        private readonly Dictionary<ModuleType, ModuleBlueprint> _byType =
            new Dictionary<ModuleType, ModuleBlueprint>();

        public BlueprintCatalog(IEnumerable<ModuleBlueprint> blueprints)
        {
            foreach (var b in Guard.NotNull(blueprints, nameof(blueprints)))
            {
                if (b == null) throw new ArgumentException("Plano nulo.", nameof(blueprints));
                if (_byType.ContainsKey(b.Type))
                    throw new ArgumentException($"Plano duplicado: {b.Type}.", nameof(blueprints));
                _byType.Add(b.Type, b);
            }
        }

        public bool TryGet(ModuleType type, out ModuleBlueprint blueprint) =>
            _byType.TryGetValue(type, out blueprint);
    }
}
