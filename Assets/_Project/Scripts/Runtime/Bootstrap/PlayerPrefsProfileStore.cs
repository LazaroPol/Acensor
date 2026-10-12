using Specimen.Core.Profile;
using UnityEngine;

namespace Specimen.Runtime.Bootstrap
{
    /// <summary>Guarda el perfil (ADN y fragmentos) en PlayerPrefs: sobrevive a nuevas partidas y a cerrar el juego.</summary>
    public sealed class PlayerPrefsProfileStore : IProfileStore
    {
        private const string Key = "specimen.profile.v1";

        public string Read() => PlayerPrefs.GetString(Key, string.Empty);

        public void Write(string text)
        {
            PlayerPrefs.SetString(Key, text ?? string.Empty);
            PlayerPrefs.Save();
        }
    }
}
