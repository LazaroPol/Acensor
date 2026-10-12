using Specimen.Core.Common;
using Specimen.Core.Time;

namespace Specimen.Core.Art
{
    /// <summary>Fundido lineal entre ambientes (el «blend por zona» de los Volumes): al cambiar el objetivo parte de lo que se ve ahora.</summary>
    public sealed class MoodTransition : ITickable
    {
        private readonly float _duration;
        private MoodProfile _from;
        private float _elapsed;

        public MoodTransition(ColorMood initial, float durationSeconds = 1.5f)
        {
            _duration = Guard.Positive(durationSeconds, nameof(durationSeconds));
            Target = initial;
            _from = MoodProfiles.Of(initial);
            _elapsed = _duration;
        }

        public ColorMood Target { get; private set; }
        public bool IsBlending => _elapsed < _duration;
        public MoodProfile Current => MoodProfile.Lerp(_from, MoodProfiles.Of(Target), _elapsed / _duration);

        public void SetTarget(ColorMood mood)
        {
            if (mood == Target) return;
            _from = Current;
            Target = mood;
            _elapsed = 0f;
        }

        public void Tick(float deltaSeconds)
        {
            Guard.NonNegative(deltaSeconds, nameof(deltaSeconds));
            _elapsed = System.Math.Min(_duration, _elapsed + deltaSeconds);
        }
    }
}
