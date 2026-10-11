namespace Specimen.Core.Abilities
{
    /// <summary>Resultado de intentar activar una habilidad.</summary>
    public enum AbilityResult
    {
        Activated,
        OnCooldown,
        NotEnoughResource,
        NotEnoughStamina
    }
}
