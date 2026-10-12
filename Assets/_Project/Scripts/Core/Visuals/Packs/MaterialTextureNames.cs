using System;
using System.Collections.Generic;

namespace Specimen.Core.Visuals.Packs
{
    /// <summary>
    /// Convenciones de nombres de los packs (Quaternius, KayKit, RPG Weapons): de un nombre de material
    /// importado («MI_Ranger») a las texturas candidatas, en orden de preferencia («T_Ranger_BaseColor»…).
    /// </summary>
    public static class MaterialTextureNames
    {
        private static readonly Dictionary<string, string> Specials =
            new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
            {
                ["Eyes"] = "T_Eye_Brown",
                ["Material"] = "Texture",
                ["tools"] = "tools_bits_texture"
            };

        /// <summary>Clave del material sin prefijos (MI_, M_) ni sufijos de instancia o duplicado.</summary>
        public static string KeyOf(string materialName)
        {
            if (string.IsNullOrWhiteSpace(materialName)) return string.Empty;
            string key = materialName.Replace(" (Instance)", string.Empty).Trim();
            int dot = key.LastIndexOf('.');
            if (dot > 0) key = key.Substring(0, dot);
            if (key.StartsWith("MI_", StringComparison.Ordinal)) return key.Substring(3);
            if (key.StartsWith("M_", StringComparison.Ordinal)) return key.Substring(2);
            return key;
        }

        /// <summary>Texturas de color candidatas; <paramref name="preferred"/> (variante) va primero.</summary>
        public static IReadOnlyList<string> BaseColorCandidates(string materialName, string preferred = null)
        {
            string key = KeyOf(materialName);
            var result = new List<string>();
            if (!string.IsNullOrEmpty(preferred)) result.Add(preferred);
            if (Specials.TryGetValue(key, out var special)) result.Add(special);
            result.Add($"T_{key}_BaseColor");
            result.Add($"T_{key}_BaseColor_1");
            result.Add($"T_{key}_Dark_BaseColor");
            result.Add($"T_{key}_Dark");
            result.Add($"T_{key}");
            return result;
        }

        public static string NormalOf(string materialName) => $"T_{KeyOf(materialName)}_Normal";

        public static string EmissiveOf(string materialName) => $"T_{KeyOf(materialName)}_Emissive";
    }
}
