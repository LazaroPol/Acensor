namespace Specimen.Core.Life
{
    /// <summary>Resultado de un intento de revivir.</summary>
    public enum ReviveResult
    {
        Revived,
        NotSpectre,
        WrongPhase,
        CannotPay,

        /// <summary>No hay Litera construida en el ascensor.</summary>
        NoBunk
    }
}
