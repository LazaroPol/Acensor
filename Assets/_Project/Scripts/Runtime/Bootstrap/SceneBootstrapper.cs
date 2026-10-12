using Specimen.Runtime.Atico;
using Specimen.Runtime.Extraction;
using Specimen.Runtime.Hubs;
using Specimen.Runtime.Incursion;
using Specimen.Runtime.Lab;
using Specimen.Runtime.Prototype;
using Specimen.Runtime.Sandbox;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Specimen.Runtime.Bootstrap
{
    /// <summary>
    /// Monta el bootstrap de cada escena en TODAS las cargas (la primera y las que hace <see cref="UnitySceneLoader"/>).
    /// <c>RuntimeInitializeOnLoadMethod</c> solo corre una vez por sesión, por eso aquí se escucha <c>sceneLoaded</c>.
    /// </summary>
    public static class SceneBootstrapper
    {
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        private static void Hook()
        {
            SceneManager.sceneLoaded -= OnSceneLoaded;
            SceneManager.sceneLoaded += OnSceneLoaded;
        }

        private static void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            HubBootstrap.Ensure();
            IncursionBootstrap.Ensure();
            AticoBootstrap.Ensure();
            ExtraccionBootstrap.Ensure();
            LabBootstrap.Ensure();
            PrototypeBootstrap.Ensure();
            SandboxBootstrap.Ensure();
        }
    }
}
