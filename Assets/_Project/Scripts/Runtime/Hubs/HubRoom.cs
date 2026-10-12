using System.Collections.Generic;
using Specimen.Core.Classes;
using Specimen.Core.Hubs;
using Specimen.Core.Hud;
using Specimen.Core.Iteration;
using Specimen.Core.Visuals;
using Specimen.Runtime.Combat;
using Specimen.Runtime.Incursion;
using Specimen.Runtime.Player;
using Specimen.Runtime.Prototype;
using Specimen.Runtime.Visuals;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Specimen.Runtime.Hubs
{
    /// <summary>
    /// Sala 3D de un hub: monta la geometría de <see cref="HubRoomLayout"/>, al jugador y resuelve la
    /// interacción por cercanía (E) con las estaciones y el Ascensor. Quien la usa pone las etiquetas y acciones.
    /// </summary>
    public sealed class HubRoom : MonoBehaviour
    {
        private const float Range = 2.2f;
        private readonly List<(float X, float Z)> _points = new List<(float X, float Z)>();
        private HubRoomLayout _layout;
        private Transform _player;
        private System.Func<int, string> _label;
        private System.Action<int> _use;

        /// <summary>Índice del Ascensor en las interacciones (las estaciones van de 0 a n-1).</summary>
        public int ElevatorIndex => _layout.StationCount;

        /// <summary>Ataque del jugador en la sala (se puede re-equipar al coger otro kit).</summary>
        public PlayerAttack Attack { get; private set; }

        public PlayerController Build(HubRoomLayout layout, ClassKit kit, IterationRun run, HudNotices notices,
            System.Func<int, string> label, System.Action<int> use)
        {
            _layout = layout;
            _label = label;
            _use = use;
            var spawner = new GameObject("Sala").AddComponent<VisualSpawner>();
            spawner.Init(new VisualCatalog<GameObject>(new DefaultGreyboxStyles()));
            foreach (var placement in layout.Build()) spawner.Spawn(placement);
            for (int i = 0; i < layout.StationCount; i++) _points.Add(layout.StationPoint(i));
            _points.Add(layout.ElevatorPoint);
            var (sx, sz) = layout.SpawnPoint;
            var player = IncursionPlayer.Spawn(new Vector3(sx, 1.2f, sz), KitStats.For(kit, Bootstrap.GameBootstrap.Services), run, notices);
            _player = player.transform;
            player.gameObject.AddComponent<KitBadge>().Init(Bootstrap.GameBootstrap.Services);
            Attack = player.gameObject.AddComponent<PlayerAttack>();
            Attack.Init(kit, player.Vitals);
            PrototypeScenery.EnsureLight();
            return player;
        }

        private int Focused() =>
            _player == null ? -1 : ProximityPicker.Nearest(_points, _player.position.x, _player.position.z, Range);

        private void Update()
        {
            int i = Focused();
            if (i < 0 || Keyboard.current == null || !Keyboard.current.eKey.wasPressedThisFrame) return;
            _use(i);
        }

        private void OnGUI()
        {
            int i = Focused();
            if (i < 0) return;
            GUI.Label(new Rect(Screen.width / 2f - 200f, Screen.height - 60f, 400f, 28f), $"[E] {_label(i)}");
        }
    }
}
