namespace Specimen.Core.Crafting.Mods
{
    /// <summary>Stats de equipo que los mods modifican (GDD §17.4). Valores estables: se serializan.</summary>
    public enum GearStat
    {
        DañoCortante = 0,
        DañoContundente = 1,
        DañoPerforante = 2,
        Durabilidad = 3,
        Velocidad = 4,
        Peso = 5,
        Armadura = 6
    }
}
