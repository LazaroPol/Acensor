using Specimen.Core.Common;
using Specimen.Core.Items;
using Specimen.Core.Iteration;
using Specimen.Core.Life;

namespace Specimen.Core.Elevator
{
    /// <summary>
    /// Litera del Ascensor-Refugio (GDD §3.3, §6): revive Espectros durante la Calma cobrando materiales
    /// del alijo común. Inyéctala como <see cref="IReviveCost"/> de cada <see cref="CharacterLife"/>;
    /// el revivir en campo se delega en <c>fieldCost</c> (sin él no se puede pagar).
    /// </summary>
    public sealed class Bunk : IReviveCost
    {
        private readonly IModuleLevels _levels;
        private readonly IInventory _stock;
        private readonly BunkConfig _config;
        private readonly IReviveCost _fieldCost;

        public Bunk(IModuleLevels levels, IInventory stock, BunkConfig config, IReviveCost fieldCost = null)
        {
            _levels = Guard.NotNull(levels, nameof(levels));
            _stock = Guard.NotNull(stock, nameof(stock));
            _config = Guard.NotNull(config, nameof(config));
            _fieldCost = fieldCost;
        }

        public int Level => _levels.LevelOf(ModuleType.Bunk);
        public bool IsBuilt => Level > 0;

        /// <summary>True si la Litera está construida y el alijo cubre el coste de su nivel actual.</summary>
        public bool CanAfford => IsBuilt && ModuleBlueprint.CanPay(_config.CostAt(Level), _stock);

        /// <summary>Revive a <paramref name="life"/> en la Litera; si falla, no se consume nada.</summary>
        public ReviveResult Revive(CharacterLife life, IterationPhase phase)
        {
            Guard.NotNull(life, nameof(life));
            if (!life.IsSpectre) return ReviveResult.NotSpectre;
            if (!IsBuilt) return ReviveResult.NoBunk;
            return life.TryRevive(ReviveMethod.ElevatorBunk, phase);
        }

        public bool TryPay(ReviveMethod method)
        {
            if (method != ReviveMethod.ElevatorBunk) return _fieldCost != null && _fieldCost.TryPay(method);
            if (!CanAfford) return false;
            ModuleBlueprint.Pay(_config.CostAt(Level), _stock);
            return true;
        }
    }
}
