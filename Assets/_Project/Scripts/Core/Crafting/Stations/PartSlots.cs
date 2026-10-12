using System.Collections.Generic;
using System.Linq;
using Specimen.Core.Common;
using Specimen.Core.Crafting.Definitions;

namespace Specimen.Core.Crafting.Stations
{
    /// <summary>Ranuras de pieza de una estación: solo admite piezas compatibles y sin repetir.</summary>
    public sealed class PartSlots
    {
        public const int DefaultCapacity = 3;

        private readonly List<StationPart> _parts = new List<StationPart>();

        public StationType Station { get; }
        public int Capacity { get; }
        public IReadOnlyList<StationPart> Parts => _parts;

        public PartSlots(StationType station, int capacity = DefaultCapacity)
        {
            Station = station;
            Capacity = Guard.Positive(capacity, nameof(capacity));
        }

        public PartPlaceResult Place(StationPart part)
        {
            Guard.NotNull(part, nameof(part));
            if (!part.Definition.FitsIn(Station)) return PartPlaceResult.Incompatible;
            if (Find(part.Id) != null) return PartPlaceResult.AlreadyPlaced;
            if (_parts.Count >= Capacity) return PartPlaceResult.NoFreeSlot;
            _parts.Add(part);
            return PartPlaceResult.Placed;
        }

        /// <summary>Retira la pieza indicada y la devuelve; null si no estaba.</summary>
        public StationPart Remove(string partId)
        {
            var part = Find(partId);
            if (part != null) _parts.Remove(part);
            return part;
        }

        public StationPart Find(string partId) => _parts.FirstOrDefault(p => p.Id == partId);

        /// <summary>True si la pieza está colocada y no está rota.</summary>
        public bool HasWorking(string partId)
        {
            var part = Find(partId);
            return part != null && !part.IsBroken;
        }

        public bool HasAll(IEnumerable<string> partIds) => partIds.All(HasWorking);

        public int QualityBonus => _parts.Where(p => !p.IsBroken).Sum(p => p.Definition.QualityBonus);

        /// <summary>Reducción de tiempo de las piezas sanas (suma; la cola la limita).</summary>
        public float TimeBonus => _parts.Where(p => !p.IsBroken).Sum(p => p.Definition.TimeBonus);

        /// <summary>Ahorro de combustible de las piezas sanas (p. ej. fuelle 0,25).</summary>
        public float FuelSaving => _parts.Where(p => !p.IsBroken).Sum(p => p.Definition.FuelSaving);

        /// <summary>Gasta 1 punto de las piezas indicadas (las que exige la receta fabricada).</summary>
        public void Wear(IEnumerable<string> partIds)
        {
            foreach (var id in partIds) Find(id)?.Wear();
        }
    }
}
