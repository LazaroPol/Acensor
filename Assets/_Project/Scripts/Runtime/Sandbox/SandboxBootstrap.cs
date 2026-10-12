using Specimen.Core.Classes;
using Specimen.Core.Visuals;
using Specimen.Runtime.Player;
using Specimen.Runtime.Prototype;
using Specimen.Runtime.Visuals;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Specimen.Runtime.Sandbox
{
    /// <summary>
    /// Arranque de la escena «CampoPruebas»: monta sala, bancos, jugador y bestiario con el arte generado
    /// (o greybox si falta) y atiende los atajos de prueba (kits, reinicio, dañar/curar).
    /// </summary>
    public sealed class SandboxBootstrap : MonoBehaviour
    {
        public const string SceneName = "CampoPruebas";

        [SerializeField] private CatalogoVisual catalogo;
        [SerializeField] private CatalogoArte arte;

        private readonly ClassKitCatalog _kits = ClassKitCatalog.CreateDefault();
        private SandboxContext _context;
        private SandboxWorld _world;
        private SandboxPlayer _player;
        private int _kitIndex;

        /// <summary>Lo llama <c>SceneBootstrapper</c> en cada carga de escena; solo actúa en la suya.</summary>
        public static void Ensure()
        {
            if (SceneManager.GetActiveScene().name != SceneName) return;
            if (FindAnyObjectByType<SandboxBootstrap>() != null) return;
            new GameObject(nameof(SandboxBootstrap)).AddComponent<SandboxBootstrap>();
        }

        private void Start()
        {
            var defaults = new DefaultGreyboxStyles();
            var visuals = catalogo != null ? catalogo.Build(defaults) : new VisualCatalog<GameObject>(defaults);
            _context = new SandboxContext(arte, visuals, new SandboxInput(), (ulong)System.Environment.TickCount);
            _world = new SandboxWorld(_context);
            _world.BuildRoom();
            _player = _world.SpawnPlayer(EnsureCamera());
            var benches = _world.PlaceBenches(_player);
            _player.Equip(_kits.All[0]);
            _world.SpawnBestiary(_player);
            new GameObject(nameof(SandboxHud)).AddComponent<SandboxHud>().Init(_context, _player, benches);
            PrototypeScenery.EnsureLight();
            if (arte == null) _context.Feed.Add("Sin CatalogoArte: todo en greybox (SPECIMEN › Arte › 2 · Generar).");
        }

        private void OnDestroy() => _context?.Input.Dispose();

        private void Update()
        {
            if (_context == null) return;
            var input = _context.Input;
            int pressed = input.ClassPressed();
            if (pressed >= 0 && pressed < _kits.All.Count) EquipIndex(pressed);
            if (input.NextClass.WasPressedThisFrame()) EquipIndex((_kitIndex + 1) % _kits.All.Count);
            if (input.Hurt.WasPressedThisFrame()) _player.Vitals.Health.Drain(15f);
            if (input.Heal.WasPressedThisFrame()) Heal();
            if (input.Restart.WasPressedThisFrame()) Restart();
        }

        private void EquipIndex(int index)
        {
            _kitIndex = index;
            _player.Equip(_kits.All[index]);
        }

        private void Heal()
        {
            if (!_player.Controller.enabled) _player.Revive(_player.transform.position);
            else _player.Vitals.Health.Restore(25f);
        }

        private void Restart()
        {
            _player.Revive(_world.PlayerSpawn);
            foreach (var enemy in _context.Enemies.All)
                if (enemy != null) enemy.ResetToHome();
            foreach (var loot in FindObjectsByType<LootPickup>()) Destroy(loot.gameObject);
            _context.Feed.Add("Campo de pruebas reiniciado.");
        }

        private static GameCamera EnsureCamera()
        {
            var camera = Camera.main != null
                ? Camera.main
                : new GameObject("Main Camera") { tag = "MainCamera" }.AddComponent<Camera>();
            var rig = camera.GetComponent<GameCamera>();
            return rig != null ? rig : camera.gameObject.AddComponent<GameCamera>();
        }
    }
}
