namespace Specimen.Core.Animation
{
    /// <summary>Nombres de los parámetros del AnimatorController generado (contrato Editor ↔ Runtime).</summary>
    public static class AnimParams
    {
        public const string Velocidad = "Velocidad";
        public const string Muerto = "Muerto";

        /// <summary>Trigger que dispara una ranura de acción; null para las ranuras de locomoción y muerte.</summary>
        public static string TriggerOf(AnimSlot slot)
        {
            switch (slot)
            {
                case AnimSlot.Esquiva: return "Esquivar";
                case AnimSlot.AtaqueMele: return "AtaqueMele";
                case AnimSlot.Hechizo: return "Hechizo";
                case AnimSlot.Golpe: return "Golpe";
                case AnimSlot.Interactuar: return "Interactuar";
                default: return null;
            }
        }

        public static bool IsLocomotion(AnimSlot slot)
            => slot == AnimSlot.Reposo || slot == AnimSlot.Andar || slot == AnimSlot.Trotar || slot == AnimSlot.Esprintar;
    }
}
