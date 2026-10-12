using System;

namespace Specimen.Core.Items
{
    /// <summary>
    /// Etiquetas de material (combinables). Los valores son explícitos y estables porque se serializan:
    /// no reordenar ni reutilizar bits; las nuevas etiquetas van al final.
    /// </summary>
    [Flags]
    public enum MaterialTags
    {
        None = 0,
        Madera = 1 << 0,
        Metal = 1 << 1,
        Piedra = 1 << 2,
        Cuero = 1 << 3,
        Tela = 1 << 4,
        Hierba = 1 << 5,
        Comida = 1 << 6,
        Hueso = 1 << 7,
        Mineral = 1 << 8,
        Combustible = 1 << 9,
        Vidrio = 1 << 10
    }
}
