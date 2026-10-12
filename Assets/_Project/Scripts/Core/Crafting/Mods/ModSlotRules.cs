namespace Specimen.Core.Crafting.Mods
{
    /// <summary>Límite de mods por tipo de hueco: 1 por defecto; Accesorio admite 2 (GDD §17.4).</summary>
    public static class ModSlotRules
    {
        public const int DefaultPerType = 1;
        public const int AccessoryPerType = 2;

        public static int MaxPerType(ModSlotType type) =>
            type == ModSlotType.Accesorio ? AccessoryPerType : DefaultPerType;
    }
}
