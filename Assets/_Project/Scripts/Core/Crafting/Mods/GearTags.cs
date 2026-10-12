using System;

namespace Specimen.Core.Crafting.Mods
{
    /// <summary>
    /// Etiquetas del objeto base con las que un mod declara compatibilidad (combinables).
    /// Valores explícitos y estables: no reordenar; las nuevas van al final.
    /// </summary>
    [Flags]
    public enum GearTags
    {
        None = 0,
        Arma = 1 << 0,
        ArmaContundente = 1 << 1,
        Herramienta = 1 << 2,
        Armadura = 1 << 3,
        Lanza = 1 << 4,
        Flecha = 1 << 5,
        Madera = 1 << 6,
        Metal = 1 << 7
    }
}
