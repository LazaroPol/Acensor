using System.Collections.Generic;
using Specimen.Core.Stats;
using T = Specimen.Core.Classes.ExplorationTraits;

namespace Specimen.Core.Classes
{
    /// <summary>Datos de los 6 kits del GDD §4. Valores base provisionales, a ajustar en balance.</summary>
    public static class DefaultClassKits
    {
        public static IReadOnlyList<ClassKit> Create() => new[]
        {
            Kit(ClassKitId.Bastion, "Bastión", ClassRole.Tank, ClassResourceType.Fury,
                T.PushBlocks | T.TrapResistance, hp: 160, res: 100, sta: 90, spd: 4.5f, dmg: 10, armor: 30, aspd: 0.9f, crit: 0.05f),
            Kit(ClassKitId.Vigia, "Vigía", ClassRole.Tank, ClassResourceType.Focus,
                T.Climb, hp: 130, res: 100, sta: 130, spd: 5.5f, dmg: 11, armor: 18, aspd: 1.1f, crit: 0.08f),
            Kit(ClassKitId.Oraculo, "Oráculo", ClassRole.Support, ClassResourceType.Mana,
                T.RevealHidden, hp: 100, res: 150, sta: 100, spd: 5f, dmg: 8, armor: 10, aspd: 1f, crit: 0.05f),
            Kit(ClassKitId.DruidaCeniza, "Druida de Ceniza", ClassRole.Support, ClassResourceType.Essence,
                T.Glide, hp: 105, res: 130, sta: 100, spd: 5f, dmg: 9, armor: 12, aspd: 1f, crit: 0.05f),
            Kit(ClassKitId.Duelista, "Duelista", ClassRole.Dps, ClassResourceType.Energy,
                T.DoubleJump | T.Dash, hp: 110, res: 100, sta: 120, spd: 6f, dmg: 16, armor: 12, aspd: 1.3f, crit: 0.15f),
            Kit(ClassKitId.Arcanista, "Arcanista", ClassRole.Dps, ClassResourceType.Mana,
                T.ShortTeleport | T.RemoteActivation, hp: 90, res: 140, sta: 100, spd: 5f, dmg: 15, armor: 8, aspd: 1f, crit: 0.12f)
        };

        private static ClassKit Kit(ClassKitId id, string name, ClassRole role, ClassResourceType resource, T traits,
            float hp, float res, float sta, float spd, float dmg, float armor, float aspd, float crit)
            => new ClassKit(id, name, role, resource, traits, new Dictionary<StatType, float>
            {
                { StatType.MaxHealth, hp }, { StatType.MaxClassResource, res }, { StatType.MaxStamina, sta },
                { StatType.MoveSpeed, spd }, { StatType.Damage, dmg }, { StatType.Armor, armor },
                { StatType.AttackSpeed, aspd }, { StatType.CritChance, crit }
            });
    }
}
