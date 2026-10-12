using Specimen.Core.Appearance;
using Specimen.Core.Classes;
using Specimen.Core.Combat.Attacks;

namespace Specimen.Core.Animation
{
    /// <summary>Empuñadura de cada kit según su arma y su estilo de ataque.</summary>
    public static class KitGrips
    {
        public static WeaponGrip For(ClassKitId kit)
        {
            var loadout = ClassLoadouts.For(kit);
            if (loadout.Style == AttackStyle.Hechizo) return WeaponGrip.Magia;
            switch (loadout.Weapon)
            {
                case WeaponId.Lanza:
                    return WeaponGrip.Asta;
                case WeaponId.Martillo:
                case WeaponId.Hacha:
                case WeaponId.EspadaLarga:
                case WeaponId.Pico:
                case WeaponId.Pala:
                    return WeaponGrip.DosManos;
                default:
                    return WeaponGrip.UnaMano;
            }
        }
    }
}
