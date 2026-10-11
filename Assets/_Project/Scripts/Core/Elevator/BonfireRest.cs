using System;
using Specimen.Core.Common;
using Specimen.Core.Time;
using Specimen.Core.Vitals;

namespace Specimen.Core.Elevator
{
    /// <summary>
    /// Descanso de un personaje junto a la Hoguera (GDD §5-§6): regenera vida y stamina y recupera
    /// fatiga a un ritmo que escala con el nivel del módulo. Sin Hoguera construida no hace nada.
    /// </summary>
    public sealed class BonfireRest : ITickable
    {
        private readonly CharacterVitals _vitals;
        private readonly Fatigue _fatigue;
        private readonly IModuleLevels _levels;
        private readonly BonfireConfig _config;
        private float _restedSeconds;

        /// <summary>Se lanza cada vez que el descanso recupera un punto de fatiga.</summary>
        public event Action<Fatigue> FatigueRecovered;

        public BonfireRest(CharacterVitals vitals, Fatigue fatigue, IModuleLevels levels, BonfireConfig config = null)
        {
            _vitals = Guard.NotNull(vitals, nameof(vitals));
            _fatigue = Guard.NotNull(fatigue, nameof(fatigue));
            _levels = Guard.NotNull(levels, nameof(levels));
            _config = config ?? BonfireConfig.Default;
        }

        public int Level => _levels.LevelOf(ModuleType.Bonfire);

        public void Tick(float deltaSeconds)
        {
            Guard.NonNegative(deltaSeconds, nameof(deltaSeconds));
            int level = Level;
            if (level <= 0 || deltaSeconds <= 0f) return;
            _vitals.Health.Restore(deltaSeconds * _config.HealthPerSecondPerLevel * level);
            _vitals.Stamina.Restore(deltaSeconds * _config.StaminaPerSecondPerLevel * level);
            RecoverFatigue(deltaSeconds, _config.FatigueIntervalAt(level));
        }

        private void RecoverFatigue(float deltaSeconds, float interval)
        {
            if (_fatigue.Points == 0) { _restedSeconds = 0f; return; }
            _restedSeconds += deltaSeconds;
            while (_restedSeconds >= interval && _fatigue.Points > 0)
            {
                _restedSeconds -= interval;
                _fatigue.Recover(1);
                FatigueRecovered?.Invoke(_fatigue);
            }
        }
    }
}
