using System;
using Specimen.Core.Common;

namespace Specimen.Core.Siege
{
    /// <summary>
    /// Director del asedio (GDD §3.4): reparte los enemigos de cada oleada, detecta cuándo se despeja,
    /// espera la pausa entre oleadas y declara superado el asedio tras la última (llega el Osprey).
    /// </summary>
    public sealed class WaveDirector
    {
        private readonly WaveConfig _config;
        private int _toSpawn;
        private float _pause;

        public WaveDirector(WaveConfig config) => _config = Guard.NotNull(config, nameof(config));

        public event Action<int> WaveStarted;
        public event Action Survived;

        public WaveState State { get; private set; } = WaveState.Idle;
        public int CurrentWave { get; private set; }
        public int Alive { get; private set; }
        public int PendingSpawns => _toSpawn;
        public int Kills { get; private set; }

        public void Start()
        {
            if (State != WaveState.Idle) throw new InvalidOperationException("El asedio ya empezó.");
            BeginWave(1);
        }

        /// <summary>Consume una aparición pendiente de la oleada; false si no hay que generar nada ahora.</summary>
        public bool TrySpawn()
        {
            if (State != WaveState.InWave || _toSpawn == 0) return false;
            _toSpawn--;
            Alive++;
            return true;
        }

        public void OnEnemyKilled()
        {
            if (State != WaveState.InWave || Alive == 0) throw new InvalidOperationException("No hay enemigos vivos.");
            Alive--;
            Kills++;
            if (_toSpawn > 0 || Alive > 0) return;
            if (CurrentWave == _config.WaveCount)
            {
                State = WaveState.Survived;
                Survived?.Invoke();
            }
            else
            {
                State = WaveState.Intermission;
                _pause = _config.IntermissionSeconds;
            }
        }

        public void Tick(float deltaSeconds)
        {
            Guard.NonNegative(deltaSeconds, nameof(deltaSeconds));
            if (State != WaveState.Intermission) return;
            _pause -= deltaSeconds;
            if (_pause <= 0f) BeginWave(CurrentWave + 1);
        }

        private void BeginWave(int wave)
        {
            CurrentWave = wave;
            _toSpawn = _config.SizeOf(wave);
            State = WaveState.InWave;
            WaveStarted?.Invoke(wave);
        }
    }
}
