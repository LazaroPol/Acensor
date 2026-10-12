using Specimen.Core.Combat;
using UnityEngine;

namespace Specimen.Runtime.Sandbox
{
    /// <summary>Color de cada tipo de daño (proyectiles, textos).</summary>
    public static class DamageColors
    {
        public static Color Of(DamageType type)
        {
            switch (type)
            {
                case DamageType.Fire: return new Color(1f, 0.45f, 0.1f);
                case DamageType.Toxic: return new Color(0.45f, 0.95f, 0.2f);
                case DamageType.Shock: return new Color(0.3f, 0.8f, 1f);
                default: return new Color(0.95f, 0.95f, 0.9f);
            }
        }

        public static string Name(DamageType type)
        {
            switch (type)
            {
                case DamageType.Fire: return "fuego";
                case DamageType.Toxic: return "tóxico";
                case DamageType.Shock: return "choque";
                default: return "físico";
            }
        }
    }
}
