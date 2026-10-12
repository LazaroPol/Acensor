using System;

namespace Specimen.Core.Art
{
    /// <summary>Perfiles de ambiente por defecto (GDD 13): refugio cálido, pisos fríos, atrapado deslumbrante, laboratorio clínico.</summary>
    public static class MoodProfiles
    {
        public static MoodProfile Of(ColorMood mood)
        {
            switch (mood)
            {
                case ColorMood.Refugio:
                    return new MoodProfile(25f, 30f, 0.2f, 0f, new MoodColor(0.85f, 0.65f, 0.35f),
                        new MoodColor(0.90f, 0.75f, 0.50f), 0.02f);
                case ColorMood.Piso:
                    return new MoodProfile(-35f, -30f, -0.2f, 0.03f, new MoodColor(0.55f, 0.62f, 0.78f),
                        new MoodColor(0.50f, 0.55f, 0.70f), 0.02f);
                case ColorMood.Atrapado:
                    return new MoodProfile(-20f, -10f, 1f, 0.01f, new MoodColor(0.90f, 0.92f, 1f),
                        new MoodColor(1f, 1f, 1f), 0.035f);
                case ColorMood.Laboratorio:
                    return new MoodProfile(-60f, -5f, 0.3f, 0f, new MoodColor(0.85f, 0.90f, 0.92f),
                        new MoodColor(0.80f, 0.88f, 0.90f), 0.015f);
                default:
                    throw new ArgumentOutOfRangeException(nameof(mood), mood, null);
            }
        }
    }
}
