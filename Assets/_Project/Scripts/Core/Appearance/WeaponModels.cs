using System;
using System.Collections.Generic;

namespace Specimen.Core.Appearance
{
    /// <summary>Carpeta y nombre de modelo de cada arma en el pack RPG Weapons («Long_Sword/Long_Sword_03»).</summary>
    public static class WeaponModels
    {
        public const int Variants = 7;

        private static readonly Dictionary<WeaponId, string> Folders = new Dictionary<WeaponId, string>
        {
            [WeaponId.EspadaLarga] = "Long_Sword", [WeaponId.EspadaCorta] = "Short_Sword",
            [WeaponId.Sable] = "Sabre", [WeaponId.Daga] = "Dagger", [WeaponId.Hacha] = "Axe",
            [WeaponId.Martillo] = "Hammer", [WeaponId.Maza] = "Mace", [WeaponId.Lanza] = "Spear",
            [WeaponId.Pico] = "Pickaxe", [WeaponId.Pala] = "Shovel"
        };

        private static readonly Dictionary<WeaponId, float> Lengths = new Dictionary<WeaponId, float>
        {
            [WeaponId.EspadaLarga] = 1.1f, [WeaponId.EspadaCorta] = 0.75f, [WeaponId.Sable] = 0.9f,
            [WeaponId.Daga] = 0.4f, [WeaponId.Hacha] = 0.8f, [WeaponId.Martillo] = 0.9f, [WeaponId.Maza] = 0.75f,
            [WeaponId.Lanza] = 1.9f, [WeaponId.Pico] = 0.85f, [WeaponId.Pala] = 1.2f
        };

        public static string Folder(WeaponId weapon) => Folders[weapon];

        /// <summary>Longitud en metros a la que se escala el arma (los modelos del pack no comparten escala).</summary>
        public static float LengthOf(WeaponId weapon) => Lengths[weapon];

        public static string Model(WeaponId weapon, int variant)
        {
            if (variant < 1 || variant > Variants)
                throw new ArgumentOutOfRangeException(nameof(variant), variant, $"Variante entre 1 y {Variants}.");
            return $"{Folder(weapon)}_{variant:00}";
        }
    }
}
