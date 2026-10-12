using Specimen.Core.Composition;
using Specimen.Core.Dna;
using Specimen.Core.Lab;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Specimen.Runtime.Lab
{
    /// <summary>
    /// Monta la pantalla del Laboratorio al cargar la escena «Laboratorio» (puede estar vacía).
    /// Mientras no haya perfil persistente, regala puntos de ADN para poder probar el árbol.
    /// </summary>
    public sealed class LabBootstrap : MonoBehaviour
    {
        private const int DemoPoints = 12;

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
        private static void EnsureInLabScene()
        {
            if (SceneManager.GetActiveScene().name != nameof(SceneId.Laboratorio)) return;
            if (FindAnyObjectByType<LabBootstrap>() != null) return;
            new GameObject(nameof(LabBootstrap)).AddComponent<LabBootstrap>();
        }

        private void Start()
        {
            var tree = DnaTree.CreateDefault();
            var dna = new DnaAllocation(tree, TierLock.CreateDefault());
            dna.AddPoints(DemoPoints);
            gameObject.AddComponent<LabScreen>().Init(new LabPresenter(tree, dna, TierLock.CreateDefault()));
        }
    }
}
