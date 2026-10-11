namespace Specimen.Core.Floors
{
    /// <summary>Función de una sala dentro del trazado de un piso.</summary>
    public enum RoomKind
    {
        /// <summary>Puertas del ascensor (anillo 0).</summary>
        Entrance,
        /// <summary>Sala de saqueo normal.</summary>
        Chamber,
        /// <summary>Hueco de Emergencia: salida en el anillo más profundo.</summary>
        EmergencyExit
    }
}
