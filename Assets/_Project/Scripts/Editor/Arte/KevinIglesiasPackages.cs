using System.IO;
using UnityEditor;
using UnityEngine;

namespace Specimen.Editor.Arte
{
    /// <summary>
    /// Importa sin diálogo los .unitypackage de animaciones de Kevin Iglesias que viven en
    /// &lt;proyecto&gt;/ThirdPartyPackages (fuera de git por su licencia). Se instalan en Assets/Kevin Iglesias.
    /// </summary>
    public static class KevinIglesiasPackages
    {
        [MenuItem("SPECIMEN/Arte/1 · Importar paquetes de animación (Kevin Iglesias)")]
        public static void ImportAll()
        {
            string folder = Path.Combine(Directory.GetCurrentDirectory(), ArtPaths.PackagesFolder);
            if (!Directory.Exists(folder))
            {
                Debug.LogWarning($"[SPECIMEN] No existe {folder}: copia ahí los .unitypackage (opcional).");
                return;
            }
            foreach (string package in Directory.GetFiles(folder, "*.unitypackage"))
            {
                Debug.Log($"[SPECIMEN] Importando {Path.GetFileName(package)}…");
                UnityEditor.AssetPackage.Package.Import(package, false);
            }
        }

        public static bool IsImported() => AssetDatabase.IsValidFolder(ArtPaths.KevinIglesias);
    }
}
