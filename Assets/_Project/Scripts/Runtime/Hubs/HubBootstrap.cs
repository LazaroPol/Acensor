using Specimen.Core.Composition;
using Specimen.Core.Iteration;
using Specimen.Runtime.Bootstrap;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Specimen.Runtime.Hubs
{
    /// <summary>
    /// Monta la pantalla de las escenas Armería y Ascensor con los servicios del contenedor y avanza la
    /// iteración. Se crea sola al cargar esas escenas; la escena puede estar vacía.
    /// </summary>
    public sealed class HubBootstrap : MonoBehaviour
    {
        private IterationRun _run;
        private SceneFlow _flow;
        private IterationPhase _previous;

        /// <summary>Lo llama <c>SceneBootstrapper</c> en cada carga de escena; solo actúa en la suya.</summary>
        public static void Ensure()
        {
            string scene = SceneManager.GetActiveScene().name;
            if (scene != nameof(SceneId.Armeria) && scene != nameof(SceneId.Ascensor)) return;
            if (FindAnyObjectByType<HubBootstrap>() != null) return;
            new GameObject(nameof(HubBootstrap)).AddComponent<HubBootstrap>();
        }

        private void Start()
        {
            var s = GameBootstrap.Services;
            _run = s.Resolve<IterationRun>();
            if (!_run.HasStarted) _run.Start();
            var flow = _flow = s.Resolve<SceneFlow>();
            if (SceneManager.GetActiveScene().name == nameof(SceneId.Armeria)) ArmoryRoom.Mount(gameObject, s, _run, flow);
            else ElevatorRoom.Mount(gameObject, s, _run, flow);
        }

        private void Update()
        {
            if (_run == null) return;
            _run.Tick(Time.deltaTime);
            var phase = _run.Phase;
            if (_previous == IterationPhase.Calm && phase == IterationPhase.Incursion) _flow.GoTo(SceneId.Incursion);
            if (_previous != IterationPhase.Siege && phase == IterationPhase.Siege) _flow.GoTo(SceneId.Atico);
            _previous = phase;
        }
    }
}
