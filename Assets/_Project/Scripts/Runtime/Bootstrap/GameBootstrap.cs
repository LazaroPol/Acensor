using Specimen.Core.Composition;
using UnityEngine;

namespace Specimen.Runtime.Bootstrap
{
    /// <summary>
    /// Punto de entrada único: antes de cargar cualquier escena crea el contenedor de dependencias
    /// y lo deja accesible a las escenas. Las escenas piden sus servicios a <see cref="Services"/>
    /// en vez de buscar objetos globales.
    /// </summary>
    public static class GameBootstrap
    {
        private static ServiceContainer _services;

        public static IServiceResolver Services => _services ??= GameComposition.Build(new UnitySceneLoader(), new PlayerPrefsProfileStore());

        /// <summary>Descarta la partida actual: el siguiente acceso a <see cref="Services"/> crea una nueva.</summary>
        public static void NewGame() => _services = null;

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
        private static void Reset() => _services = null;

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        private static void Init() => _ = Services;
    }
}
