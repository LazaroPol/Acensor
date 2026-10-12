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
using Specimen.Runtime.Incursion;
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

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
        private static void EnsureInAtticScene()
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
            IncursionPlayer.Spawn(new Vector3(Size / 2f, 1.2f, Size / 2f), kit.CreateStatSheet(), _run,
                new HudNotices(s.Resolve<IEventBus>()));
            PrototypeScenery.EnsureLight();
        }

        private static IEnumerable<LevelPlacement> Platform()
        {
            for (var x = 0; x < Size; x++)
                for (var z = 0; z < Size; z++)
                    yield return new LevelPlacement(VisualId.Suelo, x, 0f, z);
        }

        private void Update() => _run?.Tick(Time.deltaTime);
    }
}
