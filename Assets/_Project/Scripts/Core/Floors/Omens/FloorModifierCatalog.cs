using System;
using System.Collections.Generic;
using System.Linq;
using Specimen.Core.Floors.Types;

namespace Specimen.Core.Floors.Omens
{
    /// <summary>Registro de modificadores de piso indexado por tipo.</summary>
    public sealed class FloorModifierCatalog
    {
        private readonly List<FloorModifier> _all = new List<FloorModifier>();

        public FloorModifierCatalog(IEnumerable<FloorModifier> modifiers)
        {
            if (modifiers == null) throw new ArgumentNullException(nameof(modifiers));
            foreach (var m in modifiers)
            {
                if (m == null) throw new ArgumentException("Modificador nulo.", nameof(modifiers));
                if (Contains(m.Kind))
                    throw new ArgumentException($"Modificador duplicado: {m.Kind}.", nameof(modifiers));
                _all.Add(m);
            }
        }

        public IReadOnlyList<FloorModifier> All => _all;

        public bool Contains(FloorModifierKind kind) => _all.Any(m => m.Kind == kind);

        public FloorModifier Get(FloorModifierKind kind) =>
            _all.FirstOrDefault(m => m.Kind == kind)
            ?? throw new KeyNotFoundException($"Modificador no registrado: {kind}.");

        /// <summary>Modificadores aplicables al tipo de piso, en orden de registro.</summary>
        public IReadOnlyList<FloorModifier> CompatibleWith(IFloorType type)
        {
            Common.Guard.NotNull(type, nameof(type));
            return _all.Where(m => type.HasCombat || !m.RequiresCombat).ToList();
        }

        /// <summary>Catálogo con los modificadores del GDD.</summary>
        public static FloorModifierCatalog Default => new FloorModifierCatalog(new[]
        {
            new FloorModifier(FloorModifierKind.Darkness, "Oscuridad", "las antorchas no prenden aquí"),
            new FloorModifier(FloorModifierKind.LowGravity, "Gravedad baja", "el polvo flota sin caer"),
            new FloorModifier(FloorModifierKind.IgneousEnemies, "Enemigos ígneos", "algo arde tras los muros", true),
            new FloorModifier(FloorModifierKind.Fog, "Niebla", "el aliento se vuelve bruma"),
            new FloorModifier(FloorModifierKind.Flooded, "Inundado", "se oye agua bajo el suelo"),
            new FloorModifier(FloorModifierKind.Frost, "Escarcha", "la piedra cruje de frío")
        });
    }
}
