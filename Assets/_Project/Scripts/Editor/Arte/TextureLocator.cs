using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace Specimen.Editor.Arte
{
    /// <summary>Índice nombre de fichero → textura de unas carpetas; la primera carpeta tiene prioridad.</summary>
    public sealed class TextureLocator
    {
        private readonly Dictionary<string, string> _paths =
            new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);

        public TextureLocator(params string[] folders)
        {
            foreach (string folder in folders.Where(AssetDatabase.IsValidFolder))
                foreach (string guid in AssetDatabase.FindAssets("t:Texture2D", new[] { folder }))
                {
                    string path = AssetDatabase.GUIDToAssetPath(guid);
                    string name = Path.GetFileNameWithoutExtension(path);
                    if (!_paths.ContainsKey(name)) _paths[name] = path;
                }
        }

        public int Count => _paths.Count;

        public Texture2D Find(string name)
            => name != null && _paths.TryGetValue(name, out var path)
                ? AssetDatabase.LoadAssetAtPath<Texture2D>(path)
                : null;

        public Texture2D FindFirst(IEnumerable<string> names)
        {
            foreach (string name in names)
            {
                var texture = Find(name);
                if (texture != null) return texture;
            }
            return null;
        }
    }
}
