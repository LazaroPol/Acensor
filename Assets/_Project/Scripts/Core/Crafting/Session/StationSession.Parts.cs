using System.Collections.Generic;
using System.Linq;
using Specimen.Core.Crafting.Stations;
using Specimen.Core.Items;

namespace Specimen.Core.Crafting.Session
{
    /// <summary>Piezas de la estación: se colocan desde la bolsa y vuelven a ella conservando su desgaste.</summary>
    public sealed partial class StationSession
    {
        private readonly StationServices _services;
        private readonly Dictionary<string, StationPart> _stored = new Dictionary<string, StationPart>();

        /// <summary>Piezas de esta estación que el jugador lleva en la bolsa.</summary>
        public IReadOnlyList<StationPartEntry> PartsInBag() => _services.Parts
            .Where(p => p.Definition.FitsIn(Station.Type) && Profile.Bag.CountOf(p.Id) > 0)
            .ToList().AsReadOnly();

        public StationPartEntry PartEntry(string partId) => _services.PartOf(partId);

        /// <summary>Coloca una pieza de la bolsa; null si no la lleva o no es una pieza conocida.</summary>
        public PartPlaceResult? PlacePart(string partId)
        {
            var entry = _services.PartOf(partId);
            if (entry == null || Profile.Bag.CountOf(partId) == 0) return null;
            if (!_stored.TryGetValue(partId, out var part)) part = new StationPart(entry.Definition);
            var result = Station.PlacePart(part);
            if (result != PartPlaceResult.Placed) return result;
            Profile.Bag.Remove(partId, 1);
            _stored.Remove(partId);
            return result;
        }

        /// <summary>Retira la pieza a la bolsa; false si no estaba puesta o la bolsa está llena.</summary>
        public bool RemovePart(string partId)
        {
            var entry = _services.PartOf(partId);
            if (entry == null || Station.Parts.Find(partId) == null) return false;
            if (Profile.Bag.Add(ItemInstance.CreateNew(entry.Item)) == 0) return false;
            _stored[partId] = Station.RemovePart(partId);
            return true;
        }
    }
}
