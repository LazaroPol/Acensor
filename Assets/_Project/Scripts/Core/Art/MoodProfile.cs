using Specimen.Core.Common;

namespace Specimen.Core.Art
{
    /// <summary>
    /// Parámetros de ambiente que un Volume URP y el shader toon aplican: saturación y temperatura (-100..100),
    /// exposición (EV), niebla, luz ambiente y grosor del contorno entintado.
    /// </summary>
    public readonly struct MoodProfile
    {
        public MoodProfile(float saturation, float temperature, float exposure, float fogDensity,
            MoodColor fogColor, MoodColor ambient, float outlineWidth)
        {
            Saturation = saturation;
            Temperature = temperature;
            Exposure = exposure;
            FogDensity = Guard.NonNegative(fogDensity, nameof(fogDensity));
            FogColor = fogColor;
            Ambient = ambient;
            OutlineWidth = Guard.NonNegative(outlineWidth, nameof(outlineWidth));
        }

        public float Saturation { get; }
        public float Temperature { get; }
        public float Exposure { get; }
        public float FogDensity { get; }
        public MoodColor FogColor { get; }
        public MoodColor Ambient { get; }
        public float OutlineWidth { get; }

        public static MoodProfile Lerp(MoodProfile a, MoodProfile b, float t)
        {
            t = t < 0f ? 0f : t > 1f ? 1f : t;
            return new MoodProfile(
                L(a.Saturation, b.Saturation, t), L(a.Temperature, b.Temperature, t), L(a.Exposure, b.Exposure, t),
                L(a.FogDensity, b.FogDensity, t), MoodColor.Lerp(a.FogColor, b.FogColor, t),
                MoodColor.Lerp(a.Ambient, b.Ambient, t), L(a.OutlineWidth, b.OutlineWidth, t));
        }

        private static float L(float a, float b, float t) => a + (b - a) * t;
    }
}
