using System;
using System.Collections.Generic;
using System.Linq;
using Specimen.Core.Crafting.Authoring;
using Specimen.Runtime.Crafting.Authoring;
using UnityEditor;
using UnityEngine;

namespace Specimen.Editor.Oficios
{
    /// <summary>Menús de datos de oficios (GDD 17.7): validar la base de datos y generar los assets por defecto.</summary>
    public static class CraftingDataMenu
    {
        private const string Title = "SPECIMEN · Oficios";

        [MenuItem("SPECIMEN/Oficios/Validar datos")]
        public static void Validate()
        {
            var databases = AssetDatabase.FindAssets("t:" + nameof(CraftingDatabase))
                .Select(g => AssetDatabase.LoadAssetAtPath<CraftingDatabase>(AssetDatabase.GUIDToAssetPath(g)))
                .Where(db => db != null).ToList();
            if (databases.Count == 0)
            {
                EditorUtility.DisplayDialog(Title, "No hay ninguna CraftingDatabase. Usa «Generar assets por defecto».", "OK");
                return;
            }
            var total = 0;
            foreach (var db in databases)
            {
                var issues = Check(db);
                foreach (var issue in issues) Debug.LogError($"[Oficios] {db.name}: {issue}", db);
                total += issues.Count;
            }
            var message = total == 0 ? "Datos de oficios correctos." : $"{total} problemas (detalle en la consola).";
            EditorUtility.DisplayDialog(Title, message, "OK");
        }

        [MenuItem("SPECIMEN/Oficios/Generar assets por defecto")]
        public static void GenerateDefaults()
        {
            var created = CraftingAssetGenerator.Generate(DefaultCraftingData.Create());
            Selection.activeObject = AssetDatabase.LoadAssetAtPath<CraftingDatabase>(CraftingPaths.Database);
            EditorUtility.DisplayDialog(Title, $"{created} assets creados en {CraftingPaths.Root} (los existentes no se tocan).", "OK");
        }

        /// <summary>Validación de datos y, si pasa, conversión completa al núcleo (captura lo que el validador no ve).</summary>
        private static List<string> Check(CraftingDatabase db)
        {
            var issues = db.Validate().Select(i => i.ToString()).ToList();
            if (issues.Count > 0) return issues;
            try
            {
                db.Build();
            }
            catch (ArgumentException e)
            {
                issues.Add($"[Conversión] {e.Message}");
            }
            return issues;
        }
    }
}
