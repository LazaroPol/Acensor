using System;
using System.Collections.Generic;
using System.Linq;

namespace Specimen.Core.Floors.Types
{
    /// <summary>Registro de estrategias de piso indexado por tipo.</summary>
    public sealed class FloorTypeCatalog
    {
        private readonly Dictionary<FloorTypeKind, IFloorType> _types = new Dictionary<FloorTypeKind, IFloorType>();

        public FloorTypeCatalog(IEnumerable<IFloorType> types)
        {
            if (types == null) throw new ArgumentNullException(nameof(types));
            foreach (var t in types)
            {
                if (t == null) throw new ArgumentException("Tipo nulo.", nameof(types));
                if (_types.ContainsKey(t.Kind))
                    throw new ArgumentException($"Tipo de piso duplicado: {t.Kind}.", nameof(types));
                _types.Add(t.Kind, t);
            }
        }

        public IReadOnlyCollection<IFloorType> All => _types.Values;

        public bool Contains(FloorTypeKind kind) => _types.ContainsKey(kind);

        public IFloorType Get(FloorTypeKind kind) =>
            _types.TryGetValue(kind, out var t)
                ? t
                : throw new KeyNotFoundException($"Tipo de piso no registrado: {kind}.");

        /// <summary>Catálogo con todos los tipos del GDD.</summary>
        public static FloorTypeCatalog Default => new FloorTypeCatalog(new IFloorType[]
        {
            new LootFloor(), new BossFloor(), new WavesFloor(), new TrapMazeFloor(), new PuzzleFloor(),
            new DecisionFloor(), new SanctuaryFloor(), new ProfessionFloor(), new HybridFloor()
        });

        public IEnumerable<IFloorType> WithCombat(bool combat) => _types.Values.Where(t => t.HasCombat == combat);
    }
}
