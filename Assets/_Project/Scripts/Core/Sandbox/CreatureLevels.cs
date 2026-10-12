using System;
using Specimen.Core.Bestiary;
using Specimen.Core.Combat.Bosses;
using Specimen.Core.Common;

namespace Specimen.Core.Sandbox
{
    /// <summary>
    /// Nivel de cada criatura del poblado y su escalado (vida +25 %, daño +15 % y 40 de armadura por nivel),
    /// para que el nivel del jugador, su armadura y su arma importen como en WoW.
    /// </summary>
    public static class CreatureLevels
    {
        public const float HealthPerLevel = 0.25f;
        public const float DamagePerLevel = 0.15f;
        public const float ArmorPerLevel = 40f;

        public static int LevelOf(BestiaryEntry entry)
        {
            switch (Guard.NotNull(entry, nameof(entry)).Id)
            {
                case "diablillo": return 2;
                case "diablillo-toxico": return 2;
                case "diablillo-voltaico": return 3;
                case "puglin": return 4;
                case "puglin-bruto": return 5;
                case ActOneBoss.Id: return 8;
                default: return 1;
            }
        }

        public static float Health(float baseHealth, int level) => baseHealth * (1f + HealthPerLevel * Steps(level));

        public static float Damage(float baseDamage, int level) => baseDamage * (1f + DamagePerLevel * Steps(level));

        public static float Armor(float baseArmor, int level) => baseArmor + ArmorPerLevel * Math.Max(1, level);

        private static int Steps(int level) => Math.Max(0, level - 1);
    }
}
