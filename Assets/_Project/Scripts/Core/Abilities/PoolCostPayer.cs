using Specimen.Core.Common;
using Specimen.Core.Vitals;

namespace Specimen.Core.Abilities
{
    /// <summary>Cobra de los pools de recurso de clase y stamina de forma atómica (o ambos o ninguno).</summary>
    public sealed class PoolCostPayer : IAbilityCostPayer
    {
        private readonly ResourcePool _resource;
        private readonly ResourcePool _stamina;

        public PoolCostPayer(ResourcePool resource, ResourcePool stamina)
        {
            _resource = Guard.NotNull(resource, nameof(resource));
            _stamina = Guard.NotNull(stamina, nameof(stamina));
        }

        public static PoolCostPayer From(CharacterVitals vitals)
        {
            Guard.NotNull(vitals, nameof(vitals));
            return new PoolCostPayer(vitals.ClassResource, vitals.Stamina);
        }

        public AbilityResult TryPay(AbilityDefinition ability)
        {
            Guard.NotNull(ability, nameof(ability));
            if (_resource.Current < ability.ResourceCost) return AbilityResult.NotEnoughResource;
            if (_stamina.Current < ability.StaminaCost) return AbilityResult.NotEnoughStamina;
            _resource.TrySpend(ability.ResourceCost);
            _stamina.TrySpend(ability.StaminaCost);
            return AbilityResult.Activated;
        }
    }
}
