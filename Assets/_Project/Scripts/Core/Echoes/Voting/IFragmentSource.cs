namespace Specimen.Core.Echoes.Voting
{
    /// <summary>Consulta los Fragmentos de Memoria de un jugador (criterio de desempate).</summary>
    public interface IFragmentSource
    {
        int FragmentsOf(int voter);
    }
}
