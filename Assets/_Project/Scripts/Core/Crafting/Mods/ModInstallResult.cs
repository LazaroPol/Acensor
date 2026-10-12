namespace Specimen.Core.Crafting.Mods
{
    /// <summary>Resultado de instalar un mod (GDD §17.4). Cualquier fallo deja todo sin cambios.</summary>
    public enum ModInstallResult
    {
        Installed,
        NoFreeSlot,
        SlotTypeTaken,
        Incompatible,
        RequirementsNotMet,
        ItemBroken,
        /// <summary>El mod no está en la bolsa del jugador.</summary>
        ModNotOwned
    }

    /// <summary>Resultado de retirar un mod. Cualquier fallo deja todo sin cambios.</summary>
    public enum ModUninstallResult
    {
        /// <summary>Retirado y devuelto a la bolsa.</summary>
        Returned,
        /// <summary>Retirado y destruido (no retirable).</summary>
        Destroyed,
        NotInstalled,
        BagFull
    }
}
