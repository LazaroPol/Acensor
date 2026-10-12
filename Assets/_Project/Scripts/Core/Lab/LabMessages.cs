using Specimen.Core.Dna;

namespace Specimen.Core.Lab
{
    /// <summary>Textos en español del Laboratorio.</summary>
    public static class LabMessages
    {
        public static string Of(UnlockResult result)
        {
            switch (result)
            {
                case UnlockResult.Ok: return "Mutación desbloqueada.";
                case UnlockResult.AlreadyUnlocked: return "Ya tienes esta mutación.";
                case UnlockResult.NotEnoughPoints: return "No tienes puntos de ADN suficientes.";
                case UnlockResult.TierLocked: return "Invierte más puntos en la rama para abrir este nivel.";
                default: return "Mutación desconocida.";
            }
        }
    }
}
