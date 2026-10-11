namespace Specimen.Core.Visuals
{
    /// <summary>
    /// IDs lógicos de todo lo visible. Son el contrato con los assets del catálogo:
    /// no se renombran ni se renumeran (los valores se serializan en Unity).
    /// </summary>
    public enum VisualId
    {
        Jugador = 1,
        Suelo = 2,
        Muro = 3,
        Puerta = 4,
        Ascensor = 5,
        Loot = 6,
        Enemigo = 7
    }
}
