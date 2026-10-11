using System;
using System.Collections.Generic;

namespace Specimen.Core.Combat.Perception
{
    /// <summary>Combina visión y oído de un enemigo con los modificadores inyectados (p. ej. los de Atrapado).</summary>
    public sealed class EnemyPerception
    {
        private readonly PerceptionConfig _config;
        private readonly IPerceptionModifiers _modifiers;
        private readonly ISense _vision;
        private readonly ISense _hearing;

        public EnemyPerception(PerceptionConfig config, IPerceptionModifiers modifiers)
            : this(config, modifiers, new VisionSense(Checked(config).VisionHalfAngle), new HearingSense())
        {
        }

        public EnemyPerception(PerceptionConfig config, IPerceptionModifiers modifiers, ISense vision, ISense hearing)
        {
            _config = Checked(config);
            _modifiers = modifiers ?? throw new ArgumentNullException(nameof(modifiers));
            _vision = vision ?? throw new ArgumentNullException(nameof(vision));
            _hearing = hearing ?? throw new ArgumentNullException(nameof(hearing));
        }

        /// <summary>Cómo percibe el observador a un objetivo.</summary>
        public Awareness Perceive(Observer observer, PerceivedTarget target)
        {
            if (target == null) throw new ArgumentNullException(nameof(target));
            float visionRange = _config.VisionRange * _modifiers.VisionMultiplier(target);
            if (_vision.Detects(observer, target, visionRange)) return Awareness.Seen;
            return _hearing.Detects(observer, target, _config.HearingRange) ? Awareness.Heard : Awareness.None;
        }

        /// <summary>Objetivos percibidos (vistos u oídos), en el orden recibido.</summary>
        public IReadOnlyList<PerceivedTarget> Detected(Observer observer, IEnumerable<PerceivedTarget> targets)
        {
            if (targets == null) throw new ArgumentNullException(nameof(targets));
            var result = new List<PerceivedTarget>();
            foreach (PerceivedTarget target in targets)
                if (Perceive(observer, target) != Awareness.None) result.Add(target);
            return result;
        }

        /// <summary>Velocidad con la que el enemigo persigue al objetivo.</summary>
        public float ChaseSpeed(PerceivedTarget target) =>
            _config.ChaseSpeed * _modifiers.SpeedMultiplier(target ?? throw new ArgumentNullException(nameof(target)));

        private static PerceptionConfig Checked(PerceptionConfig config) =>
            config ?? throw new ArgumentNullException(nameof(config));
    }
}
