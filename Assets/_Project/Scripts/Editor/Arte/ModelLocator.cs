using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace Specimen.Editor.Arte
{
    /// <summary>Índice nombre de fichero → modelo (FBX) de unas carpetas; la primera carpeta tiene prioridad.</summary>
    public sealed class ModelLocator
    {
        private readonly Dictionary<string, string> _paths =
            new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);

        public ModelLocator(params string[] folders)
        {
            foreach (string folder in folders.Where(AssetDatabase.IsValidFolder))
                foreach (string guid in AssetDatabase.FindAssets("t:Model", new[] { folder }))
                {
                    string path = AssetDatabase.GUIDToAssetPath(guid);
                    string name = Path.GetFileNameWithoutExtension(path);
                    if (!_paths.ContainsKey(name)) _paths[name] = path;
                }
        }

        public IEnumerable<string> Names => _paths.Keys;

        public string PathOf(string name) => name != null && _paths.TryGetValue(name, out var p) ? p : null;

        public GameObject Load(string name)
        {
            string path = PathOf(name);
            if (path == null)
            {
                Debug.LogWarning($"[SPECIMEN] Modelo no encontrado: {name}");
                return null;
            }
            return AssetDatabase.LoadAssetAtPath<GameObject>(path);
        }

        public Avatar AvatarOf(string name)
        {
            string path = PathOf(name);
            return path == null ? null : AssetDatabase.LoadAllAssetsAtPath(path).OfType<Avatar>().FirstOrDefault();
        }
    }
}
