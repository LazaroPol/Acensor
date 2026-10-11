using System;
using System.Collections.Generic;

namespace Specimen.Core.Items
{
    /// <summary>Inventario de ranuras con capacidad fija y apilado por objeto.</summary>
    public interface IInventory
    {
        int Capacity { get; }
        int UsedSlots { get; }
        int FreeSlots { get; }
        IReadOnlyList<ItemStack> Stacks { get; }

        /// <summary>Se lanza tras cualquier cambio de contenido.</summary>
        event Action Changed;

        /// <summary>Unidades totales de un objeto (por id).</summary>
        int CountOf(string itemId);

        /// <summary>Unidades del objeto que cabrían ahora mismo.</summary>
        int SpaceFor(ItemInstance item);

        /// <summary>Añade lo que quepa; devuelve las unidades añadidas (el resto se queda fuera).</summary>
        int Add(ItemInstance item, int quantity = 1);

        /// <summary>Quita hasta <paramref name="quantity"/> unidades; devuelve las quitadas.</summary>
        int Remove(string itemId, int quantity = 1);

        /// <summary>Quita una instancia concreta (p. ej. un arma desgastada).</summary>
        bool RemoveInstance(ItemInstance item);
    }
}
