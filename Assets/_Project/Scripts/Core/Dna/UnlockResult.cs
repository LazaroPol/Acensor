namespace Specimen.Core.Dna
{
    /// <summary>Resultado de intentar desbloquear un nodo de ADN.</summary>
    public enum UnlockResult
    {
        Ok,
        UnknownNode,
        AlreadyUnlocked,
        NotEnoughPoints,
        TierLocked
    }
}
