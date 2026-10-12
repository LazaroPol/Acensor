using Specimen.Core.Classes;
using Specimen.Core.Visuals;
using Specimen.Runtime.Player;
using Specimen.Runtime.Sandbox.Ui;
using Specimen.Runtime.Sandbox.World;
using Specimen.Runtime.Visuals;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Specimen.Runtime.Sandbox
{
    /// <summary>
    /// Arranque de la escena «CampoPruebas»: monta la Armería, el poblado, jugador, bestiario e interfaz con el
    /// arte generado (o greybox si falta) y atiende los atajos de prueba (kit con Tab, reinicio, dañar/curar).
    /// </summary>
    public sealed partial class SandboxBootstrap : MonoBehaviour
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
            var camera = EnsureCamera();
            _context.Mouse = gameObject.AddComponent<MouseGestures>();
            _player = _world.SpawnPlayer(camera);
            gameObject.AddComponent<WowCamera>().Init(camera, _context.Mouse, _player);
            gameObject.AddComponent<InteractionHub>().Init(_context, _player);
            _world.PlaceBenches(_player);
            new VillageBuilder(_context).Build();
            _player.Equip(_kits.All[0]);
            _world.SpawnBestiary(_player);
            _context.LoadCrafting();
            gameObject.AddComponent<Nameplates>().Init(_context, _player.GetComponent<PlayerTarget>());
            new GameObject(nameof(SandboxUi)).AddComponent<SandboxUi>().Init(_context, _player, _kits.All, Equip);
            _context.Feed.Add("Sal por la puerta sur de la Armería: el poblado, el bosque y los campamentos te esperan.");
            SandboxLighting.Apply(camera.GetComponent<Camera>());
            if (arte == null) _context.Feed.Add("Sin CatalogoArte: todo en greybox (SPECIMEN › Arte › 2 · Generar).");
        }

        private void OnApplicationQuit() => _context?.SaveCrafting();

        private void OnDestroy()
        {
            _context?.SaveCrafting();
            _context?.Input.Dispose();
        }

        private void Update()
        {
            if (_context == null) return;
            var input = _context.Input;
            if (input.NextClass.WasPressedThisFrame()) EquipIndex((_kitIndex + 1) % _kits.All.Count);
            if (input.Hurt.WasPressedThisFrame()) _player.Vitals.Health.Drain(15f);
            if (input.Heal.WasPressedThisFrame()) Heal();
            if (input.Restart.WasPressedThisFrame()) Restart();
            TestShortcuts(input);
        }

        private void EquipIndex(int index) => Equip(_kits.All[index]);

        private void Equip(ClassKit kit)
        {
            for (int i = 0; i < _kits.All.Count; i++)
                if (_kits.All[i] == kit) _kitIndex = i;
            _player.Equip(kit);
        }

        private void Heal()
        {
            if (_player.IsDead) _player.Revive(_player.transform.position);
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
