using Specimen.Core.Composition;
using Specimen.Core.Iteration.Presence;

namespace Specimen.Core.Art
{
    /// <summary>Decide el ambiente según dónde está el jugador o qué escena se muestra.</summary>
    public static class MoodSelector
    {
        public static ColorMood For(PlayerLocation location)
        {
            switch (location)
            {
                case PlayerLocation.InElevator: return ColorMood.Refugio;
                case PlayerLocation.Trapped: return ColorMood.Atrapado;
                default: return ColorMood.Piso;
            }
        }

        public static ColorMood For(SceneId scene)
        {
            switch (scene)
            {
                case SceneId.Laboratorio: return ColorMood.Laboratorio;
                case SceneId.Atico:
                case SceneId.Incursion: return ColorMood.Piso;
                default: return ColorMood.Refugio;
            }
        }
    }
}
