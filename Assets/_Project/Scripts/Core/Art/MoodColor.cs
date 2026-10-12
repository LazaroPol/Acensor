namespace Specimen.Core.Art
{
    /// <summary>Color RGB lineal 0..1 independiente de Unity.</summary>
    public readonly struct MoodColor
    {
        public MoodColor(float r, float g, float b) { R = r; G = g; B = b; }

        public float R { get; }
        public float G { get; }
        public float B { get; }

        public static MoodColor Lerp(MoodColor a, MoodColor b, float t) =>
            new MoodColor(a.R + (b.R - a.R) * t, a.G + (b.G - a.G) * t, a.B + (b.B - a.B) * t);
    }
}
