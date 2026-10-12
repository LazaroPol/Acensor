using System;
using System.Collections.Generic;
using Specimen.Core.Bestiary;
using Specimen.Core.Combat.Enemies;
using Specimen.Core.Professions;
using Specimen.Core.Sandbox;
using Specimen.Core.Visuals;
using Specimen.Runtime.Player;
using Specimen.Runtime.Visuals;
using UnityEngine;

namespace Specimen.Runtime.Sandbox
{
    /// <summary>Construye el contenido del Campo de pruebas: sala, decorado, bancos, jugador y bestiario.</summary>
    public sealed partial class SandboxWorld
    {
        private readonly SandboxContext _context;
        private readonly SandboxLayout _layout = new SandboxLayout();
        private readonly EnemyFactory _factory = new EnemyFactory(new LinearDepthScaling(DepthScalingConfig.Default));

        /// <summary>Distancia inicial de cámara (m): más cerca que en la Incursión para ver bien modelos y animaciones.</summary>
        public const float CameraDistance = 8f;

        public SandboxWorld(SandboxContext context) => _context = context;

        public Vector3 PlayerSpawn => At(_layout.PlayerSpawn) + Vector3.up * 0.05f;

        public void BuildRoom()
        {
            var spawner = new GameObject("Nivel").AddComponent<VisualSpawner>();
            spawner.Init(_context.Visuals);
            foreach (var placement in _layout.Room()) spawner.Spawn(placement);
            foreach (var spot in _layout.Decor())
                VisualFactory.Prop(Decor(spot.Id), At(spot), spot.Yaw, new Vector3(1f, 1f, 1f), new Color(0.5f, 0.4f, 0.3f));
        }

        public BenchStation[] PlaceBenches(SandboxPlayer player)
        {
            var benches = new List<BenchStation>();
            foreach (var spot in _layout.Benches())
            {
                var profession = (ProfessionId)Enum.Parse(typeof(ProfessionId), spot.Id);
                var prefab = _context.Art != null ? _context.Art.BenchPrefab(profession) : null;
                var go = VisualFactory.Prop(prefab, At(spot), spot.Yaw, new Vector3(1.4f, 0.8f, 0.7f), new Color(0.55f, 0.35f, 0.2f));
                go.name = $"Banco_{profession}";
                var station = go.AddComponent<BenchStation>();
                station.Init(profession, _context);
                _context.Interactions?.Register(station);
                benches.Add(station);
            }
            return benches.ToArray();
        }

        public SandboxPlayer SpawnPlayer(GameCamera camera)
        {
            var root = new GameObject("Jugador");
            root.transform.position = PlayerSpawn;
            var body = root.AddComponent<CharacterController>();
            body.height = 1.8f;
            body.radius = 0.4f;
            body.center = new Vector3(0f, 0.9f, 0f);
            var controller = root.AddComponent<PlayerController>();
            var player = root.AddComponent<SandboxPlayer>();
            player.Init(_context, camera.transform);
            root.AddComponent<PlayerCombat>().Init(player, _context);
            root.AddComponent<SignatureCaster>().Init(player, _context);
            root.AddComponent<SpellCaster>().Init(player, _context);
            camera.Follow(root.transform, controller.Input, CameraDistance);
            return player;
        }

        private GameObject Decor(string name)
        {
            if (_context.Art == null) return null;
            foreach (var prefab in _context.Art.decorado)
                if (prefab != null && prefab.name == $"Decor_{name}") return prefab;
            return null;
        }

        /// <summary>Altura de la cara superior del suelo (las baldosas greybox miden 0,1 m).</summary>
        private const float FloorTop = 0.1f;

        private static Vector3 At(SandboxSpot spot) => new Vector3(spot.X, FloorTop, spot.Z);
    }
}
