using Specimen.Core.Composition;
using Specimen.Core.Dna;
using Specimen.Core.Iteration;
using Specimen.Core.Lab;
using Specimen.Core.Profile;
using Specimen.Runtime.Bootstrap;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Specimen.Runtime.Lab
{
    /// <summary>
    /// Monta el Laboratorio Genético al cargar su escena (puede estar vacía). Es el menú entre partidas: muestra
    /// el árbol de ADN del perfil persistente, guarda cada desbloqueo y permite empezar una nueva incursión.
    /// </summary>
    public sealed class LabBootstrap : MonoBehaviour
    {
        /// <summary>Lo llama <c>SceneBootstrapper</c> en cada carga de escena; solo actúa en la suya.</summary>
        public static void Ensure()
        {
            if (SceneManager.GetActiveScene().name != nameof(SceneId.Laboratorio)) return;
            if (FindAnyObjectByType<LabBootstrap>() != null) return;
            new GameObject(nameof(LabBootstrap)).AddComponent<LabBootstrap>();
        }

        private void Start()
        {
            var s = GameBootstrap.Services;
            var profile = s.Resolve<PlayerProfile>();
            var flow = s.Resolve<SceneFlow>();
            var run = s.Resolve<IterationRun>();
            var lab = new LabPresenter(profile.Tree, profile.Dna, TierLock.CreateDefault());
            var actions = new LabActions(
                () => { if (lab.UnlockSelected() == UnlockResult.Ok) profile.Save(); },
                NewRun,
                () => run.Phase != IterationPhase.Ended && flow.CanGoTo(SceneId.Ascensor),
                () => flow.GoTo(SceneId.Ascensor));
            gameObject.AddComponent<LabScreen>().Init(lab, actions);
        }

        /// <summary>Descarta la partida terminada y aparece en la Armería; el perfil se recarga del almacén.</summary>
        private static void NewRun()
        {
            GameBootstrap.NewGame();
            SceneManager.LoadScene(nameof(SceneId.Armeria));
        }
    }
}
