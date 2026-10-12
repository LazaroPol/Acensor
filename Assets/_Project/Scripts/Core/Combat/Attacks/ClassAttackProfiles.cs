using System.Collections.Generic;
using Specimen.Core.Classes;

namespace Specimen.Core.Combat.Attacks
{
    /// <summary>Ataque básico de cada kit. Valores provisionales para el Campo de pruebas, a ajustar en balance.</summary>
    public static class ClassAttackProfiles
    {
        private static readonly Dictionary<ClassKitId, AttackProfile> Profiles =
            new Dictionary<ClassKitId, AttackProfile>
            {
                [ClassKitId.Bastion] = Melee(DamageType.Physical, 1.2f, 2.2f, 120f, 0.9f),
                [ClassKitId.Vigia] = Melee(DamageType.Physical, 1f, 2.8f, 60f, 0.7f),
                [ClassKitId.Duelista] = Melee(DamageType.Physical, 0.9f, 2f, 100f, 0.5f),
                [ClassKitId.Oraculo] = Spell(DamageType.Shock, 1.1f, 6f),
                [ClassKitId.DruidaCeniza] = Spell(DamageType.Toxic, 1.2f, 8f),
                [ClassKitId.Arcanista] = Spell(DamageType.Fire, 1.4f, 10f)
            };

        public static AttackProfile For(ClassKitId kit) => Profiles[kit];

        private static AttackProfile Melee(DamageType type, float mult, float range, float arc, float cooldown)
            => new AttackProfile(AttackStyle.CuerpoACuerpo, type, mult, range, arc, cooldown);

        private static AttackProfile Spell(DamageType type, float mult, float cost)
            => new AttackProfile(AttackStyle.Hechizo, type, mult, 16f, 0f, 0.8f, cost, 14f);
    }
}
