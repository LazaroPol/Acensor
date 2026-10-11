namespace Specimen.Core.Life
{
    /// <summary>Paga el coste de un revivir (materiales de la Litera u objeto de campo).</summary>
    public interface IReviveCost
    {
        /// <summary>Intenta cobrar el coste; si no puede, no consume nada.</summary>
        bool TryPay(ReviveMethod method);
    }
}
