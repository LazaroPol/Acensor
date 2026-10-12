using Specimen.Core.Common;
using Specimen.Core.Items;
using Specimen.Core.Vitals;

namespace Specimen.Core.Items.Catalog
{
    /// <summary>Consumir un objeto de la bolsa (clic derecho): aplica su efecto a las vitales y gasta una unidad.</summary>
    public static class ConsumableUse
    {
        public static bool TryUse(Inventory inventory, string itemId, CharacterVitals vitals, out ConsumableEffect effect)
        {
            Guard.NotNull(inventory, nameof(inventory));
            Guard.NotNull(vitals, nameof(vitals));
            if (!ConsumableEffect.TryGet(itemId, out effect) || inventory.CountOf(itemId) <= 0) return false;
            inventory.Remove(itemId);
            if (effect.Health > 0f) vitals.Health.Restore(effect.Health);
            if (effect.Resource > 0f) vitals.ClassResource.Restore(effect.Resource);
            return true;
        }
    }
}
