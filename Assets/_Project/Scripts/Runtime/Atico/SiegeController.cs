using Specimen.Core.Composition;
using Specimen.Core.Iteration;
using Specimen.Core.Siege;
using Specimen.Core.Vitals;
using Specimen.Runtime.Combat;
using UnityEngine;

namespace Specimen.Runtime.Atico
{
    /// <summary>Dirige el asedio en la azotea: aparece cada oleada del <see cref="WaveDirector"/> y avisa al sobrevivir.</summary>
    public sealed class SiegeController : MonoBehaviour
    {
        private const float SpawnRadius = 13f;
        private WaveDirector _director;
        private IterationRun _run;
        private Transform _player;
        private CharacterVitals _vitals;
        private int _spawned;

        public bool Survived => _director != null && _director.State == WaveState.Survived;
        public string Status => _director == null ? string.Empty : $"Oleada {_director.CurrentWave} · enemigos {_director.Alive + _director.PendingSpawns}";

        public void Init(IterationRun run, Transform player, CharacterVitals vitals)
        {
            _run = run;
            _player = player;
            _vitals = vitals;
            _director = new WaveDirector(new WaveConfig(3, 4, 2, 8f));
            _director.Survived += run.CompleteSiege;
            _director.Start();
        }

        private void Update()
        {
            if (_director == null) return;
            _director.Tick(Time.deltaTime);
            while (_director.TrySpawn()) Spawn();
        }

        private void Spawn()
        {
            float angle = _spawned * 2.4f;
            var center = new Vector3(AticoBootstrap.Size / 2f, 0.5f, AticoBootstrap.Size / 2f);
            var at = center + new Vector3(Mathf.Cos(angle), 0f, Mathf.Sin(angle)) * SpawnRadius;
            var actor = EnemySpawner.SpawnOne(at, _run.Floor, 3, _spawned++, _player, _vitals);
            actor.Died += _director.OnEnemyKilled;
        }
    }
}
