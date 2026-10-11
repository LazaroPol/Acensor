namespace Specimen.Core.Stats
{
    /// <summary>
    /// Cómo se aplica un modificador: <c>Flat</c> suma al valor base, <c>PercentAdd</c> se suma con
    /// los demás porcentajes (+10% y +20% = +30%) y <c>Multiplier</c> multiplica el resultado.
    /// </summary>
    public enum ModifierKind
    {
        Flat,
        PercentAdd,
        Multiplier
    }
}
