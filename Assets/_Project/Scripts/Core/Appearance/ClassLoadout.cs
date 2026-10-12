using Specimen.Core.Classes;
using Specimen.Core.Combat.Attacks;

namespace Specimen.Core.Appearance
{
    /// <summary>Equipo visible de un kit: arma (tipo y variante) y estilo de ataque con el que se usa.</summary>
    public sealed class ClassLoadout
    {
        public ClassLoadout(ClassKitId kit, WeaponId weapon, int variant)
        {
            Kit = kit;
            Weapon = weapon;
            Variant = variant;
            WeaponModels.Model(weapon, variant);
        }

        public ClassKitId Kit { get; }
        public WeaponId Weapon { get; }
        public int Variant { get; }
        public string Model => WeaponModels.Model(Weapon, Variant);
        public string Folder => WeaponModels.Folder(Weapon);
        public AttackStyle Style => ClassAttackProfiles.For(Kit).Style;
    }
}
