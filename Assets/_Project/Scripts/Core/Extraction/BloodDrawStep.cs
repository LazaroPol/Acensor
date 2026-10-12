using Specimen.Core.Common;

namespace Specimen.Core.Extraction
{
    /// <summary>Un paso de la cinemática: texto en pantalla, duración y tono.</summary>
    public sealed class BloodDrawStep
    {
        public BloodDrawStep(string text, float seconds, BloodDrawTone tone)
        {
            Text = Guard.NotNull(text, nameof(text));
            Seconds = Guard.Positive(seconds, nameof(seconds));
            Tone = tone;
        }

        public string Text { get; }
        public float Seconds { get; }
        public BloodDrawTone Tone { get; }
    }
}
