using System.Collections.Generic;
using Specimen.Core.Armory;
using Specimen.Core.Classes;
using Specimen.Core.Composition;
using Specimen.Core.Events;
using Specimen.Core.Hud;
using Specimen.Core.Iteration;
using Specimen.Core.Prototype;
using Specimen.Core.Visuals;
using Specimen.Runtime.Bootstrap;
using Specimen.Runtime.Combat;
using Specimen.Runtime.Incursion;
using Specimen.Runtime.Player;
using Specimen.Runtime.Prototype;
using Specimen.Runtime.Visuals;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Specimen.Runtime.Atico
{
    /// <summary>
    /// Escena «Atico»: la azotea de la torre donde se resiste el asedio y llega el Osprey. Monta la plataforma
    /// greybox, al jugador con su kit, cámara y HUD. La escena puede estar vacía.
    /// </summary>
    public sealed class AticoBootstrap : MonoBehaviour
    {
        public const int Size = 30;
        private IterationRun _run;
        private PlayerController _player;
        private SiegeController _siege;
        private OspreyPad _pad;

        /// <summary>Lo llama <c>SceneBootstrapper</c> en cada carga de escena; solo actúa en la suya.</summary>
        public static void Ensure()
        {
            if (SceneManager.GetActiveScene().name != nameof(SceneId.Atico)) return;
            if (FindAnyObjectByType<AticoBootstrap>() != null) return;
            new GameObject(nameof(AticoBootstrap)).AddComponent<AticoBootstrap>();
        }

        private void Start()
        {
            var s = GameBootstrap.Services;
            _run = s.Resolve<IterationRun>();
            var spawner = new GameObject("Azotea").AddComponent<VisualSpawner>();
            spawner.Init(new VisualCatalog<GameObject>(new DefaultGreyboxStyles()));
            foreach (var placement in Platform()) spawner.Spawn(placement);
            var catalog = s.Resolve<IClassKitCatalog>();
            var kit = s.Resolve<ClassKitArmory>().TryGetKit(GameComposition.LocalPlayerId, out var mine) ? mine : catalog.All[0];
            var player = IncursionPlayer.Spawn(new Vector3(Size / 2f, 1.2f, Size / 2f), kit.CreateStatSheet(), _run,
                new HudNotices(s.Resolve<IEventBus>()));
            player.gameObject.AddComponent<PlayerAttack>().Init(kit.CreateStatSheet());
            _player = player;
            player.gameObject.AddComponent<KitBadge>().Init(s);
            _siege = gameObject.AddComponent<SiegeController>();
            _siege.Init(_run, player.transform, player.Vitals);
            _pad = gameObject.AddComponent<OspreyPad>();
            _pad.Init(_run, player.transform);
            PrototypeScenery.EnsureLight();
        }

        private static IEnumerable<LevelPlacement> Platform()
        {
            for (var x = 0; x < Size; x++)
                for (var z = 0; z < Size; z++)
                    yield return new LevelPlacement(VisualId.Suelo, x, 0f, z);
        }

        private void Update()
        {
            if (_run == null) return;
            _run.Tick(Time.deltaTime);
            if (_siege.Survived) _pad.Arrive();
            if (!_player.Vitals.IsAlive && _run.Phase != IterationPhase.Ended) Respawn();
        }

        private void Respawn()
        {
            _player.Vitals.RefillAll();
            var body = _player.GetComponent<CharacterController>();
            body.enabled = false;
            _player.transform.position = new Vector3(Size / 2f, 1.2f, Size / 2f);
            body.enabled = true;
        }

        private void OnGUI()
        {
            if (_run == null) return;
            if (_run.Phase == IterationPhase.Ended) { EndScreen(); return; }
            string hint = _pad.Arrived ? "El Osprey ha llegado: acércate y pulsa E" : _siege.Status;
            GUI.Label(new Rect(10f, Screen.height - 30f, 600f, 24f), hint);
        }

        private void EndScreen()
        {
            GUI.Label(new Rect(Screen.width / 2f - 150f, Screen.height / 2f - 40f, 300f, 28f), "¡Extracción completa! Victoria");
            if (!GUI.Button(new Rect(Screen.width / 2f - 100f, Screen.height / 2f, 200f, 36f), "Nueva partida")) return;
            GameBootstrap.NewGame();
            SceneManager.LoadScene(nameof(SceneId.Armeria));
        }
    }
}
