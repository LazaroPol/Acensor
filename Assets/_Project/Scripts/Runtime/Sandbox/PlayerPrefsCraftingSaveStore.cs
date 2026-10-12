using Specimen.Core.Crafting.Save;
using UnityEngine;

namespace Specimen.Runtime.Sandbox
{
    /// <summary>Guardado de oficios del Campo de pruebas en PlayerPrefs (GDD §17.7), con copia .bak si se corrompe.</summary>
    public sealed class PlayerPrefsCraftingSaveStore : ICraftingSaveStore
    {
        public const string Key = "specimen.oficios.v1";
        public const string BackupKey = Key + ".bak";

        public string Read() => PlayerPrefs.HasKey(Key) ? PlayerPrefs.GetString(Key) : null;

        public void Write(string text) => Store(Key, text);

        public void WriteBackup(string text) => Store(BackupKey, text);

        private static void Store(string key, string text)
        {
            PlayerPrefs.SetString(key, text ?? string.Empty);
            PlayerPrefs.Save();
        }
    }
}
