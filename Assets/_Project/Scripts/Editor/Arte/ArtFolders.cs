using UnityEditor;

namespace Specimen.Editor.Arte
{
    /// <summary>Crea carpetas del AssetDatabase de forma recursiva («a/b/c» crea las que falten).</summary>
    public static class ArtFolders
    {
        public static void Ensure(string path)
        {
            if (AssetDatabase.IsValidFolder(path)) return;
            int slash = path.LastIndexOf('/');
            string parent = path.Substring(0, slash);
            Ensure(parent);
            AssetDatabase.CreateFolder(parent, path.Substring(slash + 1));
        }

        public static string FileName(string folder, string name) => $"{folder}/{name}";
    }
}
