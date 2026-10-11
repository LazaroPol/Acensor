namespace Specimen.Core.Abilities
{
    /// <summary>Cobra el coste de una habilidad; si no alcanza, no cobra nada.</summary>
    public interface IAbilityCostPayer
    {
        AbilityResult TryPay(AbilityDefinition ability);
    }
}
