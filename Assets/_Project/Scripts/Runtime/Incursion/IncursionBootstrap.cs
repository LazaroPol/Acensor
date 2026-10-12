using Specimen.Core.Armory;
using Specimen.Core.Classes;
using Specimen.Core.Composition;
using Specimen.Core.Events;
using Specimen.Core.Floors;
using Specimen.Core.Hud;
using Specimen.Core.Iteration;
using Specimen.Core.Visuals;
using Specimen.Runtime.Bootstrap;
using Specimen.Runtime.Prototype;
using Specimen.Runtime.Visuals;
using UnityEngine.InputSystem;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Specimen.Runtime.Incursion
{
    /// <summary>
    /// Escena «Incursion»: genera el piso, lo monta en greybox, coloca al jugador con el kit de la Armería
    /// y lo devuelve al Ascensor (tecla E junto a él) o cuando la fase termina. La escena puede estar vacía.
    /// </summary>
    public sealed class IncursionBootstrap : MonoBehaviour
    {
        private const float InteractRange = 3f;
        private IterationRun _run;
        private SceneFlow _flow;
        private Transform _player;
        private Vector3 _elevator;

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
        private static void EnsureInIncursionScene()
        {
            if (SceneManager.GetActiveScene().name != nameof(SceneId.Incursion)) return;
            if (FindAnyObjectByType<IncursionBootstrap>() != null) return;
            new GameObject(nameof(IncursionBootstrap)).AddComponent<IncursionBootstrap>();
        }

        private void Start()
        {
            var s = GameBootstrap.Services;
            _run = s.Resolve<IterationRun>();
            _flow = s.Resolve<SceneFlow>();
            var floor = new RingCorridorGenerator(RingLayoutConfig.Default).Generate((ulong)(_run.Floor + 1) * 7919UL);
            var layout = new FloorGreyboxLayout(floor);
            var spawner = new GameObject("Nivel").AddComponent<VisualSpawner>();
            spawner.Init(new VisualCatalog<GameObject>(new DefaultGreyboxStyles()));
            foreach (var placement in layout.Build()) spawner.Spawn(placement);
            var (x, z) = layout.SpawnPoint;
            _elevator = new Vector3(x, 0f, z + 2f);
            var kit = KitOf(s);
            var player = IncursionPlayer.Spawn(new Vector3(x, 1.2f, z), kit.CreateStatSheet(), _run,
                new HudNotices(s.Resolve<IEventBus>()));
            _player = player.transform;
            _run.LeaveElevator(GameComposition.LocalPlayerId);
            PrototypeScenery.EnsureLight();
        }

        private static ClassKit KitOf(IServiceResolver s)
        {
            var catalog = s.Resolve<IClassKitCatalog>();
            return s.Resolve<ClassKitArmory>().TryGetKit(GameComposition.LocalPlayerId, out var kit) ? kit : catalog.All[0];
        }

        private void Update()
        {
            if (_run == null) return;
            _run.Tick(Time.deltaTime);
            if (_run.Phase != IterationPhase.Incursion) { _flow.GoTo(SceneId.Ascensor); return; }
            bool near = (_player.position - _elevator).sqrMagnitude < InteractRange * InteractRange;
            if (near && Keyboard.current != null && Keyboard.current.eKey.wasPressedThisFrame)
            {
                _run.ReturnToElevator(GameComposition.LocalPlayerId);
                _flow.GoTo(SceneId.Ascensor);
            }
        }

        private void OnGUI() =>
            GUI.Label(new Rect(10f, Screen.height - 30f, 600f, 24f), "E junto al Ascensor: volver · busca el Hueco de Emergencia");
    }
}
