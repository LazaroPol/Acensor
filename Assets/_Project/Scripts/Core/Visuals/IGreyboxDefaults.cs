namespace Specimen.Core.Visuals
{
    /// <summary>Estrategia que da el estilo greybox de un ID lógico.</summary>
    public interface IGreyboxDefaults
    {
        GreyboxStyle For(VisualId id);
    }
}
