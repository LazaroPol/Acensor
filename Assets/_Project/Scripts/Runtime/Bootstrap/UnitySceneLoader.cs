using Specimen.Core.Composition;
using UnityEngine.SceneManagement;

namespace Specimen.Runtime.Bootstrap
{
    /// <summary>Adaptador de <see cref="ISceneLoader"/> sobre SceneManager (el nombre de la escena es el del enum).</summary>
    public sealed class UnitySceneLoader : ISceneLoader
    {
        public void Load(SceneId scene) => SceneManager.LoadScene(scene.ToString());
    }
}
