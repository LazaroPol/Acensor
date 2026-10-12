using Specimen.Core.Armory;
using Specimen.Core.Classes;
using Specimen.Core.Composition;
using Specimen.Core.Events;
using Specimen.Core.Floors;
using Specimen.Core.Hud;
using Specimen.Core.Iteration;
using Specimen.Core.Vitals;
using Specimen.Core.Visuals;
using Specimen.Runtime.Bootstrap;
using Specimen.Runtime.Combat;
using Specimen.Runtime.Player;
using Specimen.Runtime.Prototype;
using Specimen.Runtime.Visuals;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Specimen.Runtime.Incursion
{
    /// <summary>
    /// Escena «Incursion»: genera el piso (laberinto de salas con enemigos y botín), coloca al jugador con el
    /// kit de la Armería y resuelve la salida: volver al Ascensor a tiempo (E) o, si se va sin él, saltar por
    /// el Hueco de Emergencia y cerrar la partida en fracaso. La escena puede estar vacía.
    /// </summary>
    public sealed partial class IncursionBootstrap : MonoBehaviour
    {
        private const float InteractRange = 3f;
        private IterationRun _run;
        private SceneFlow _flow;
        private Transform _player;
        private Vector3 _elevator;
        private Vector3 _shaft;
        private CharacterVitals _vitals;
        private LootPickups _loot;

        /// <summary>Lo llama <c>SceneBootstrapper</c> en cada carga de escena; solo actúa en la suya.</summary>
        public static void Ensure()
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
            var (sx, sz) = layout.ShaftPoint;
            _shaft = new Vector3(sx, 0f, sz);
            var kit = KitOf(s);
            var player = IncursionPlayer.Spawn(new Vector3(x, 1.2f, z), kit.CreateStatSheet(), _run,
                new HudNotices(s.Resolve<IEventBus>()));
            _player = player.transform;
            _vitals = player.Vitals;
            player.gameObject.AddComponent<KitBadge>().Init(s);
            player.gameObject.AddComponent<PlayerAttack>().Init(kit, player.Vitals);
            EnemySpawner.Populate(floor, layout, _run.Floor, _player, player.Vitals);
            _loot = gameObject.AddComponent<LootPickups>();
            _loot.Init(floor, layout, _player);
            _run.LeaveElevator(GameComposition.LocalPlayerId);
            PrototypeScenery.EnsureLight();
        }

        private static ClassKit KitOf(IServiceResolver s)
        {
            var catalog = s.Resolve<IClassKitCatalog>();
            return s.Resolve<ClassKitArmory>().TryGetKit(GameComposition.LocalPlayerId, out var kit) ? kit : catalog.All[0];
        }

        private bool Near(Vector3 point) => (_player.position - point).sqrMagnitude < InteractRange * InteractRange;

        private void Respawn()
        {
            _vitals.RefillAll();
            var body = _player.GetComponent<CharacterController>();
            body.enabled = false;
            _player.position = _elevator + Vector3.back * 2f + Vector3.up;
            body.enabled = true;
        }
    }
}
