using Specimen.Core.Prototype;
using Specimen.Core.Visuals;
using Specimen.Runtime.Visuals;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Specimen.Runtime.Prototype
{
    /// <summary>
    /// Arranque de la escena Prototipo: construye la sala greybox desde el catálogo visual
    /// y monta el jugador con su cámara en tercera persona (PG-03).
    /// Si la escena no trae bootstrap, se crea solo (la escena puede quedar casi vacía).
    /// </summary>
    public sealed class PrototypeBootstrap : MonoBehaviour
    {
        public const string SceneName = "Prototipo";

        [SerializeField] private CatalogoVisual catalogo;
        [SerializeField] private int anchoSala = 10;
        [SerializeField] private int fondoSala = 8;

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
        private static void EnsureInPrototypeScene()
        {
            if (SceneManager.GetActiveScene().name != SceneName) return;
            if (FindFirstObjectByType<PrototypeBootstrap>() != null) return;
            new GameObject(nameof(PrototypeBootstrap)).AddComponent<PrototypeBootstrap>();
        }

        private void Start()
        {
            var defaults = new DefaultGreyboxStyles();
            IVisualCatalog<GameObject> catalog = catalogo != null
                ? catalogo.Build(defaults)
                : new VisualCatalog<GameObject>(defaults);
            var spawner = new GameObject("Nivel").AddComponent<VisualSpawner>();
            spawner.Init(catalog);
            foreach (var placement in new GreyboxRoomLayout(anchoSala, fondoSala).Build())
            {
                if (placement.Id == VisualId.Jugador) PrototypePlayer.Spawn(spawner, placement);
                else spawner.Spawn(placement);
            }
            PrototypeScenery.EnsureLight();
        }
    }
}
