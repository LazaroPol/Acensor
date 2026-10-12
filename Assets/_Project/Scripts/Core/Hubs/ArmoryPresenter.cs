using System.Collections.Generic;
using System.Linq;
using Specimen.Core.Armory;
using Specimen.Core.Classes;
using Specimen.Core.Common;
using Specimen.Core.Iteration;

namespace Specimen.Core.Hubs
{
    /// <summary>
    /// Pantalla de la Armería (Piso 0): el jugador local elige un kit de clase con el stock limitado del
    /// <see cref="ClassKitArmory"/> y, con kit en mano, sale hacia el Ascensor (señal <c>LeaveArmory</c>).
    /// </summary>
    public sealed class ArmoryPresenter
    {
        private readonly ClassKitArmory _armory;
        private readonly IClassKitCatalog _catalog;
        private readonly IterationRun _run;
        private readonly int _playerId;

        public ArmoryPresenter(ClassKitArmory armory, IClassKitCatalog catalog, IterationRun run, int playerId)
        {
            _armory = Guard.NotNull(armory, nameof(armory));
            _catalog = Guard.NotNull(catalog, nameof(catalog));
            _run = Guard.NotNull(run, nameof(run));
            _playerId = playerId;
        }

        public string Message { get; private set; } = "Elige tu kit de clase.";
        public bool HasKit => _armory.TryGetKit(_playerId, out _);
        public bool CanLeave => HasKit && _run.HasStarted && _run.Phase == IterationPhase.Armory;

        public IReadOnlyList<KitEntry> Entries() => _catalog.All.Select(Entry).ToList();

        /// <summary>Elige un kit (suelta el anterior si lo cambia). Devuelve si quedó asignado.</summary>
        public bool Choose(ClassKitId id)
        {
            _armory.TryGetKit(_playerId, out var current);
            if (current != null && current.Id == id) { Message = "Ya llevas ese kit."; return true; }
            if (!_armory.CanClaim(id)) { Message = "No quedan más de ese kit."; return false; }
            _armory.Release(_playerId);
            _armory.TryClaim(_playerId, id);
            Message = "Llevas: " + _catalog.Get(id).DisplayName;
            return true;
        }

        /// <summary>Sale de la Armería hacia el Ascensor si lleva kit.</summary>
        public bool Leave()
        {
            if (!HasKit) { Message = "Coge un kit antes de salir."; return false; }
            if (!CanLeave) return false;
            _run.LeaveArmory();
            Message = "Subiendo al Ascensor…";
            return true;
        }

        private KitEntry Entry(ClassKit kit)
        {
            _armory.TryGetKit(_playerId, out var mine);
            var availability = mine != null && mine.Id == kit.Id ? KitAvailability.Mine
                : _armory.CanClaim(kit.Id) ? KitAvailability.Free : KitAvailability.Unavailable;
            return new KitEntry(kit.Id, kit.DisplayName, kit.Role + " · " + kit.Resource, availability);
        }
    }
}
