using System.Collections.Generic;
using Specimen.Core.Classes;

namespace Specimen.Core.Appearance
{
    /// <summary>Arma de cada kit: los de melé llevan armas de filo o contundentes; los de hechizo, un foco.</summary>
    public static class ClassLoadouts
    {
        private static readonly Dictionary<ClassKitId, ClassLoadout> Loadouts = new Dictionary<ClassKitId, ClassLoadout>
        {
            [ClassKitId.Bastion] = new ClassLoadout(ClassKitId.Bastion, WeaponId.Martillo, 2),
            [ClassKitId.Vigia] = new ClassLoadout(ClassKitId.Vigia, WeaponId.Lanza, 1),
            [ClassKitId.Duelista] = new ClassLoadout(ClassKitId.Duelista, WeaponId.Sable, 3),
            [ClassKitId.Oraculo] = new ClassLoadout(ClassKitId.Oraculo, WeaponId.Maza, 5),
            [ClassKitId.DruidaCeniza] = new ClassLoadout(ClassKitId.DruidaCeniza, WeaponId.Hacha, 4),
            [ClassKitId.Arcanista] = new ClassLoadout(ClassKitId.Arcanista, WeaponId.Daga, 6)
        };

        public static ClassLoadout For(ClassKitId kit) => Loadouts[kit];
    }
}
