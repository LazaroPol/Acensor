using Specimen.Core.Equipment;
using Specimen.Core.Items;

namespace Specimen.Runtime.Sandbox.Ui
{
    /// <summary>Equipar y quitar piezas moviéndolas entre la bolsa y el personaje; recalcula los stats al momento.</summary>
    public static class Gear
    {
        public static void EquipFromBag(SandboxContext context, SandboxPlayer player, ItemInstance instance)
        {
            var gear = player.Gear;
            if (!gear.CanEquip(instance.Definition) || !context.Inventory.RemoveInstance(instance)) return;
            var previous = gear.Equip(instance.Definition);
            if (previous != null) context.Inventory.Add(ItemInstance.CreateNew(previous));
            player.RefreshProgression(false);
            context.Feed.Add($"Te equipas {instance.Definition.Name}" + (previous != null ? $" (guardas {previous.Name})." : "."));
        }

        public static void Unequip(SandboxContext context, SandboxPlayer player, EquipSlot slot)
        {
            var item = player.Gear.In(slot);
            if (item == null) return;
            if (context.Inventory.SpaceFor(ItemInstance.CreateNew(item)) <= 0)
            {
                context.Events.Fail("Inventario lleno");
                return;
            }
            player.Gear.Unequip(slot);
            context.Inventory.Add(ItemInstance.CreateNew(item));
            player.RefreshProgression(false);
            context.Feed.Add($"Te quitas {item.Name}.");
        }
    }
}
