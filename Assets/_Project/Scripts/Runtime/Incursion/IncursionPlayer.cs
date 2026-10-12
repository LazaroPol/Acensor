using Specimen.Core.Hud;
using Specimen.Core.Iteration;
using Specimen.Core.Stats;
using Specimen.Runtime.Hud;
using Specimen.Runtime.Player;
using UnityEngine;

namespace Specimen.Runtime.Incursion
{
    /// <summary>Monta el jugador del piso: cápsula con controlador, cámara en tercera persona y HUD de partida.</summary>
    public static class IncursionPlayer
    {
        public static PlayerController Spawn(Vector3 position, StatSheet stats, IterationRun run, HudNotices notices)
        {
            var root = GameObject.CreatePrimitive(PrimitiveType.Capsule);
            root.name = "Jugador";
            Object.Destroy(root.GetComponent<Collider>());
            root.transform.position = position;
            var body = root.AddComponent<CharacterController>();
            body.height = 1.8f;
            body.radius = 0.4f;
            var camera = EnsureCamera();
            var player = root.AddComponent<PlayerController>();
            player.Init(stats, camera.transform);
            camera.Follow(root.transform, player.Input);
            var hud = new GameObject(nameof(GameHud)).AddComponent<GameHud>();
            hud.Init(new GameHudPresenter(run, player.Vitals, notices));
            return player;
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
